using HarmonyLib;

namespace VT_Referance.Event
{
    internal static class ServerSingleton
    {
        private static Server _instance;
        private static readonly object _lock = new object();

        public static Server Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Server();
                        var instance = new Harmony("VT_Referance.Patch.Event");
                        instance.PatchAll();
                    }
                    return _instance;
                }
            }
        }
    }

    public class Server
    {
        public Server(){}
        public EventHandler Events { get; } = new EventHandler();
    }
    public class EventHandler
    {
        public VT_ServerEvents Server { get; } = new VT_ServerEvents();
        public VT_PlayerEvents Player { get; } = new VT_PlayerEvents();
        public VT_RoundEvents Round { get; } = new VT_RoundEvents();
        public VT_MapEvents Map { get; } = new VT_MapEvents();
        public VT_ScpEvents Scp { get; } = new VT_ScpEvents();
        public VT_GrenadeEvents Grenade { get; } = new VT_GrenadeEvents();
    }
}

/*
    private static class Singleton<T>
    where T : class, new()
    {
        private static T _instance;
        private static readonly object _lock = new object();
        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new T());
                }
            }
        }
    }
*/



