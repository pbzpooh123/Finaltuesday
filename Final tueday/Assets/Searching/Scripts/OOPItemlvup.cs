using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{ 
    public class OOPItemlvup : Identity
    {
        public override void Hit()
        {
            mapGenerator.player.GainExperience(100);
            mapGenerator.fireStorms[positionX, positionY] = null;
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            Destroy(gameObject);
        }
    }
}

