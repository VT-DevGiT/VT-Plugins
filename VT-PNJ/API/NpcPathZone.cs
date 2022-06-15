using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_PNJ.API
{
    public class NpcPathZone
    {
        #region Attributes & Properties
        public string Name { get; set; }
        public ZoneType Zone { get; set; }

        private List<NpcMapPointRoute> _NpcMapPoints = new List<NpcMapPointRoute>();

        public List<NpcMapPointRoute> Points { get { return _NpcMapPoints; } set { _NpcMapPoints = value; } }

        private List<KeyValuePair<uint, uint>> _chemins = new List<KeyValuePair<uint, uint>>();

        public List<KeyValuePair<uint, uint>> Chemins
        {
            get { return _chemins; }
            set { _chemins = value; }
        }

        #endregion


        #region Constructors & Destructor

        public NpcPathZone(string nom, ZoneType zone)
        {
            Name = nom;
            Zone = zone;
            NpcManager.Get.CheminsZones.Add(this);
        }

        #endregion

        #region Methods

        public void AddPath(uint beginning, uint end, bool reciprocal = true)
        {
            Chemins.Add(new KeyValuePair<uint, uint>(beginning, end));
            if (reciprocal)
                Chemins.Add(new KeyValuePair<uint, uint>(end, beginning));
        }

        private uint Distance(NpcMapPointRoute beginning, NpcMapPointRoute end)
        {
            return (uint)Vector3.Distance(beginning.Position, end.Position);
        }

        public uint Distance(List<NpcMapPointRoute> path)
        {
            uint resultat = 0;
            for (int i = 0; i < path.Count - 1; i++)
                resultat += Distance(path[i], path[i + 1]);
            return resultat;
        }

        public List<NpcMapPointRoute> ShorterPath(uint beginning, uint end)
        {
            var resultat = new List<List<NpcMapPointRoute>>();
            PathRecursive(beginning, end, new List<NpcMapPointRoute>(), resultat);

            if (!resultat.Any())
                return null;

            IOrderedEnumerable<List<NpcMapPointRoute>> orderBy = resultat.OrderBy(p => Distance(p));
            return orderBy.First();
        }

        private void PathRecursive(uint beginning, uint end, List<NpcMapPointRoute> alreadyPass, List<List<NpcMapPointRoute>> result)
        {
            var ptDepart = Points.First(p => p.Id == beginning);
            if (beginning == end)
            {
                alreadyPass.Add(ptDepart);
                result.Add(alreadyPass);
            }

            IEnumerable<KeyValuePair<uint, uint>> listPossible = Chemins.Where(p => p.Key == beginning && !alreadyPass.Any(pt => pt.Id == p.Value));
            if (listPossible == null || !listPossible.Any())
                return;

            foreach (var element in listPossible)
            {
                List<NpcMapPointRoute> chemin = new List<NpcMapPointRoute>();
                chemin.AddRange(alreadyPass);
                chemin.Add(ptDepart);
                PathRecursive(element.Value, end, chemin, result);
            }
        }
        #endregion
    }
}
