
using System;

public class RandomUtil
{
    private static Random random = new Random();
    public static int next(int maxValue,  int minValue = 0)
    {
        return random.Next(minValue, maxValue);
    }
}
