using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
        public int armor;
        public GameObject atktime2item;
        public GameObject stunitem;
        public GameObject energyitem;
        
        
        [Header("Stat Levels")]
        public int hpLevel = 0;
        public int attackLevel = 0;
        public int manaLevel = 0;
        public int speedLevel = 0;

        [Header("Character State")]
        protected bool isAlive;
        protected bool isFreeze;
        
        [Header("Skill Level Sliders")]
        public Slider hpLevelSlider;
        public Slider attackLevelSlider;
        public Slider manaLevelSlider;
        public Slider speedLevelSlider;
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

        [Header("Enemy kill")] 
        public int Enemykill;
        
        
        
        // Start is called before the first frame update
        protected void GetRemainEnergy()
        {
            Debug.Log(Name + " : " + hp);
            Debug.Log(Name + " : " + energy);
            hpLevel = 0; 
            attackLevel = 0; 
            manaLevel = 0; 
            speedLevel = 0;
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
                else if (Isfruit(toX, toY))
                {
                    mapGenerator.fruitpos[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (Isarmor(toX, toY))
                {
                    mapGenerator.Armor[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (Issword(toX, toY))
                {
                    mapGenerator.Sword[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                }
                else if (Isatk(toX, toY))
                {
                    mapGenerator.atktime2s[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                    ShowatkPopup();
                }
                else if (Isheal(toX, toY))
                {
                    mapGenerator.itemheals[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                    ShowenergyPopup();
                }
                else if (Isstun(toX, toY))
                {
                    mapGenerator.itemstuns[toX, toY].Hit();
                    positionX = toX;
                    positionY = toY;
                    transform.position = new Vector3(positionX, positionY, 0);
                    ShowstunPopup();
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
                TakeEnergy(4);
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
        public bool Isfruit(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.fruit;
        }
        public bool Isarmor(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.armor;
        }
        public bool Issword(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.sword;
        }
        public bool Isatk(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.atktime2;
        }
        public bool Isheal(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.itemheal;
        }
        public bool Isstun(int x, int y)
        {
            int mapData = mapGenerator.GetMapData(x, y);
            return mapData == mapGenerator.itemstun;
        }


        public virtual void TakeDamage(int Damage)
        {
            int reducedDamage = Mathf.Max(0, Damage - armor); // Ensure damage doesn't go below 0
            hp -= reducedDamage;

            Debug.Log($"{Name} took {reducedDamage} damage after armor reduction. Current HP: {hp}");
            CheckDead();
            UpdateHPUI();
        }

        public virtual void TakeDamage(int Damage, bool freeze)
        {
            int reducedDamage = Mathf.Max(0, Damage - armor);
            hp -= reducedDamage;
            isFreeze = freeze;

            GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log($"{Name} took {reducedDamage} damage after armor reduction. Current HP: {hp}");
            Debug.Log("You are frozen.");
            CheckDead();
            UpdateHPUI();
        }
        
        public virtual void TakeDamage1(int Damage)
        {
            hp -= Damage;
            Debug.Log(Name + " Current Hp : " + hp);
            CheckDead();
        }
        
        public void UpdateSkillLevelUI()
        {
            hpLevelSlider.value = hpLevel;
            attackLevelSlider.value = attackLevel;
            manaLevelSlider.value = manaLevel;
            speedLevelSlider.value = speedLevel;
        }
        
        public virtual void TakeEnergy(int baseEnergyCost)
        {
            // Calculate the energy cost reduction based on the speed level
            float energyCostMultiplier = Mathf.Max(0.1f, 1f - (speedLevel * 0.3f)); // Minimum multiplier is 0.1 to avoid zero cost
            int adjustedEnergyCost = Mathf.CeilToInt(baseEnergyCost * energyCostMultiplier);

            Debug.Log("Base Energy Cost: " + baseEnergyCost);
            Debug.Log("Speed Level: " + speedLevel);
            Debug.Log("Multiplier: " + energyCostMultiplier);
            Debug.Log("Adjusted Energy Cost: " + adjustedEnergyCost);

            // Deduct adjusted energy cost
            energy -= adjustedEnergyCost;

            Debug.Log(Name + " Current Energy : " + energy);
            CheckDead();
            UpdateEnergyUI();
        }
        
        public void TakeMana(int baseEnergyCost)
        {

            // Deduct adjusted energy cost
            mana -= baseEnergyCost;
            UpdateManaUI();
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
            Debug.Log("Current hp : " + hp);
            UpdateHPUI();
        }
        
        public void Energy(int healPoint, bool Bonuse)
        {
            energy += healPoint * (Bonuse ? 2 : 1);
            Debug.Log("Current Energy : " + energy);
            UpdateEnergyUI();
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

            // Handle multiple level-ups if experience exceeds the threshold
            while (experience >= experienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            level += 1;
            experience -= experienceToNextLevel; // Deduct the required experience

            // Scale experience required for the next level
            experienceToNextLevel = Mathf.CeilToInt(experienceToNextLevel);

            // Show level-up pop-up
            ShowLevelUpPopup();
        }

        private void ShowLevelUpPopup()
        {
            levelUpPopup.SetActive(true);
            levelUpText.text = "Level Up! Level " + level;

            // Clear existing listeners to prevent stacking
            hpButton.onClick.RemoveAllListeners();
            atkButton.onClick.RemoveAllListeners();
            manaButton.onClick.RemoveAllListeners();
            speedButton.onClick.RemoveAllListeners();

            // Debug to ensure listeners are cleared
            Debug.Log("Listeners cleared.");

            // Assign each button to increase a specific stat
            hpButton.onClick.AddListener(() => { Debug.Log("HP button clicked."); IncreaseStat("HP"); });
            atkButton.onClick.AddListener(() => { Debug.Log("Attack button clicked."); IncreaseStat("Attack"); });
            manaButton.onClick.AddListener(() => { Debug.Log("Mana button clicked."); IncreaseStat("Mana"); });
            speedButton.onClick.AddListener(() => { Debug.Log("Speed button clicked."); IncreaseStat("Speed"); });
        }




        public void IncreaseStat(string stat)
        {
            Debug.Log("Increasing stat: " + stat);

            switch (stat)
            {
                case "HP":
                    hpLevel += 1;
                    hp += 10; // Scale HP based on its level
                    Debug.Log("HP increased to: " + hp);
                    break;

                case "Attack":
                    attackLevel += 1;
                    AttackPoint += 2; // Scale Attack based on its level
                    Debug.Log("Attack increased to: " + AttackPoint);
                    break;

                case "Mana":
                    manaLevel += 1;
                    mana += 5; // Scale Mana based on its level
                    Debug.Log("Mana increased to: " + mana);
                    break;

                case "Speed":
                    speedLevel += 1;
                    speed += 1; // Scale Speed based on its level
                    Debug.Log("Speed increased to: " + speed);
                    break;
            }

            // Update UI and hide pop-up
            UpdateSkillLevelUI();
            UpdateHPUI();
            UpdateATkUI();
            UpdateManaUI();
            levelUpPopup.SetActive(false);

            // Clear listeners to avoid stacking on next level-up
            hpButton.onClick.RemoveAllListeners();
            atkButton.onClick.RemoveAllListeners();
            manaButton.onClick.RemoveAllListeners();
            speedButton.onClick.RemoveAllListeners();
            
        }
        
        public void UpdateEnergyUI()
        {
            energyText.text = "Energy: " + energy;
        }
        public void UpdateHPUI()
        {
            hpText.text = "Hit point: " +  hp;
        }
        public void UpdateATkUI()
        {
            atkText.text = "Damage: " + AttackPoint;
        }
        public void UpdateManaUI()
        {
            manaText.text = "Mana: " + mana;
        }
        
        public void EquipArmor(int armorValue)
        {
            armor += armorValue; // Add armor value
            Debug.Log($"{Name} equipped armor. Current armor: {armor}");
        }
        
        public void ShowatkPopup()
        {
            atktime2item.SetActive(true);
        }

        // Hide the level-up popup
        public void HideatkPopup()
        {
            atktime2item.SetActive(false);
        }
    
        public void ShowstunPopup()
        {
            stunitem.SetActive(true);
        }

        // Hide the level-up popup
        public void HidestunPopup()
        {
            stunitem.SetActive(false);
        }
    
        public void ShowenergyPopup()
        {
            energyitem.SetActive(true);
        }

        // Hide the level-up popup
        public void HideenergyPopup()
        {
            energyitem.SetActive(false);
        }
    
    }
}