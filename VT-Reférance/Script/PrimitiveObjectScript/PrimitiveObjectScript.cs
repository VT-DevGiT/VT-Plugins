using AdminToys;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VT_Referance.Script
{
    public abstract class PrimitiveObjectScript
    {

        public object Spawn()
        {
            foreach (GameObject gameObject in NetworkClient.prefabs.Values)
            {
                AdminToyBase component;
                if (gameObject.TryGetComponent<AdminToyBase>(out component))
                {
                    if (string.Equals(a, component.CommandName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        AdminToyBase adminToyBase = UnityEngine.Object.Instantiate<AdminToyBase>(component);
                        adminToyBase.OnSpawned(RefHub, arguments);
                        response = string.Format("Toy \"{0}\" placed! You can remove it by using \"DESTROYTOY {1}\" command.", (object)adminToyBase.CommandName, (object)adminToyBase.netId);
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
