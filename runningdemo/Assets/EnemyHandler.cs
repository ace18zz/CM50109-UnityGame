using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyHandler : MonoBehaviour
{
    //Enemy's health
    public int enemyHealth = 20;

    //Enemy's damage
    public int enemyDamage()
    {
        int damage = (int)Random.Range(0, 5);
        return damage;
    }

    //Enemy's movement
    public int maxMovement = 3;

    //Enemy's actions
    public int maxActions = 1;

    //Enemy color
    public Color enemyColor;

    public void moveLeft()
    {
        if (!isSpaceOccupiedByEnemy(transform.position + Vector3.left) && !isSpaceOccupiedByAlly(transform.position + Vector3.left))
        {
            transform.Translate(Vector3.left);
        }
    }

    //Move to the right one tile
    public void moveRight()
    {
        if (!isSpaceOccupiedByEnemy(transform.position + Vector3.right) && !isSpaceOccupiedByAlly(transform.position + Vector3.right))
        {
            transform.Translate(Vector3.right);
        }
    }

    //Move up one tile
    public void moveUp()
    {
        if (!isSpaceOccupiedByEnemy(transform.position + Vector3.up) && !isSpaceOccupiedByAlly(transform.position + Vector3.up))
        {
            transform.Translate(Vector3.up);
        }
    }

    //Move down one tile
    public void moveDown()
    {
        if (!isSpaceOccupiedByEnemy(transform.position + Vector3.down) && !isSpaceOccupiedByAlly(transform.position + Vector3.down))
        {
            transform.Translate(Vector3.down);
        }
    }

    public bool isSpaceOccupiedByAlly(Vector3 destination)
    {
        bool isOccupied = false;

        foreach (GameObject ally in TurnHandler.allies)
        {
            if (ally.transform.position == destination)
            {
                isOccupied = true;
            }
        }

        return isOccupied;
    }

    public bool isSpaceOccupiedByEnemy(Vector3 destination)
    {
        bool isOccupied = false;

        foreach (GameObject enemy in TurnHandler.enemies)
        {
            if (enemy.transform.position == destination)
            {
                isOccupied = true;
            }
        }

        return isOccupied;
    }
    
    //Enemy makes a move towards monster
    public IEnumerator makeMove()
    {
        TurnHandler.enemiesMoving = true;
        for (int i = 0; i < maxMovement; i++)
        {
            GameObject closestMonster = TurnHandler.allies[0];

            //Scans through all allies
            foreach (GameObject ally in TurnHandler.allies)
            {
                //Checks if monster is closer than the previous closest monster
                if (Vector3.Distance(transform.position, ally.transform.position) < Vector3.Distance(transform.position, closestMonster.transform.position))
                {
                    closestMonster = ally;
                }
            }

            int xDiff = (int)closestMonster.transform.position.x - (int)transform.position.x;
            int yDiff = (int)closestMonster.transform.position.y - (int)transform.position.y;

            //Basically it checks if distancs is further on x or y axis then moves one tile on that axis towards the monster
            if (Math.Abs(xDiff) > Math.Abs(yDiff))
            {
                if (xDiff > 0)
                {
                    moveRight();
                }
                else if (xDiff < 0)
                {
                    moveLeft();
                }
            }
            else if (Math.Abs(xDiff) < Math.Abs(yDiff))
            {
                if (yDiff > 0)
                {
                    moveUp();
                }
                else if (yDiff < 0)
                {
                    moveDown();
                }
            }
            //If equidistant along x and y
            else if (Math.Abs(xDiff) == Math.Abs(yDiff))
            {
                if (yDiff < 0 && xDiff < 0)
                {
                    if (!isSpaceOccupiedByEnemy(transform.position + Vector3.left))
                    {
                        moveLeft();
                    }
                    else
                    {
                        moveDown();
                    }
                }
                else if (yDiff > 0 && xDiff < 0)
                {
                    if (!isSpaceOccupiedByEnemy(transform.position + Vector3.left))
                    {
                        moveLeft();
                    }
                    else
                    {
                        moveUp();
                    }
                }
                else if (yDiff < 0 && xDiff > 0)
                {
                    if (!isSpaceOccupiedByEnemy(transform.position + Vector3.right))
                    {
                        moveRight();
                    }
                    else
                    {
                        moveDown();
                    }
                }
                else
                {
                    if (!isSpaceOccupiedByEnemy(transform.position + Vector3.right))
                    {
                        moveRight();
                    }
                    else
                    {
                        moveUp();
                    }
                }
            }
            yield return new WaitForSeconds(0.33f);

        }

        for (int i = 0; i < maxActions; i++)
        {
            makeAttack();
        }
        GameObject.Find("LevelHandler").GetComponent<TurnHandler>().playerTurn();
        TurnHandler.enemiesMoving = false;
        TurnHandler.isPlayerTurn = true;
    }

    //Enemy makes an attack
    public void makeAttack()
    {
        GameObject closestMonster = TurnHandler.allies[0];

        //Scans through all allies
        foreach (GameObject ally in TurnHandler.allies)
        {
            //Checks if monster is closer than the previous closest monster
            if (Vector3.Distance(transform.position, ally.transform.position) < Vector3.Distance(transform.position, closestMonster.transform.position))
            {
                closestMonster = ally;
            }
        }

        int xDiff = (int)closestMonster.transform.position.x - (int)transform.position.x;
        int yDiff = (int)closestMonster.transform.position.y - (int)transform.position.y;

        if (xDiff == 0 && Math.Abs(yDiff) == 1 || Math.Abs(xDiff) == 1 && yDiff == 0)
        {
            closestMonster.GetComponent<MonsterHandler>().monsterHealth = closestMonster.GetComponent<MonsterHandler>().monsterHealth - enemyDamage();
            GameObject.Find("Combat Log").GetComponent<Text>().text =  "An enemy hit your monster for " + enemyDamage() + " damage! It now has " + closestMonster.GetComponent<MonsterHandler>().monsterHealth + " health remaining!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
        }
    }
    
    //Drops enemy loot
    public void dropLoot()
    {
        int lootDrop = (int)Random.Range(0, 100);

        if (lootDrop < 40)
        { 
            //do nothing
        } 
        else if (lootDrop < 75)
        {
            Inventory.playerInventory.Add(new ItemHandler.Item("Werewolf Fur", Resources.Load<Sprite>("Werewolf Fur")));
        }
        else if (lootDrop < 95)
        {
            Inventory.playerInventory.Add(new ItemHandler.Item("Werewolf Teeth", Resources.Load<Sprite>("Werewolf Teeth")));
        }
        else if (lootDrop < 100)
        {
            Inventory.playerInventory.Add(new ItemHandler.Item("Werewolf Paw", Resources.Load<Sprite>("Werewolf Paw")));
        }
    }
    
    //Removes the enemy from the game
    public void die()
    {
        dropLoot();
        TurnHandler.enemies.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //If it's health is zero, it dies
        if (enemyHealth <= 0)
        {
            die();
        }
    }
}
