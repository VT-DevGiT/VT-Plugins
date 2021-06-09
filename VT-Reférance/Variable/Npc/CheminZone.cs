using Synapse.Api.Enum;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VT_Referance.Variable.Npc
{
    public class CheminZone
    {
        #region Attributes & Properties

        public static List<CheminZone> CheminsZones { get; internal set; } = new List<CheminZone>();
        
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

        public CheminZone(string nom, ZoneType zone)
        {
            Name = nom;
            Zone = zone;
            CheminsZones.Add(this);
        }

        #endregion

        #region Methods

        public static void Clear()
        {
            foreach(var instance in CheminsZones)
            {
                instance.Points.Clear();
                instance.Chemins.Clear();
            }
        }

        public void AddChemin(uint depart, uint arivee, bool doubleSens = true)
        {
            Chemins.Add(new KeyValuePair<uint, uint>(depart, arivee));
            if (doubleSens)
            {
                Chemins.Add(new KeyValuePair<uint, uint>(arivee, depart));
            }
        }



        private uint Distance(NpcMapPointRoute depart, NpcMapPointRoute arivee)
        {
            return (uint)Vector3.Distance(depart.Position, arivee.Position);
        }

        public uint Distance(List<NpcMapPointRoute> chemin)
        {
            uint resultat = 0;
            for (int i = 0; i < chemin.Count - 1; i++)
                resultat += Distance(chemin[i], chemin[i + 1]);
            return resultat;
        }

        public List<NpcMapPointRoute> PlusCourtChemin(uint depart, uint arivee)
        {
            var resultat = new List<List<NpcMapPointRoute>>();
            CheminRecursif(depart, arivee, new List<NpcMapPointRoute>(), resultat);

            // string str = "";
            if (resultat == null || !resultat.Any())
                return null;

            IOrderedEnumerable<List<NpcMapPointRoute>> orderBy = resultat.OrderBy(p => Distance(p));
            return orderBy.First();

            /*foreach (List<NpcMapPoint> chemin in orderBy)
            {
                foreach (var pt in chemin)
                {
                   str += $",{pt.Id}";
                }
                str += $" distance = {Distance(chemin)}\n";
            }
            return str;*/
        }



        private void CheminRecursif(uint depart, uint arivee, List<NpcMapPointRoute> dejaPasse, List<List<NpcMapPointRoute>> resultat)
        {
            var ptDepart = Points.First(p => p.Id == depart);
            if (depart == arivee)
            {
                dejaPasse.Add(ptDepart);
                resultat.Add(dejaPasse);
            }

            IEnumerable<KeyValuePair<uint, uint>> listPossible = Chemins.Where(p => p.Key == depart && !dejaPasse.Any(pt => pt.Id == p.Value));
            if (listPossible == null || !listPossible.Any())
                return;

            foreach (var element in listPossible)
            {
                List<NpcMapPointRoute> chemin = new List<NpcMapPointRoute>();
                chemin.AddRange(dejaPasse);
                chemin.Add(ptDepart);
                CheminRecursif(element.Value, arivee, chemin, resultat);
            }
        }
        #endregion
    }
}
