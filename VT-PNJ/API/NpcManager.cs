using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_PNJ.API
{
    public class NpcManager
    {
        public static NpcManager Get { get; private set; }

        public List<NpcPathZone> CheminsZones { get; internal set; } = new List<NpcPathZone>();


        private NpcManager()
        {
            Get = this;
        }

        public void ClearChemins()
        {
            foreach (var instance in CheminsZones)
            {
                instance.Points.Clear();
                instance.Chemins.Clear();
            }
        }

    }
}
