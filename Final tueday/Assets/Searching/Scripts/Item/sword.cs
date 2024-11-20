using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{
    public class sword : Identity
    {
        public override void Hit()
        {
            mapGenerator.player.inventory.AddItem("sword");
            mapGenerator.player.AttackPoint += 3;
            mapGenerator.Sword[positionX, positionY] = null;
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            Destroy(gameObject);
        }
    }
}

