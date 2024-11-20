using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{
    public class itemstun : Identity
    {
        public override void Hit()
        {
            mapGenerator.player.inventory.AddItem("itemstun");
            mapGenerator.fireStorms[positionX, positionY] = null;
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            Destroy(gameObject);
        }
    }
}
