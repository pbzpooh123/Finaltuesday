using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{
    public class atktime2 : Identity
    {
        public override void Hit()
        {
            mapGenerator.player.inventory.AddItem("atktime2");
            mapGenerator.fireStorms[positionX, positionY] = null;
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            Destroy(gameObject);
        }
    }
}

