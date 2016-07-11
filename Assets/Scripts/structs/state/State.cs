
/// <summary>
/// 状态
/// </summary>
public class State
{
    // 状态值
    public int Value;

    // 状态组
    public int Mark;

    public State(int value, int mark)
    {
        this.Value = value;
        this.Mark = mark;
    }

    /// <summary>
    /// 判断玩家是否处于该状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool compare(int state)
    {
        return ((this.Value & state) != 0);
    }
}
