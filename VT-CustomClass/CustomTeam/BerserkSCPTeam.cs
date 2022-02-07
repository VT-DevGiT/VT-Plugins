using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using System.Collections.Generic;
using VT_Api.Core.Enum;

namespace VTCustomClass.CustomTeam
{
    [SynapseTeamInformation(
        ID = (int)TeamID.BerserkSCP,
        Name = "BerserkSCP"
        )]
    public class BerserkSCPTeam : SynapseTeam
    {
        public override void Spawn(List<Player> players)
            => Server.Get.Logger.Warn($"you can't make the team spawn the BerserkSCP");
    }
}
