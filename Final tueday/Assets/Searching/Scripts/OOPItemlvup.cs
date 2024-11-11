using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{

    public class OOPItemlvup : Identity
    {
        public int experienceReward = 100;

        public override void Hit()
        {
            Debug.Log("Hit detected on OOPItemlvup");
            mapGenerator.player.GainExperience(experienceReward);
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            Destroy(gameObject);
        }
    }
}

