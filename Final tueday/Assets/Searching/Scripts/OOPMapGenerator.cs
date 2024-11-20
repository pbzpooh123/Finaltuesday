using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Searching
{

    public class OOPMapGenerator : MonoBehaviour
    {
        [Header("Set MapGenerator")]
        public int X;
        public int Y;

        [Header("Set Player")]
        public OOPPlayer player;
        public Vector2Int playerStartPos;

        [Header("Set Exit")]
        public OOPExit Exit;

        [Header("Set Prefab")]
        public GameObject[] floorsPrefab;
        public GameObject[] wallsPrefab;
        public GameObject[] demonWallsPrefab;
        public GameObject[] itemsPrefab;
        public GameObject[] lvPrefab;
        public GameObject[] keysPrefab;
        public GameObject[] enemiesPrefab;
        public GameObject[] fireStormPrefab;
        public GameObject[] fruitPrefab;
        public GameObject[] armorPrefab;
        public GameObject[] swordPrefab;
        public GameObject[] atkPrefab;
        public GameObject[] healPrefab;
        public GameObject[] stunPrefab;

        [Header("Set Transform")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemPotionParent;
        public Transform enemyParent;

        [Header("Set object Count")]
        public int obsatcleCount;
        public int itemPotionCount;
        public int itemKeyCount;
        public int itemFireStormCount;
        public int enemyCount;
        public int lvCount;
        public int fruitCount;
        public int armorCount;
        public int swordCount;
        public int atkCount;
        public int healCount;
        public int stunCount;
        

        public int[,] mapdata;

        public OOPWall[,] walls;
        public OOPItemPotion[,] potions;
        public OOPFireStormItem[,] fireStorms;
        public OOPItemKey[,] keys;
        public OOPEnemy[,] enemies;
        public OOPItemlvup[,] lvup;
        public OOPfruit[,] fruitpos;
        public armor[,] Armor;
        public sword[,] Sword;
        public atktime2[,] atktime2s;
        public itemheal[,] itemheals;
        public itemstun[,] itemstuns;


        // block types ...
        [Header("Block Types")]
        public int playerBlock = 99;
        public int empty = 0;
        public int demonWall = 1;
        public int potion = 2;
        public int bonuesPotion = 3;
        public int exit = 4;
        public int key = 5;
        public int enemy = 6;
        public int fireStorm = 7;
        public int levelup = 8;
        public int fruit = 9;
        public int armor = 10;
        public int sword = 11;
        public int atktime2 = 12;
        public int itemheal = 13;
        public int itemstun = 14;

        // Start is called before the first frame update
        void Start()
        {
            mapdata = new int[X, Y];
            for (int x = -1; x < X + 1; x++)
            {
                for (int y = -1; y < Y + 1; y++)
                {
                    if (x == -1 || x == X || y == -1 || y == Y)
                    {
                        int r = Random.Range(0, wallsPrefab.Length);
                        GameObject obj = Instantiate(wallsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
                        obj.transform.parent = wallParent;
                        obj.name = "Wall_" + x + ", " + y;
                    }
                    else
                    {
                        int r = Random.Range(0, floorsPrefab.Length);
                        GameObject obj = Instantiate(floorsPrefab[r], new Vector3(x, y, 1), Quaternion.identity);
                        obj.transform.parent = floorParent;
                        obj.name = "floor_" + x + ", " + y;
                    }
                }
            }

            player.mapGenerator = this;
            player.positionX = playerStartPos.x;
            player.positionY = playerStartPos.y;
            player.transform.position = new Vector3(playerStartPos.x, playerStartPos.y, -0.1f);
            mapdata[playerStartPos.x, playerStartPos.y] = playerBlock;

            walls = new OOPWall[X, Y];
            int count = 0;
            while (count < obsatcleCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == 0)
                {
                    PlaceDemonWall(x, y);
                    count++;
                }
            }

            potions = new OOPItemPotion[X, Y];
            count = 0;
            while (count < itemPotionCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    PlaceItem(x, y);
                    count++;
                }
            }

            keys = new OOPItemKey[X, Y];
            count = 0;
            while (count < itemKeyCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    PlaceKey(x, y);
                    count++;
                }
            }

            enemies = new OOPEnemy[X, Y];
            count = 0;
            while (count < enemyCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    PlaceEnemy(x, y);
                    count++;
                }
            }

            fireStorms = new OOPFireStormItem[X, Y];
            count = 0;
            while (count < itemFireStormCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    PlaceFireStorm(x, y);
                    count++;
                }
            }
            
            lvup = new OOPItemlvup[X, Y];
            count = 0;
            while (count < lvCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    Placelv(x, y);
                    count++;
                }
            }
            
            fruitpos = new OOPfruit[X, Y];
            count = 0;
            while (count < fruitCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    Placefruit(x, y);
                    count++;
                }
            }
            
            Armor = new armor[X, Y];
            count = 0;
            while (count < armorCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    Placearmor(x, y);
                    count++;
                }
            }
            
            Sword = new sword[X, Y];
            count = 0;
            while (count < swordCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    Placesword(x, y);
                    count++;
                }
            }
            
            atktime2s = new atktime2[X, Y];
            count = 0;
            while (count < atkCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    Placeatk(x, y);
                    count++;
                }
            }
            
            itemheals = new itemheal[X, Y];
            count = 0;
            while (count < healCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    Placeheal(x, y);
                    count++;
                }
            }
            
            itemstuns = new itemstun[X, Y];
            count = 0;
            while (count < stunCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    Placestun(x, y);
                    count++;
                }
            }

            mapdata[X - 1, Y - 1] = exit;
            Exit.transform.position = new Vector3(X - 1, Y - 1, 0);
        }

        public int GetMapData(float x, float y)
        {
            if (x >= X || x < 0 || y >= Y || y < 0) return -1;
            return mapdata[(int)x, (int)y];
        }

        public void PlaceItem(int x, int y)
        {
            int r = Random.Range(0, itemsPrefab.Length);
            GameObject obj = Instantiate(itemsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = potion;
            potions[x, y] = obj.GetComponent<OOPItemPotion>();
            potions[x, y].positionX = x;
            potions[x, y].positionY = y;
            potions[x, y].mapGenerator = this;
            obj.name = $"Item_{potions[x, y].Name} {x}, {y}";
        }
        

        public void PlaceKey(int x, int y)
        {
            int r = Random.Range(0, keysPrefab.Length);
            GameObject obj = Instantiate(keysPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = key;
            keys[x, y] = obj.GetComponent<OOPItemKey>();
            keys[x, y].positionX = x;
            keys[x, y].positionY = y;
            keys[x, y].mapGenerator = this;
            obj.name = $"Item_{keys[x, y].Name} {x}, {y}";
        }

        public void PlaceEnemy(int x, int y)
        {
            int r = Random.Range(0, enemiesPrefab.Length);
            GameObject obj = Instantiate(enemiesPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = enemy;
            enemies[x, y] = obj.GetComponent<OOPEnemy>();
            enemies[x, y].positionX = x;
            enemies[x, y].positionY = y;
            enemies[x, y].mapGenerator = this;
            obj.name = $"Enemy_{enemies[x, y].Name} {x}, {y}";
            
        }

        public void PlaceDemonWall(int x, int y)
        {
            int r = Random.Range(0, demonWallsPrefab.Length);
            GameObject obj = Instantiate(demonWallsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = wallParent;
            mapdata[x, y] = demonWall;
            walls[x, y] = obj.GetComponent<OOPWall>();
            walls[x, y].positionX = x;
            walls[x, y].positionY = y;
            walls[x, y].mapGenerator = this;
            obj.name = $"DemonWall_{walls[x, y].Name} {x}, {y}";
        }

        public void PlaceFireStorm(int x, int y)
        {
            int r = Random.Range(0, fireStormPrefab.Length);
            GameObject obj = Instantiate(fireStormPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = wallParent;
            mapdata[x, y] = fireStorm;
            fireStorms[x, y] = obj.GetComponent<OOPFireStormItem>();
            fireStorms[x, y].positionX = x;
            fireStorms[x, y].positionY = y;
            fireStorms[x, y].mapGenerator = this;
            obj.name = $"FireStorm_{fireStorms[x, y].Name} {x}, {y}";
        }
        
        public void Placelv(int x, int y)
        {
            int r = Random.Range(0, lvPrefab.Length);
            GameObject obj = Instantiate(lvPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = levelup;
            lvup[x, y] = obj.GetComponent<OOPItemlvup>(); 
            lvup[x, y].positionX = x;
            lvup[x, y].positionY = y;
            lvup[x, y].mapGenerator = this;
            obj.name = $"Item_{lvup[x, y].Name} {x}, {y}";
        }
        
        public void Placefruit(int x, int y)
        {
            int r = Random.Range(0, fruitPrefab.Length);
            GameObject obj = Instantiate(fruitPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = fruit;
            fruitpos[x, y] = obj.GetComponent<OOPfruit>(); 
            fruitpos[x, y].positionX = x;
            fruitpos[x, y].positionY = y;
            fruitpos[x, y].mapGenerator = this;
            obj.name = $"Item_{fruitpos[x, y].Name} {x}, {y}";
        }
        
        public void Placearmor(int x, int y)
        {
            int r = Random.Range(0, armorPrefab.Length);
            GameObject obj = Instantiate(armorPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = armor;
           Armor[x, y] = obj.GetComponent<armor>(); 
           Armor[x, y].positionX = x;
           Armor[x, y].positionY = y;
           Armor[x, y].mapGenerator = this;
            obj.name = $"Item_{Armor[x, y].Name} {x}, {y}";
        }
        public void Placesword(int x, int y)
        {
            int r = Random.Range(0, swordPrefab.Length);
            GameObject obj = Instantiate(swordPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = sword;
            Sword[x, y] = obj.GetComponent<sword>(); 
            Sword[x, y].positionX = x;
            Sword[x, y].positionY = y;
            Sword[x, y].mapGenerator = this;
            obj.name = $"Item_{Sword[x, y].Name} {x}, {y}";
        }
        
        public void Placeatk(int x, int y)
        {
            int r = Random.Range(0, atkPrefab.Length);
            GameObject obj = Instantiate(atkPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = atktime2;
            atktime2s[x, y] = obj.GetComponent<atktime2>(); 
            atktime2s[x, y].positionX = x;
            atktime2s[x, y].positionY = y;
            atktime2s[x, y].mapGenerator = this;
            obj.name = $"Item_{atktime2s[x, y].Name} {x}, {y}";
        }
        
        public void Placeheal(int x, int y)
        {
            int r = Random.Range(0, healPrefab.Length);
            GameObject obj = Instantiate(healPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = itemheal;
            itemheals[x, y] = obj.GetComponent<itemheal>(); 
            itemheals[x, y].positionX = x;
            itemheals[x, y].positionY = y;
            itemheals[x, y].mapGenerator = this;
            obj.name = $"Item_{itemheals[x, y].Name} {x}, {y}";
        }
        
        public void Placestun(int x, int y)
        {
            int r = Random.Range(0, stunPrefab.Length);
            GameObject obj = Instantiate(stunPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = itemstun;
            itemstuns[x, y] = obj.GetComponent<itemstun>(); 
            itemstuns[x, y].positionX = x;
            itemstuns[x, y].positionY = y;
            itemstuns[x, y].mapGenerator = this;
            obj.name = $"Item_{itemstuns[x, y].Name} {x}, {y}";
        }

        public OOPEnemy[] GetEnemies()
        {
            List<OOPEnemy> list = new List<OOPEnemy>();
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    list.Add(enemy);
                }
            }
            return list.ToArray();
        }

        public void MoveEnemies()
        {
            List<OOPEnemy> list = new List<OOPEnemy>();
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    list.Add(enemy);
                }
            }
            foreach (var enemy in list)
            {
                enemy.RandomMove();
            }
        }
    }
}