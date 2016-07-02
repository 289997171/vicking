using System;
using System.IO;
using ProtoBuf.Meta;
using UnityEngine;

public class NetMessageBuilder
{
    // 对于所有线程里，它的值都是一个，即它的唯一性。(因为U3D中是保证了线程安全的，所以不需要加锁)
    // [ThreadStatic]
    private static MemoryStream memoryStream = new MemoryStream(4096);

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="msgID"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static byte[] Encode(int msgID, global::ProtoBuf.IExtensible msg)
    {
        RuntimeTypeModel.Default.Serialize(memoryStream, msg, null);

        // Serializer.Serialize(memoryStream, msg);

        // 修改为short版本
        byte[] ret = new byte[4 + memoryStream.Length];

        int msgLength = 2 + (int)memoryStream.Length; // 不包含数据长度
        byte[] bytes = new byte[2];
        bytes[0] = (byte)(msgLength >> 8);
        bytes[1] = (byte)(msgLength);
        ret[0] = bytes[0];
        ret[1] = bytes[1];


        bytes = new byte[2];
        bytes[0] = (byte)(msgID >> 8);
        bytes[1] = (byte)(msgID);
        ret[2] = bytes[0];
        ret[3] = bytes[1];

        memoryStream.Position = 0;

        memoryStream.Position = 0;
        memoryStream.Read(ret, 4, ret.Length - 4);

        // 原服务器版本
        //byte[] ret = new byte[8 + memoryStream.Length];
        //int msgLength = 4 + (int)memoryStream.Length; // 不包含数据长度

        //byte[] lenthbytes = BitConverter.GetBytes(msgLength);
        //byte[] msgidbytes = BitConverter.GetBytes(msgID);

        //Array.Reverse(lenthbytes);
        //Array.Reverse(msgidbytes);

        //Array.Copy(lenthbytes, 0, ret, 0, lenthbytes.Length);
        //Array.Copy(msgidbytes, 0, ret, 4, msgidbytes.Length);

        //memoryStream.Position = 0;
        //memoryStream.Read(ret, 8, ret.Length - 8);

        // LogSystem.Info("encode message:id {0} len({1})[{2}]", id, ret.Length - 2, msg.GetType().Name);

        return ret;

    }

    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="type"></param>
    /// <param name="baseMessage"></param>
    /// <returns></returns>
    public static T Decode<T>(NetMessage baseMessage)
    {
        // 新版本short
        memoryStream.SetLength(0);
        memoryStream.Write(baseMessage.MessageData, 4, baseMessage.MessageData.Length - 4);
        memoryStream.Position = 0;
        try
        {
            // object msg = Serializer.Deserialize(memoryStream, null, t);
            T msg = (T)RuntimeTypeModel.Default.Deserialize(memoryStream, null, typeof(T), null);
            if (msg == null)
            {
                Debug.LogFormat("decode message error:can't find id {0} len({1}) !!!", baseMessage.MessageID,
                    baseMessage.MessageData.Length - 4);
                return msg;
            }
            // DebugSystem.Info("decode message:id {0} len({1})[{2}]", id, msgbuf.Length - 2, msg.GetType().Name);
            return msg;
        }
        catch (Exception ex)
        {
            //Debug.LogErrorFormat("decode message error:id({0}) len({1}) {2}\n{3}\nData:\n{4}", baseMessage.MessageID,
            //    baseMessage.MessageData.Length - 4, ex.Message, ex.StackTrace,
            //    ByteUtil.BinToHex(baseMessage.MessageData, 4));
            throw ex;
        }

        //// 老版本int
        //memoryStream.SetLength(0);
        //memoryStream.Write(baseMessage.MessageData, 8, baseMessage.MessageData.Length - 8);
        //memoryStream.Position = 0;

        //T msg = (T)RuntimeTypeModel.Default.Deserialize(memoryStream, null, typeof(T), null);
        //return msg;
    }


    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="type"></param>
    /// <param name="bytes"></param>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static T Decode<T>(byte[] bytes, int index = 0, int length = 0)
    {
        memoryStream.SetLength(0);
        memoryStream.Write(bytes, index, length == 0 ? bytes.Length : length);
        memoryStream.Position = 0;

        T msg = (T)RuntimeTypeModel.Default.Deserialize(memoryStream, null, typeof(T), null);
        return msg;
    }

    //            targets[1] = (byte) s;
    //            targets[0] = (byte) (s >> 8);
    //            byte[] targets = new byte[2];  
    //        public static byte[] shortToByteArray(int s) {  

    // byte[] short 转换
    //            return targets;
    //        }  
    //            
    //       
    //        public static int byte2short(byte[] b) {  
    //            return b[0] << 8 | b[1] & 0xFF;  
    //        }
}
