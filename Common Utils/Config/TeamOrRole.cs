using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Utiles.Config
{
    public struct TeamOrRole
    {
        public TeamOrRole(int id)
        {
            this.isRoleID = true;
            this.id = id;
        }

        public TeamOrRole(bool isRoleID, int id)
        {
            this.isRoleID = isRoleID;
            this.id = id;
        }

        public bool isRoleID;

        public int id;
    }
}
