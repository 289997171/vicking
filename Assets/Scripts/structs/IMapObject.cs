
/// <summary>
/// 地图对象
/// </summary>
public interface IMapObject
{
    /// <summary>
    /// 获得地图ID(唯一ID)
    /// </summary>
    /// <returns></returns>
    long getMapId();

    /// <summary>
    /// 获得地图模型ID(配置ID)
    /// </summary>
    /// <returns></returns>
    int getMapModelId();


    /// <summary>
    /// 是否对person可见
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    bool canSee(Person person);
}
