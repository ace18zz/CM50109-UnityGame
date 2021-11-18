using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    //Keeps track of whether it's the players turn or not
    public static bool isPlayerTurn = true;

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
        if (!isPlayerTurn)
        {
            foreach(GameObject enemy in enemies)
            {
                int movement = enemy.GetComponent<EnemyHandler>().maxMovement;
                for(int i = 0; i < movement; i++)
                {
                    enemy.GetComponent<EnemyHandler>().makeMove();
                }
                int actions = enemy.GetComponent<EnemyHandler>().maxActions;
                for (int i = 0; i < actions; i++)
                {
                    enemy.GetComponent<EnemyHandler>().makeAttack();
                }
            }
            playerTurn();
            isPlayerTurn = true;
            TurnCounterUI.turnNumber++;
        }

    }
}
