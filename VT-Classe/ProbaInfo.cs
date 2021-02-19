namespace CustomClass
{
    public class ProbaInfo
    {
        public int Role { get; private set; }
        public int MinPlayer { get; private set; }

        public ProbaInfo(int role, int minPlayer)
        {
            Role = role;
            MinPlayer = minPlayer;
        }
    }
}
