using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHandler : MonoBehaviour
{
    //These variables store the max tiles this unit can move and the amount of tiles it can still move this turn
    public int maxMovement = 4;
    public int currentMovement = 4;

    //These variables store the number of actions this unit has
    public int maxActions = 1;
    public int currentActions = 1;

    //Monster's health and damage
    public int monsterHealth = 40;
    public int monsterDamage = 10;

    //Importing sprite renderer
    SpriteRenderer sprite;

    //Sprite color
    public Color monsterColor;

    //Keeps track of whether this unit has been selected for movement
    public bool isSelected = false;

    //This function checks whether a destination space has an ally or enemy in it
    public bool isSpaceOccupiedByAlly(Vector3 destination)
    {
        bool isOccupied = false;
        
        foreach(GameObject ally in TurnHandler.allies)
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

    //Move to the left one tile
    public void moveLeft()
    {
        if (!isSpaceOccupiedByAlly(transform.position + Vector3.left))
        {
            transform.Translate(Vector3.left);
            currentMovement--;
        }
    }

    //Move to the right one tile
    public void moveRight()
    {
        if (!isSpaceOccupiedByAlly(transform.position + Vector3.right))
        {
            transform.Translate(Vector3.right);
            currentMovement--;
        } 
    }

    //Move up one tile
    public void moveUp()
    {
        if (!isSpaceOccupiedByAlly(transform.position + Vector3.up))
        {
            transform.Translate(Vector3.up);
            currentMovement--;
        }
    }

    //Move down one tile
    public void moveDown()
    {
        if (!isSpaceOccupiedByAlly(transform.position + Vector3.down))
        {
            transform.Translate(Vector3.down);
            currentMovement--;
        }
    }

    public void attackEnemy(Vector3 target)
    {
        if (currentActions > 0)
        {
            foreach (GameObject enemy in TurnHandler.enemies)
            {
                if (enemy.transform.position == target)
                {
                    enemy.GetComponent<EnemyHandler>().enemyHealth = enemy.GetComponent<EnemyHandler>().enemyHealth - monsterDamage;
                    GameObject.Find("Combat Log").GetComponent<Text>().text = "You hit an enemy for " + monsterDamage + " damage! It now has " + enemy.GetComponent<EnemyHandler>().enemyHealth + " health remaining!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
                }
            }
            currentActions--;
        }
    }

    public void die()
    {
        MonsterList.monsterList.Remove(this.gameObject);
        TurnHandler.allies.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void removeFromScreen()
    {
        transform.position = new Vector2(100000,100000);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Getting the sprite renderer
        sprite = GetComponent<SpriteRenderer>();
    }

    //This allows for the player to slightly overclick a tile and still be able to move
    public float movementLeniency = 2.5f;

    // Update is called once per frame
    void Update()
    {
        //Detects left click
        if (Input.GetMouseButtonDown(0))
        {
            //This gets the position of the cursor when the click was made
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Checks if the unit had remaining movement, and if it is currently their turn
            if (TurnHandler.isPlayerTurn)
            {
                //Checks if player clicks to the right
                if (cursorPos.x > (transform.position.x + 0.5) && cursorPos.x < (transform.position.x + movementLeniency) && cursorPos.y < (transform.position.y + 0.5) && cursorPos.y > (transform.position.y - 0.5) && isSelected)
                {
                    //Checks that target does not contain enemy
                    if (currentMovement > 0 && !isSpaceOccupiedByEnemy(transform.position + Vector3.right))
                    {
                        moveRight();
                    }
                    else
                    {
                        attackEnemy(transform.position + Vector3.right);
                    }
                }
                //Checks if player clicks to the left
                if (cursorPos.x < (transform.position.x - 0.5) && cursorPos.x > (transform.position.x - movementLeniency) && cursorPos.y < (transform.position.y + 0.5) && cursorPos.y > (transform.position.y - 0.5) && isSelected)
                {
                    //Checks that target does not contain enemy
                    if (currentMovement > 0 && !isSpaceOccupiedByEnemy(transform.position + Vector3.left))
                    {
                        moveLeft();
                    }
                    else
                    {
                        attackEnemy(transform.position + Vector3.left);
                    }
                }
                //Checks if player clicks upwards
                if (cursorPos.y > (transform.position.y + 0.5) && cursorPos.y < (transform.position.y + movementLeniency) && cursorPos.x < (transform.position.x + 0.5) && cursorPos.x > (transform.position.x - 0.5) && isSelected)
                {
                    //Checks that target does not contain enemy
                    if (currentMovement > 0 && !isSpaceOccupiedByEnemy(transform.position + Vector3.up))
                    {
                        moveUp();
                    }
                    else
                    {
                        attackEnemy(transform.position + Vector3.up);
                    }
                }
                //Checks if player clicks downwards
                if (cursorPos.y < (transform.position.y - 0.5) && cursorPos.y > (transform.position.y - movementLeniency) && cursorPos.x < (transform.position.x + 0.5) && cursorPos.x > (transform.position.x - 0.5) && isSelected)
                {
                    //Checks that target does not contain enemy
                    if (currentMovement > 0 && !isSpaceOccupiedByEnemy(transform.position + Vector3.down))
                    {
                        moveDown();
                    }
                    else
                    {
                        attackEnemy(transform.position + Vector3.down);
                    }
                }
                //Checks if the player clicks one of their units
                if (cursorPos.y < (transform.position.y + 0.5) && cursorPos.y > (transform.position.y - 0.5) && cursorPos.x < (transform.position.x + 0.5) && cursorPos.x > (transform.position.x - 0.5))
                {
                    //Sets all allies to be unselected
                    foreach (GameObject ally in TurnHandler.allies)
                    {
                        ally.GetComponent<MonsterHandler>().isSelected = false;
                    }
                    //Sets this unit to be selected
                    isSelected = true;
                }
            }
        }
        //When a unit runs out of movement it turns translucent
        if (currentMovement == 0)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.2f);
        }
        if (currentMovement != 0)
        {
            sprite.color = monsterColor;
        }
        if(monsterHealth <= 0)
        {
            die();
        }
    }
}