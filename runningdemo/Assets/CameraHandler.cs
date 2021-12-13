using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public bool viewingMap = false;
    public Camera cam;
    public void viewMap()
    {
        if (!viewingMap)
        {
            Debug.Log("Boop");
            cam.orthographicSize = 20;
            viewingMap = true;
        }
        else
        {
            cam.orthographicSize = 5;
            viewingMap = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!viewingMap)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Translate(Vector3.up);
                foreach (GameObject monster in TurnHandler.allies)
                {
                    monster.GetComponent<MonsterHandler>().monsterHealthBar.transform.position = monster.GetComponent<MonsterHandler>().monsterHealthBar.transform.position + Vector3.down;
                }
                foreach (GameObject enemy in TurnHandler.enemies)
                {
                    enemy.GetComponent<EnemyHandler>().enemyHealthBar.transform.position = enemy.GetComponent<EnemyHandler>().enemyHealthBar.transform.position + Vector3.down;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Translate(Vector3.left);
                foreach (GameObject monster in TurnHandler.allies)
                {
                    monster.GetComponent<MonsterHandler>().monsterHealthBar.transform.position = monster.GetComponent<MonsterHandler>().monsterHealthBar.transform.position + Vector3.right;
                }
                foreach (GameObject enemy in TurnHandler.enemies)
                {
                    enemy.GetComponent<EnemyHandler>().enemyHealthBar.transform.position = enemy.GetComponent<EnemyHandler>().enemyHealthBar.transform.position + Vector3.right;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.Translate(Vector3.down);
                foreach (GameObject monster in TurnHandler.allies)
                {
                    monster.GetComponent<MonsterHandler>().monsterHealthBar.transform.position = monster.GetComponent<MonsterHandler>().monsterHealthBar.transform.position + Vector3.up;
                }
                foreach (GameObject enemy in TurnHandler.enemies)
                {
                    enemy.GetComponent<EnemyHandler>().enemyHealthBar.transform.position = enemy.GetComponent<EnemyHandler>().enemyHealthBar.transform.position + Vector3.up;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Translate(Vector3.right);
                foreach (GameObject monster in TurnHandler.allies)
                {
                    monster.GetComponent<MonsterHandler>().monsterHealthBar.transform.position = monster.GetComponent<MonsterHandler>().monsterHealthBar.transform.position + Vector3.left;
                }
                foreach (GameObject enemy in TurnHandler.enemies)
                {
                    enemy.GetComponent<EnemyHandler>().enemyHealthBar.transform.position = enemy.GetComponent<EnemyHandler>().enemyHealthBar.transform.position + Vector3.left;
                }
            }
        }
    }
}
