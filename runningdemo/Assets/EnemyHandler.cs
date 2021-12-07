using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyHandler : MonoBehaviour
{
<<<<<<< HEAD
    //Enemy's health
=======
    //Enemy type
    public string enemyType;

    //Enemy's health and damage
>>>>>>> origin/Fei
    public int enemyHealth = 20;

    //Enemy's damage
    public int enemyDamage()
    {
        int damage = (int)Random.Range(0, 5);
        return damage;
    }

    //Enemy's movement
    public int maxMovement = 3;
    public int currentMovement = 3;

    //Enemy's actions
    public int maxActions = 1;

    //Keeps track of whether enemy movement is slowed
    public int slowStacks = 0;

    //Is the enemy poisoned?
    public bool isPoisoned = false;

    //Enemy color
    public Color enemyColor;

    public void moveLeft()
    {
        if (!isSpaceOccupiedByEnemy(transform.position + Vector3.left) && !isSpaceOccupiedByAlly(transform.position + Vector3.left) && !isSpaceOccupiedByWall(transform.position + Vector3.left))
        {
            transform.Translate(Vector3.left);
        }
    }

    //Move to the right one tile
    public void moveRight()
    {
        if (!isSpaceOccupiedByEnemy(transform.position + Vector3.right) && !isSpaceOccupiedByAlly(transform.position + Vector3.right) && !isSpaceOccupiedByWall(transform.position + Vector3.right))
        {
            transform.Translate(Vector3.right);
        }
    }

    //Move up one tile
    public void moveUp()
    {
        if (!isSpaceOccupiedByEnemy(transform.position + Vector3.up) && !isSpaceOccupiedByAlly(transform.position + Vector3.up) && !isSpaceOccupiedByWall(transform.position + Vector3.up))
        {
            transform.Translate(Vector3.up);
        }
    }

    //Move down one tile
    public void moveDown()
    {
        if (!isSpaceOccupiedByEnemy(transform.position + Vector3.down) && !isSpaceOccupiedByAlly(transform.position + Vector3.down) && !isSpaceOccupiedByWall(transform.position + Vector3.down))
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

    static public bool isSpaceOccupiedByWall(Vector3 destination)
    {
        bool isOccupied = false;

        foreach (GameObject wall in LevelHandler.walls)
        {
            List<Vector2> wallPos = new List<Vector2>();

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

                    Vector2 pos1 = new Vector2(wall.transform.position.x - halfwidth + i, wall.transform.position.y - halfheight + j);
                    wallPos.Add(pos1);
                }
            }

            if (wallPos.Contains(destination))
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
        currentMovement = currentMovement - slowStacks;
        slowStacks = 0;
        if (isPoisoned)
        {
            enemyHealth -= 5;
        }
        for (int i = 0; i < currentMovement; i++)
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

            //Basically it checks if distance is further on x or y axis then moves one tile on that axis towards the monster
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
        
        bool mostMoves = true;
        foreach(GameObject enemy in TurnHandler.enemies)
        {
            if (enemy.GetComponent<EnemyHandler>().currentMovement > currentMovement){
                mostMoves = false;
            }
        }
        if (mostMoves)
        {
            TurnHandler.enemyTurnOver = true;
            TurnHandler.enemiesMoving = false;
            TurnHandler.isPlayerTurn = true;
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

        //Checks if monster is in adjacent tile
        int xDiff = (int)closestMonster.transform.position.x - (int)transform.position.x;
        int yDiff = (int)closestMonster.transform.position.y - (int)transform.position.y;

        if (xDiff == 0 && Math.Abs(yDiff) == 1 || Math.Abs(xDiff) == 1 && yDiff == 0)
        {
<<<<<<< HEAD
            closestMonster.GetComponent<MonsterHandler>().monsterHealth = closestMonster.GetComponent<MonsterHandler>().monsterHealth - enemyDamage();
            GameObject.Find("Combat Log").GetComponent<Text>().text =  "An enemy hit your monster for " + enemyDamage() + " damage! It now has " + closestMonster.GetComponent<MonsterHandler>().monsterHealth + " health remaining!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
=======
            //Type-dependent effects considered
            if (enemyType != "kangaroo")
            {
                closestMonster.GetComponent<MonsterHandler>().monsterHealth = closestMonster.GetComponent<MonsterHandler>().monsterHealth - enemyDamage;
            }
            //Spider
            if(enemyType == "spider")
            {
                closestMonster.GetComponent<MonsterHandler>().slowStacks++;
            }
            //Slime
            else if (enemyType == "slime")
            {
                closestMonster.GetComponent<MonsterHandler>().isPoisoned = true;
                GameObject.Find("Combat Log").GetComponent<Text>().text = "Your monster was poisoned." + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
            }
            //Kangaroo
            else if (enemyType == "kangaroo")
            {
                closestMonster.GetComponent<MonsterHandler>().monsterHealth = closestMonster.GetComponent<MonsterHandler>().monsterHealth - enemyDamage;
                GameObject.Find("Combat Log").GetComponent<Text>().text = " An enemy hit your monster for " + enemyDamage + " damage! It now has " + closestMonster.GetComponent<MonsterHandler>().monsterHealth + " health remaining!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
                closestMonster.GetComponent<MonsterHandler>().monsterHealth = closestMonster.GetComponent<MonsterHandler>().monsterHealth - (enemyDamage + 5);
                GameObject.Find("Combat Log").GetComponent<Text>().text = " An enemy hit your monster for " + (enemyDamage + 5) + " damage! It now has " + closestMonster.GetComponent<MonsterHandler>().monsterHealth + " health remaining!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
                StartCoroutine(knockBack(closestMonster,3));
                GameObject.Find("Combat Log").GetComponent<Text>().text = "Your monster was knocked away." + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
            }
            else if(enemyType == "dragon")
            {
                closestMonster.GetComponent<MonsterHandler>().monsterDamage = closestMonster.GetComponent<MonsterHandler>().monsterDamage - 4;
                GameObject.Find("Combat Log").GetComponent<Text>().text = "The dragon siphoned some of your power!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
                enemyDamage += 4;
                
            }
            //Not kangaroo
            if(enemyType != "kangaroo")
            {
                GameObject.Find("Combat Log").GetComponent<Text>().text = " An enemy hit your monster for " + enemyDamage + " damage! It now has " + closestMonster.GetComponent<MonsterHandler>().monsterHealth + " health remaining!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
            }
        }
    }
    
    public IEnumerator knockBack(GameObject closestMonster,int power)
    {
        if (closestMonster.transform.position == transform.position + Vector3.left)
        {
            for (int i = 0; i < power; i++)
            {
                closestMonster.GetComponent<MonsterHandler>().moveLeft();
                closestMonster.GetComponent<MonsterHandler>().currentMovement ++;
                yield return new WaitForSeconds(0.33f);
            }
            
        }
        else if (closestMonster.transform.position == transform.position + Vector3.right)
        {
            for (int i = 0; i < power; i++)
            {
                closestMonster.GetComponent<MonsterHandler>().moveRight();
                closestMonster.GetComponent<MonsterHandler>().currentMovement++;
                yield return new WaitForSeconds(0.33f);
            }
        }
        else if (closestMonster.transform.position == transform.position + Vector3.up)
        {
            for (int i = 0; i < power; i++)
            {
                closestMonster.GetComponent<MonsterHandler>().moveUp();
                closestMonster.GetComponent<MonsterHandler>().currentMovement++;
                yield return new WaitForSeconds(0.33f);
            }
        }
        else if (closestMonster.transform.position == transform.position + Vector3.down)
        {
            for (int i = 0; i < power; i++)
            {
                closestMonster.GetComponent<MonsterHandler>().moveDown();
                closestMonster.GetComponent<MonsterHandler>().currentMovement++;
                yield return new WaitForSeconds(0.33f);
            }
>>>>>>> origin/Fei
        }
    }
    
    //Drops enemy loot
    public void dropLoot()
    {
        int lootDrop = (int)Random.Range(0, 100);
        
        if (enemyType == "werewolf")
        {
            if (lootDrop < 39)
            { 
                //do nothing
            } 
            else if (lootDrop < 79)
            {
                Inventory.playerInventory.Add(new ItemHandler.Item("Werewolf Fur", Resources.Load<Sprite>("Werewolf Fur")));
            }
            else if (lootDrop < 94)
            {
                Inventory.playerInventory.Add(new ItemHandler.Item("Werewolf Teeth", Resources.Load<Sprite>("Werewolf Teeth")));
            }
            else if (lootDrop < 99)
            {
                //do nothing
            }
            else if (lootDrop < 100)
            {
                //do nothing
            }
        }
        else if (enemyType == "spider")
        {
            if (lootDrop < 39)
            {
                //do nothing
            }
            else if (lootDrop < 79)
            {
                Inventory.playerInventory.Add(new ItemHandler.Item("Spider Legs", Resources.Load<Sprite>("Spider Legs")));
            }
            else if (lootDrop < 94)
            {
                //do nothing
            }
            else if (lootDrop < 99)
            {
                Inventory.playerInventory.Add(new ItemHandler.Item("Spider Mandibles", Resources.Load<Sprite>("Spider Mandibles")));
            }
            else if (lootDrop < 100)
            {
                Inventory.playerInventory.Add(new ItemHandler.Item("Spider Web Sac", Resources.Load<Sprite>("Spider Web Sac")));
            }
        }
        else if (enemyType == "slime")
        {
            if (lootDrop < 39)
            {
                //do nothing
            }
            else if (lootDrop < 79)
            {
                //do nothing
            }
            else if (lootDrop < 94)
            {
                Inventory.playerInventory.Add(new ItemHandler.Item("Slimey Slime", Resources.Load<Sprite>("Slimey Slime")));
            }
            else if (lootDrop < 99)
            {
                //do nothing
            }
            else if (lootDrop < 100)
            {
                Inventory.playerInventory.Add(new ItemHandler.Item("Toxic Slime", Resources.Load<Sprite>("Toxic Slime")));
            }
        }
        else if (enemyType == "kangaroo")
        {
            Inventory.playerInventory.Add(new ItemHandler.Item("Boxing Gloves", Resources.Load<Sprite>("Boxing Gloves")));
        }
        else if (enemyType == "dragon")
        {
            Inventory.playerInventory.Add(new ItemHandler.Item("Dragon Head", Resources.Load<Sprite>("Dragon Head")));
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
