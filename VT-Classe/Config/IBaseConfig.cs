using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.Config
{
    public interface IBaseConfig : IConfigSection
    {
        SerializedMapPoint ConfigSpawnPoint { get; }
        int ConfigHealth { get; }
        int ConfigArtificialHealth { get; }
        int ConfigMaxArtificialHealth { get; }
        bool ConfigloseArtificialHealth { get; }
        List<SerializedItem> ConfigItems { get; }
        int ConfigSpawnChance { get; }
        int ConfigRequiredPlayers { get; }
        string ConfigRoleName { get; }
        bool CongifShowTag { get; }
    }
}
