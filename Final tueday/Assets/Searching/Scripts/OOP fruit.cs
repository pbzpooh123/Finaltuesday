using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{
    public class OOPfruit : Identity
    {
        public int healPoint = 20;
        
        public override void Hit()
        {
                mapGenerator.player.Energy(healPoint, false);
                mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty; 
                Destroy(gameObject);
        }
    }
}