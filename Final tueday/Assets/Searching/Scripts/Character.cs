using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;
using UnityEngine.UI;

namespace Searching
{
    public class Character : Identity
    {
        [Header("Character Stats")]
        public int level = 1;
        public int experience = 0;
        public int experienceToNextLevel = 100;
        public int energy;
        public int hp;
        public int AttackPoint;
        public int mana;
        public int speed;

        [Header("Character State")]
        protected bool isAlive;
        protected bool isFreeze;

        // UI references for level-up pop-up (assign in Inspector)
        public GameObject levelUpPopup;
        public TMP_Text levelUpText;
        public TMP_Text energyText;
        public TMP_Text hpText;
        public TMP_Text atkText;
        public TMP_Text manaText;
        public Button hpButton;
        public Button atkButton;
        public Button manaButton;
        public Button speedButton;
        
        // Start is called before the first frame update
        protected void GetRemainEnergy()
        {
            Debug.Log(Name + " : " + hp);
            Debug.Log(Name + " : " + energy);
        }

        public virtual void Move(Vector2 direction)
        {
            
            if (isFreeze == true)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                isFreeze = false;
                return;
            }
            int toX = (int)(positionX + direction.x);
            int toY = (int)(positionY + direction.y);
            int fromX = positionX;
            int fromY = positionY;

            if (HasPlacement(toX, toY))
            {
                if (IsDemonWalls(toX, toY))
                {
                    mapGenerator.walls[toX, toY].Hit();
                }
                else if (IsPotion(toX, toY))
                {
                    mapGenerator.potions[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsPotionBonus(toX, toY))
                {
                    mapGenerator.potions[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsExit(toX, toY))
                {
                    mapGenerator.Exit.Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsKey(toX, toY))
                {
                    mapGenerator.keys[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsFireStorm(toX, toY))
                {
                    mapGenerator.fireStorms[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsLvup(toX, toY))
                {
                    mapGenerator.lvup[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (IsEnemy(toX, toY))
                {
                    mapGenerator.enemies[toX, toY].Hit();
                }
            }
            else
            {
                positionX = toX;
                positionY = toY;
                transform.position = new Vector3(positionX, positionY, 0);
                Takenergy(4);
            }

            if (this is OOPPlayer)
            {
                if (fromX != positionX || fromY != positionY)
                {
                    mapGenerator.mapdata[fromX, fromY] = mapGenerator.empty;
                    mapGenerator.mapdata[positionX, positionY] = mapGenerator.playerBlock;
                    mapGenerator.MoveEnemies();
                }
            }

        }
        // hasPlacement คืนค่า true ถ้ามีการวางอะไรไว้บน map ที่ตำแหน่ง x,y
        public bool HasPlacement(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData != mapGenerator.empty;
        }
        public bool IsDemonWalls(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.demonWall;
        }
        public bool IsPotion(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.potion;
        }
        public bool IsPotionBonus(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.potion;
        }
        public bool IsFireStorm(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.fireStorm;
        }
        public bool IsKey(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.key;
        }
        public bool IsEnemy(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.enemy;
        }
        public bool IsExit(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.exit;
        }
        public bool IsLvup(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.levelup;
        }

        public virtual void TakeDamage(int Damage)
        {
            hp -= Damage;
            Debug.Log(Name + " Current Hp : " + hp);
            CheckDead();
        }
        public virtual void TakeDamage(int Damage, bool freeze)
        {
            energy -= Damage;
            isFreeze = freeze;
            GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log(Name + " Current Hp  : " + energy);
            Debug.Log("you is Freeze");
            CheckDead();
        }
        
        public virtual void Takenergy(int Damage)
        {
            energy -= Damage;
            Debug.Log(Name + " Current Energy : " + energy);
            CheckDead();
            UpdateEnergyUI();
        }


        public void Heal(int healPoint)
        {
            // energy += healPoint;
            // Debug.Log("Current Energy : " + energy);
            // เราสามารถเรียกใช้ฟังก์ชัน Heal โดยกำหนดให้ Bonuse = false ได้ เพื่อที่จะให้ logic ในการ heal อยู่ที่ฟังก์ชัน Heal อันเดียวและไม่ต้องเขียนซ้ำ
            Heal(healPoint, false);
        }

        public void Heal(int healPoint, bool Bonuse)
        {
            hp += healPoint * (Bonuse ? 2 : 1);
            Debug.Log("Current Energy : " + hp);
        }

        protected virtual void CheckDead()
        {
            if (energy <= 0)
            {
                Destroy(gameObject);
            }
            else if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        public void GainExperience(int amount)
        {
            experience += amount;
            if (experience >= experienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            level++;
            experience -= experienceToNextLevel;

            // Show level-up pop-up
            ShowLevelUpPopup();
        }

        private void ShowLevelUpPopup()
        {
            levelUpPopup.SetActive(true);
            levelUpText.text = "Level Up! Level " + level;

            // Assign each button to increase a specific stat
            hpButton.onClick.AddListener(() => IncreaseStat("HP"));
            atkButton.onClick.AddListener(() => IncreaseStat("Attack"));
            manaButton.onClick.AddListener(() => IncreaseStat("Mana"));
            speedButton.onClick.AddListener(() => IncreaseStat("Speed"));
        }

        public void IncreaseStat(string stat)
        {
            switch (stat)
            {
                case "HP":
                    hp += 10;
                    break;
                case "Attack":
                    AttackPoint += 2;
                    break;
                case "Mana":
                    mana += 5;
                    break;
                case "Speed":
                    speed += 1;
                    break;
            }

            // Hide level-up pop-up and remove listeners
            levelUpPopup.SetActive(false);
            hpButton.onClick.RemoveAllListeners();
            atkButton.onClick.RemoveAllListeners();
            manaButton.onClick.RemoveAllListeners();
            speedButton.onClick.RemoveAllListeners();
        }
        
        public void UpdateEnergyUI()
        {
            energyText.text = "Energy: " + energy.ToString();
        }
        public void UpdateHPUI()
        {
            energyText.text = "Energy: " + energy;
        }
        public void UpdateATkUI()
        {
            energyText.text = "Energy: " + energy;
        }
        public void UpdateManaUI()
        {
            energyText.text = "Energy: " + energy;
        }
    
    }
}