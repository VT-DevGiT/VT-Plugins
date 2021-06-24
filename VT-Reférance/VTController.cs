namespace VT_Referance
{
    public class VTController
    {
        public static Event.Server Server { get { return Event.ServerSingleton.Instance; } }

        public static Method.TextHandle TextHandler { get { return Method.TextHandleSingleton.Instance; } }
    }
}

