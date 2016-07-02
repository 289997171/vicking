using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Http请求工具类
/// </summary>
public class HttpUtil : DDOSingleton<HttpUtil>
{

    public void SendHttpRequest(string url, WWWForm form = null, Action<WWW> resultProcess = null)
    {
        StartCoroutine(SendHttpRequestSync(url, form, resultProcess));
    }

    public void SendHttpRequest(string url, Action<WWW> resultProcess = null, params object[] args)
    {
        if (args != null && args.Length > 1)
        {
            WWWForm form = null;
            for (int i = 0; i < args.Length - 1; i += 2)
            {
                form = new WWWForm();
                form.AddField((string)args[i], (string)args[i + 1]);
            }
            StartCoroutine(SendHttpRequestSync(url, form, resultProcess));
        }
        else
        {
            StartCoroutine(SendHttpRequestSync(url, null, resultProcess));
        }
    }

    private IEnumerator SendHttpRequestSync(string url, WWWForm form = null, Action<WWW> resultProcess = null)
    {
        yield return 0;

        WWW www = null;
        if (form != null)
        {
            www = new WWW(url, form);
        }
        else
        {
            www = new WWW(url);
        }

        yield return www;

        if (resultProcess != null) resultProcess(www);
    }


#if SDK_TEST
        void OnGUI()
        {
            if (GUI.Button(new Rect(200, 10, 100, 30), "测试1"))
            {
                HttpUtil.Instance.SendHttpRequest("http://www.baidu.com", null, (w) =>
                {
                    Debug.Log("w = " + w.text);
                });
            }

            if (GUI.Button(new Rect(200, 50, 100, 30), "测试2"))
            {
            }
        }
#endif
}
