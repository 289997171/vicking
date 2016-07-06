
using com.game.proto;

public class ResPingHandler
{
    public static void Execute(ResPingMessage message)
    {
        NetManager.Instance.OnPing();
    }
}