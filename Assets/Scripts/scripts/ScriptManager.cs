public class ScriptManager : NormalSingleton<ScriptManager>, IManager
{

    

    public bool Init()
    {
        GameNetCloseEvts gameNetCloseEvts = GameNetCloseEvts.Instance;
        return true;
    }
    
}
