
public class NetMessage
{
    // 数据，包含了数据长度（2） + 消息ID（2） + 消息体（N）
    public byte[] MessageData;
    // 消息ID
    public int MessageID;

    public NetMessage(byte[] bytes)
    {
        // 新版本
        MessageID = bytes[2] << 8 | bytes[3] & 0xFF;

        // 老版本
        //            MessageID = BitConverter.ToInt32(bytes, 4);
        //            Debug.LogError("MessageID :" + MessageID);
        //            MessageID = (bytes[4] & 0xFF) | ((bytes[5] & 0xFF) << 8) | ((bytes[6] & 0xFF) << 16) | ((bytes[7] & 0xFF) << 24);
        //            Debug.LogError("MessageID :" + MessageID);
        // MessageID = (bytes[7] & 0xFF) | ((bytes[6] & 0xFF) << 8) | ((bytes[5] & 0xFF) << 16) | ((bytes[4] & 0xFF) << 24);
        MessageData = bytes;
    }
}
