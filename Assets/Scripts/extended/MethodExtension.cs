
using UnityEngine;

/// <summary>
/// 类的成员属性扩展
/// </summary>
public static class MethodExtension
{
    /// <summary>
    /// 获得或添加组件
    /// </summary>
    /// <returns>The or add component.</returns>
    /// <param name="go">Go.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T getOrAddComponent<T>(this GameObject go) where T : Component
    {
        T ret = go.GetComponent<T>();
        if (null == ret)
            ret = go.AddComponent<T>();
        return ret;
    }

    /// <summary>
    /// 通过名称查找节点
    /// 会遍历所有子节点，以及子节点的子节点查找
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    public static Transform findInChildrens(this Transform transform, string nodeName)
    {
        if (transform.name.Equals(nodeName)) return transform;

        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform node = transform.GetChild(i);
            if (node.name.Equals(nodeName))
            {
                return node;
            }
            Transform ret = node.findInChildrens(nodeName);
            if (ret != null)
            {
                return ret;
            }
        }

        return null;
    }

}
