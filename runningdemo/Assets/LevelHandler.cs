using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    //Gets the monster prefab
    public GameObject monster;
    //Gets spider prefab
    public GameObject spider;
    //Gets the werewolf prefab
    public GameObject werewolf;
    //Gets the slime prefab
    public GameObject slime;
    //Gets the kangaroo prefab
    public GameObject kangaroo;
    //Gets the dragon prefab
    public GameObject dragon;

    //Sets the areas in which Monsters can spawn
    public Vector3 minMonsterPosition;
    public Vector3 maxMonsterPosition;

    //Sets the areas in which Monsters can spawn
    public Vector3 minEnemyPosition1;
    public Vector3 maxEnemyPosition1;

    public Vector3 minEnemyPosition2;
    public Vector3 maxEnemyPosition2;

    public Vector3 minEnemyPosition3;
    public Vector3 maxEnemyPosition3;

    //Sets the number of Monsters to spawn
    public List<Vector3> monsterCoords;

    //Sets the number of Enemies to spawn
    public int numWerewolf;
    public int numSpider;
    public int numSlime;
    public int numKangaroo;
    public int numDragon;

    public List<Vector3> enemyCoords;

    //Keeps track of walls
    public static List<GameObject> walls;
    List<Vector2> wallCoords = new List<Vector2>();

    //Spawns in a monster at random coords within monster area    
    public void spawnMonster(GameObject monsterInstance)
    {
        int monsterX = (int)Random.Range(minMonsterPosition.x, maxMonsterPosition.x);
        int monsterY = (int)Random.Range(minMonsterPosition.y, maxMonsterPosition.y);
        Vector3 monsterVector = new Vector3(monsterX, monsterY, 0);
        if (!monsterCoords.Contains(monsterVector) && !enemyCoords.Contains(monsterVector) && !wallCoords.Contains(monsterVector))
        {
            monsterInstance.transform.position = monsterVector;
            monsterCoords.Add(monsterVector);
            monsterInstance.GetComponent<MonsterHandler>().createHealthUI();
        }
        else
        {
            spawnMonster(monsterInstance);
        }
    }

    //Spawns in an enemy at random coords within enemy area 
    public void spawnEnemy(GameObject enemyType)
    {
        int enemyX;
        int enemyY;

        int spawnSeed = (int)Random.Range(1, 4);

        if (spawnSeed == 1)
        {
            enemyX = (int)Random.Range(minEnemyPosition1.x, maxEnemyPosition1.x);
            enemyY = (int)Random.Range(minEnemyPosition1.y, maxEnemyPosition1.y);
        }
        else if (spawnSeed == 2)
        {
            enemyX = (int)Random.Range(minEnemyPosition2.x, maxEnemyPosition2.x);
            enemyY = (int)Random.Range(minEnemyPosition2.y, maxEnemyPosition2.y);
        }
        else
        {
            enemyX = (int)Random.Range(minEnemyPosition3.x, maxEnemyPosition3.x);
            enemyY = (int)Random.Range(minEnemyPosition3.y, maxEnemyPosition3.y);
        }


        Vector3 enemyVector = new Vector3(enemyX, enemyY, 0);
        if (!enemyCoords.Contains(enemyVector) && !monsterCoords.Contains(enemyVector) && !wallCoords.Contains(enemyVector))
        {
            GameObject enemyInstance = Instantiate(enemyType, enemyVector, Quaternion.identity);
            //livingEnemies.Add(enemyInstance);
            enemyCoords.Add(enemyVector);
            enemyInstance.GetComponent<EnemyHandler>().createHealthUI();
        }
        else
        {
            spawnEnemy(enemyType);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //checks level then changes numEnemy values
        int numWerewolfEasy = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numWerewolf * 0.5f);
        int numSpiderEasy = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numSpider * 0.5f);
        int numSlimeEasy = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numSlime * 0.5f);
        int numKangarooEasy = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numKangaroo * 0.5f);
        int numDragonEasy = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numDragon * 0.5f);

        int numWerewolfHard = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numWerewolf * 2f);
        int numSpiderHard = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numSpider * 2f);
        int numSlimeHard = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numSlime * 2f);
        int numKangarooHard = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numKangaroo * 2f);
        int numDragonHard = (int)Mathf.Ceil(GameObject.Find("LevelHandler").GetComponent<LevelHandler>().numDragon * 2f);

        //Gets wall locations
        walls = new System.Collections.Generic.List<GameObject>();
        walls.AddRange(GameObject.FindGameObjectsWithTag("UnpassableTerrain"));

        foreach (GameObject wall in walls)
        {
            int height = (int)wall.GetComponent<Renderer>().bounds.size.y;
            int width = (int)wall.GetComponent<Renderer>().bounds.size.x;

            float halfheight = height / 2;
            float halfwidth = width / 2;

            if (width % 2 == 0)
            {
                halfwidth = halfwidth - 0.5f;
            }

            if (height % 2 == 0)
            {
                halfheight = halfheight - 0.5f;
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 posComponent = new Vector2(wall.transform.position.x - halfwidth + i, wall.transform.position.y - halfheight + j);
                    wallCoords.Add(posComponent);
                }
            }
        }

        //Spawns monsters in unnocupied spaces in given area
        foreach (GameObject currentMonster in MonsterList.monsterList){
            if (currentMonster != null)
            {
                spawnMonster(currentMonster);
            }
		}
        
        //Spawns enemies in unnocupied spaces in given area
        if (Difficulty.difficulty == 0)
        {
            for (int i = 0; i < numWerewolfEasy; i++)
            {
                spawnEnemy(werewolf);
            }
            for (int i = 0; i < numSpiderEasy; i++)
            {
                spawnEnemy(spider);
            }
            for (int i = 0; i < numSlimeEasy; i++)
            {
                spawnEnemy(slime);
            }
            for (int i = 0; i < numKangarooEasy; i++)
            {
                spawnEnemy(kangaroo);
            }
            for (int i = 0; i < numDragonEasy; i++)
            {
                spawnEnemy(dragon);
            }
        }
        else if (Difficulty.difficulty == 2)
        {
            for (int i = 0; i < numWerewolfHard; i++)
            {
                spawnEnemy(werewolf);
            }
            for (int i = 0; i < numSpiderHard; i++)
            {
                spawnEnemy(spider);
            }
            for (int i = 0; i < numSlimeHard; i++)
            {
                spawnEnemy(slime);
            }
            for (int i = 0; i < numKangarooHard; i++)
            {
                spawnEnemy(kangaroo);
            }
            for (int i = 0; i < numDragonHard; i++)
            {
                spawnEnemy(dragon);
            }
        }
        else
        {
            for (int i = 0; i < numWerewolf; i++)
            {
                spawnEnemy(werewolf);
            }
            for (int i = 0; i < numSpider; i++)
            {
                spawnEnemy(spider);
            }
            for (int i = 0; i < numSlime; i++)
            {
                spawnEnemy(slime);
            }
            for (int i = 0; i < numKangaroo; i++)
            {
                spawnEnemy(kangaroo);
            }
            for (int i = 0; i < numDragon; i++)
            {
                spawnEnemy(dragon);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
