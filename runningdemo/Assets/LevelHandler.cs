using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    //Gets the monster prefab
    public GameObject monster;
    //Gets fastmonster prefab
    public GameObject fastmonster;
    //Gets the enemy prefab
    public GameObject enemy;

    //Sets the areas in which Monsters can spawn
    Vector3 minMonsterPosition = new Vector3(-9, -5, 0);
    Vector3 maxMonsterPosition = new Vector3(-1, 5, 0);

    //Sets the areas in which Monsters can spawn
    Vector3 minEnemyPosition = new Vector3(1, -5, 0);
    Vector3 maxEnemyPosition = new Vector3(9, 5, 0);
    
    //Sets the number of Monsters to spawn
    public int numMonstersToSpawn = MonsterList.monsterList.Count;
    public List<Vector3> monsterCoords;

    //Sets the number of Enemies to spawn
    public int numEnemiesToSpawn = 3;
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
        }
        else
        {
            spawnMonster(monsterInstance);
        }
    }

    //Spawns in an enemy at random coords within enemy area 
    public void spawnEnemy()
    {
        int enemyX = (int)Random.Range(minEnemyPosition.x, maxEnemyPosition.x);
        int enemyY = (int)Random.Range(minEnemyPosition.y, maxEnemyPosition.y);
        Vector3 enemyVector = new Vector3(enemyX, enemyY, 0);
        if (!enemyCoords.Contains(enemyVector) && !monsterCoords.Contains(enemyVector) && !wallCoords.Contains(enemyVector))
        {
            GameObject enemyInstance = Instantiate(enemy, enemyVector, Quaternion.identity);
            //livingEnemies.Add(enemyInstance);
            enemyCoords.Add(enemyVector);
        }
        else
        {
            spawnEnemy();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
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
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            spawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
