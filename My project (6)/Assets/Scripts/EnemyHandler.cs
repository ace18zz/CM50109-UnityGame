using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    //Enemy's health and damage
    public int enemyHealth = 20;
    public int enemyDamage = 5;

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
    public void makeMove()
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
        if(Math.Abs(xDiff) > Math.Abs(yDiff))
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
        else if (Math.Abs(xDiff) <= Math.Abs(yDiff))
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
            closestMonster.GetComponent<MonsterHandler>().monsterHealth = closestMonster.GetComponent<MonsterHandler>().monsterHealth - enemyDamage;
            Debug.Log("An enemy hit your monster for " + enemyDamage + " damage! It now has " + closestMonster.GetComponent<MonsterHandler>().monsterHealth + " health remaining!");
        }
    }
    
    //Removes the enemy from the game
    public void die()
    {
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
