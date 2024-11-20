using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Searching
{
    public class armor : Identity
    {
        public override void Hit()
        {
            mapGenerator.player.inventory.AddItem("Armor");
            mapGenerator.player.EquipArmor(3);
            mapGenerator.Armor[positionX, positionY] = null;
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            Destroy(gameObject);
        }
    }
    
}

