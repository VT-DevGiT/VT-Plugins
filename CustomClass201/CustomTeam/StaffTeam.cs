using Synapse;
using Synapse.Api;
using Synapse.Api.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VT_Referance.Variable;

namespace CustomClass.CustomTeam
{
    [SynapseTeamInformation(
        ID = (int)TeamID.ServerStaff,
        Name = name
        )]
    public class StaffTeam : SynapseTeam
    {
        const string name = "Staff";
        public override void Spawn(List<Player> players)
        {
            Server.Get.Logger.Warn($"you can't make the team spawn {name}");
        }
    }
}
