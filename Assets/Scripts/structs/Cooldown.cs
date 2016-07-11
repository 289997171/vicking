
using System;

public class Cooldown
{
    //冷却类型
    public string type;
    //关键字
    public string key;
    //开始时间
    public long start;
    //持续时间
    public long delay;
    //结束时间
    public long endTime;

    /**
     * 获取结束时间
     *
     * @return
     */
    public long getEndTime()
    {
        return start + delay;
    }

    /**
     * 获取剩余时间
     *
     * @return
     */
    public long getRemainTime()
    {
        return getEndTime() - DateTime.Now.Ticks;
    }

    public void release()
    {
        key = null;
        type = null;
        start = 0;
        delay = 0;
    }
}
