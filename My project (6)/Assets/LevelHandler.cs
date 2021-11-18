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

    //Keeps list of living enemies
    //public List<GameObject> livingEnemies;
    //public List<GameObject> deadEnemies;

    //Sets the areas in which Monsters can spawn
    Vector3 minMonsterPosition = new Vector3(-9, -5, 0);
    Vector3 maxMonsterPosition = new Vector3(-1, 5, 0);

    //Sets the areas in which Monsters can spawn
    Vector3 minEnemyPosition = new Vector3(1, -5, 0);
    Vector3 maxEnemyPosition = new Vector3(9, 5, 0);
    
    //Sets the number of Monsters to spawn
    public int numMonstersToSpawn = 3;
    public List<Vector3> monsterCoords;

    //Sets the number of Enemies to spawn
    public int numEnemiesToSpawn = 3;
    public List<Vector3> enemyCoords;

    //Spawns in a monster at random coords within monster area    
    public void spawnMonster()
    {
        int monsterX = (int)Random.Range(minMonsterPosition.x, maxMonsterPosition.x);
        int monsterY = (int)Random.Range(minMonsterPosition.y, maxMonsterPosition.y);
        Vector3 monsterVector = new Vector3(monsterX, monsterY, 0);
        if (!monsterCoords.Contains(monsterVector) && !enemyCoords.Contains(monsterVector))
        {
            Instantiate(monster, monsterVector, Quaternion.identity);
            monsterCoords.Add(monsterVector);
        }
        else
        {
            spawnMonster();
        }
    }

    //Spawns in an enemy at random coords within enemy area 
    public void spawnEnemy()
    {
        int enemyX = (int)Random.Range(minEnemyPosition.x, maxEnemyPosition.x);
        int enemyY = (int)Random.Range(minEnemyPosition.y, maxEnemyPosition.y);
        Vector3 enemyVector = new Vector3(enemyX, enemyY, 0);
        if (!enemyCoords.Contains(enemyVector) && !monsterCoords.Contains(enemyVector))
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
        //Spawns monsters in unnocupied spaces in given area
        for (int i = 0; i < numMonstersToSpawn; i++)
        {
            spawnMonster();
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
