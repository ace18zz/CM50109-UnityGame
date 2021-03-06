using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnHandler : MonoBehaviour
{
    //Keeps track of whether it's the players turn or not
    public static bool isPlayerTurn = true;
    public static bool enemiesMoving = false;
    public static bool enemyTurnOver = false;

    //Get all enemies
    public static List<GameObject> enemies; 
    public static List<GameObject> allies;

    //Find camera
    GameObject cam;

    public void playerTurn()
    {
        foreach (GameObject ally in allies)
        {
            ally.GetComponent<MonsterHandler>().currentMovement = ally.GetComponent<MonsterHandler>().maxMovement - ally.GetComponent<MonsterHandler>().slowStacks;
            ally.GetComponent<MonsterHandler>().slowStacks = 0;
            ally.GetComponent<MonsterHandler>().currentActions = ally.GetComponent<MonsterHandler>().maxActions;
            if (ally.GetComponent<MonsterHandler>().isPoisoned)
            {
                ally.GetComponent<MonsterHandler>().monsterHealth = ally.GetComponent<MonsterHandler>().monsterHealth - 5;
                GameObject.Find("Combat Log").GetComponent<Text>().text = "Your monster took 5 damage from poison! It now has " + ally.GetComponent<MonsterHandler>().monsterHealth + " health remaining!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new System.Collections.Generic.List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        allies = new System.Collections.Generic.List<GameObject>();
        allies.AddRange(GameObject.FindGameObjectsWithTag("Monster"));

        cam = GameObject.Find("Main Camera");
        cam.transform.position = allies[0].transform.position + new Vector3(0f,0f,-10f);
        playerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject ally in allies)
        {
            ally.GetComponent<MonsterHandler>().updateHealthUI();
        }
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHandler>().updateHealthUI();
        }

        //If player presses enter on their, move onto enemy turn and add to turn counter
        if (Input.GetKeyDown(KeyCode.Return) && isPlayerTurn && !GameMenuHandler.isInMenu)
        {
            isPlayerTurn = false;
            foreach (GameObject ally in allies)
            {
                ally.GetComponent<MonsterHandler>().deselectMonster();
            }
        }

        //Carrying out enemy turns
        if (!isPlayerTurn && !enemiesMoving)
        {
            foreach(GameObject enemy in enemies)
            {
                StartCoroutine(enemy.GetComponent<EnemyHandler>().makeMove());
            }
            TurnCounterUI.turnNumber++;
        }
        if (enemies.Count == 0)
        {
            foreach (GameObject ally in allies)
            {
                ally.GetComponent<MonsterHandler>().removeFromScreen();
                //ally.GetComponent<MonsterHandler>().die();
                ally.GetComponent<MonsterHandler>().reset();
            }
            SceneManager.LoadScene("Scenes/Victory", LoadSceneMode.Single);
        }
        else if (allies.Count == 0)
        {
            SceneManager.LoadScene("Scenes/Defeat", LoadSceneMode.Single);
        }
        if (enemyTurnOver)
        {
            playerTurn();
            enemyTurnOver = false;
        }
    }
}
