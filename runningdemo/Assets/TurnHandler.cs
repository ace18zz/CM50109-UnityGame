using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnHandler : MonoBehaviour
{
    //Keeps track of whether it's the players turn or not
    public static bool isPlayerTurn = true;
    public static bool enemiesMoving = false;

    //Get all enemies
    public static List<GameObject> enemies; 
    public static List<GameObject> allies;

    public void playerTurn()
    {
        foreach (GameObject ally in allies)
        {
            ally.GetComponent<MonsterHandler>().currentMovement = ally.GetComponent<MonsterHandler>().maxMovement;
            ally.GetComponent<MonsterHandler>().currentActions = ally.GetComponent<MonsterHandler>().maxActions;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        enemies = new System.Collections.Generic.List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        allies = new System.Collections.Generic.List<GameObject>();
        allies.AddRange(GameObject.FindGameObjectsWithTag("Monster"));
    }

    // Update is called once per frame
    void Update()
    {
        //If player presses enter on their, move onto enemy turn and add to turn counter
        if (Input.GetKeyDown(KeyCode.Return) && isPlayerTurn)
        {
            isPlayerTurn = false;
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

    }
}
