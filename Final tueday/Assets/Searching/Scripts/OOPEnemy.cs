using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Searching
{
    public class OOPEnemy : Character
{
    public int experienceReward = 100; // Experience points awarded to the player when this enemy is defeated
    private bool stunned = false; // Flag to track if the enemy is stunned

    public void Start()
    {
        GetRemainEnergy();
    }

    public override void Hit()
    {
        if (!stunned) // Allow attack only if not stunned
        {
            mapGenerator.player.Attack(this);
            this.Attack(mapGenerator.player);
        }
    }

    public void Attack(OOPPlayer _player)
    {
        if (!stunned) // Prevent attack if stunned
        {
            _player.TakeDamage(AttackPoint);
        }
    }

    public void Stun()
    {
        stunned = true; // Set stunned to true
    }

    public void RandomMove()
    {
        if (stunned)
        {
            stunned = false; // Skip this turn and reset stunned flag
            return;
        }

        int toX = positionX;
        int toY = positionY;
        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                toY += 1; // up
                break;
            case 1:
                toY -= 1; // down 
                break;
            case 2:
                toX -= 1; // left
                break;
            case 3:
                toX += 1; // right
                break;
        }
        if (!HasPlacement(toX, toY))
        {
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            mapGenerator.enemies[positionX, positionY] = null;
            positionX = toX;
            positionY = toY;
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.enemy;
            mapGenerator.enemies[positionX, positionY] = this;
            transform.position = new Vector3(positionX, positionY, 0);
        }
    }

    protected override void CheckDead()
    {
        base.CheckDead();
        if (hp <= 0)
        {
            mapGenerator.player.GainExperience(experienceReward);
            mapGenerator.player.inventory.AddItem("Flesh");

            mapGenerator.enemies[positionX, positionY] = null;
            mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
            Destroy(gameObject);
        }
    }
}

}
