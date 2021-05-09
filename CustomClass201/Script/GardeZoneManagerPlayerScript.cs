﻿using Synapse;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class ZoneManagerScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;
        protected override List<int> EnemysList => new List<int> { (int)TeamID.CHI, (int)TeamID.SCP, (int)TeamID.CDP };

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : new List<int> { (int)TeamID.MTF, (int)TeamID.CDM, (int)TeamID.RSC, (int)TeamID.U2I };

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.ZoneManager;

        protected override string RoleName => PluginClass.ConfigGardeSuperviseur.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigGardeSuperviseur;
    }
}