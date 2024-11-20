using System;
using System.Collections;
using System.Collections.Generic;
using Tree;
using UnityEngine;
using UnityEngine.UIElements;

namespace Searching
{
    public class OOPPlayer : Character
    {
        public Inventory inventory;

        public void Start()
        {
            PrintInfo();
            GetRemainEnergy();
            UpdateEnergyUI();
            UpdateHPUI();
            UpdateATkUI();
            UpdateManaUI();
            UpdateSkillLevelUI();
        }

        public void Update()
        {
            UpdateATkUI();
            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(Vector2.up);
                
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2.down);
                
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2.left);
                
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2.right);
              
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UseFireStorm();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Useatktime2();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Useheal();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Usestun();
            }
        }

        public void Attack(OOPEnemy _enemy)
        {
            _enemy.TakeDamage1(AttackPoint);
        }

        protected override void CheckDead()
        {
            base.CheckDead();
            if (energy <= 0)
            {
                Debug.Log("Player is Dead");
            }
        }

        public void UseFireStorm()
        {
            if (inventory.numberOfItem("FireStorm") > 0)
            {
                inventory.UseItem("FireStorm");
                OOPEnemy[] enemies = SortEnemiesByRemainningEnergy2();
                int count = 3;
                if (count > enemies.Length)
                {
                    count = enemies.Length;
                }
                for (int i = 0; i < count; i++)
                {
                    enemies[i].TakeDamage(10);
                }
            }
            else
            {
                Debug.Log("No FireStorm in inventory");
            }
        }

        public OOPEnemy[] SortEnemiesByRemainningEnergy1()
        {
            // do selection sort of enemy's energy
            var enemies = mapGenerator.GetEnemies();
            for (int i = 0; i < enemies.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < enemies.Length; j++)
                {
                    if (enemies[j].energy < enemies[minIndex].energy)
                    {
                        minIndex = j;
                    }
                }
                var temp = enemies[i];
                enemies[i] = enemies[minIndex];
                enemies[minIndex] = temp;
            }
            return enemies;
        }

        public OOPEnemy[] SortEnemiesByRemainningEnergy2()
        {
            var enemies = mapGenerator.GetEnemies();
            Array.Sort(enemies, (a, b) => a.energy.CompareTo(b.energy));
            return enemies;
        }
        
       public void Useatktime2()
{
    int manaCost = 5;
    if (inventory.numberOfItem("atktime2") > 0)
    {
        if (mapGenerator.player.mana >= manaCost)
        {
            inventory.UseItem("atktime2");
            mapGenerator.player.AttackPoint *= 2;
            mapGenerator.player.TakeMana(manaCost);
            UpdateATkUI();
        }
        else
        {
            Debug.Log("Not enough mana to use atktime2!");
        }
    }
    else
    {
        Debug.Log("No atktime2 item in inventory!");
    }
}

public void Useheal()
{
    int manaCost = 2;
    if (inventory.numberOfItem("itemheal") > 0)
    {
        if (mapGenerator.player.mana >= manaCost)
        {
            mapGenerator.player.Energy(30, false);
            mapGenerator.player.TakeMana(manaCost);
            UpdateEnergyUI();
        }
        else
        {
            Debug.Log("Not enough mana to use heal!");
        }
    }
    else
    {
        Debug.Log("No heal item in inventory!");
    }
}

public void Usestun()
{
    int manaCost = 5;
    if (inventory.numberOfItem("itemstun") > 0)
    {
        if (mapGenerator.player.mana >= manaCost)
        {
            inventory.UseItem("itemstun");
            mapGenerator.player.TakeMana(manaCost);
            OOPEnemy[] enemies = mapGenerator.GetEnemies();

            if (enemies.Length > 0)
            {
                OOPEnemy enemy = enemies[0]; // Stun the first enemy (or choose based on logic)
                enemy.Stun();
                Debug.Log("Enemy stunned!");
            }
            else
            {
                Debug.Log("No enemies to stun!");
            }
        }
        else
        {
            Debug.Log("Not enough mana to use stun!");
        }
    }
    else
    {
        Debug.Log("No stun item in inventory!");
    }
}



    }

}