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
    public int maxHealth = 40;
    public int monsterHealth = 40;
    public int monsterDamage = 10;

    //Importing sprite renderer
    SpriteRenderer sprite;

    //Possible movement sprite
    public GameObject possibleMovement;

    //Sprite color
    public Color monsterColor;

    //Keeps track of whether this unit has been selected for movement
    public bool isSelected = false;

    //Keeps track of whether unit is slowed
    public int slowStacks = 0;

    //Does this monster have spider special ability?
    public bool spiderSpecial = false;

    //Does this monster have slime special ability?
    public bool slimeSpecial = false;

    //Does this monster have kangaroo special ability?
    public bool kangarooSpecial = false;

    //Does this monster have dragon special ability?
    public bool dragonSpecial = false;

    //Keeps track of whether unit is poisoned
    public bool isPoisoned = false;

    //Monster's health bar
    public GameObject monsterHealthBar;

    public GameObject healthBarPrefab;

    public void createHealthUI()
    {
        GameObject healthUI = Instantiate(healthBarPrefab, transform.position + new Vector3(0f, 0.6f, 0f), Quaternion.identity);
        healthUI.transform.SetParent(GameObject.Find("Canvas").transform);
        healthUI.transform.localScale = new Vector3(0.8f, 0.1f, 0);
        monsterHealthBar = healthUI;
        updateHealthUI();
    }

    public void updateHealthUI()
    {
        monsterHealthBar.GetComponent<Image>().fillAmount = (float)monsterHealth/(float)maxHealth;
        monsterHealthBar.transform.position = transform.position + new Vector3(0f, 0.6f, 0f);

        if (monsterHealthBar.GetComponent<Image>().fillAmount <= 0.2f)
        {
            monsterHealthBar.GetComponent<Image>().color = Color.red;
        }
        else if (monsterHealthBar.GetComponent<Image>().fillAmount <= 0.5f)
        {
            monsterHealthBar.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            monsterHealthBar.GetComponent<Image>().color = Color.green;
        }
    }

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

    //Move to the left one tile
    public void moveLeft()
    {
        if (!isSpaceOccupiedByAlly(transform.position + Vector3.left) && !isSpaceOccupiedByWall(transform.position + Vector3.left))
        {
            transform.Translate(Vector3.left);
            currentMovement--;
        }
    }

    //Move to the right one tile
    public void moveRight()
    {
        if (!isSpaceOccupiedByAlly(transform.position + Vector3.right) && !isSpaceOccupiedByWall(transform.position + Vector3.right))
        {
            transform.Translate(Vector3.right);
            currentMovement--;
        } 
    }

    //Move up one tile
    public void moveUp()
    {
        if (!isSpaceOccupiedByAlly(transform.position + Vector3.up) && !isSpaceOccupiedByWall(transform.position + Vector3.up))
        {
            transform.Translate(Vector3.up);
            currentMovement--;
        }
    }

    //Move down one tile
    public void moveDown()
    {
        if (!isSpaceOccupiedByAlly(transform.position + Vector3.down) && !isSpaceOccupiedByWall(transform.position + Vector3.down))
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
                    if (spiderSpecial)
                    {
                        enemy.GetComponent<EnemyHandler>().slowStacks += 2;
                    }
                    if (slimeSpecial)
                    {
                        enemy.GetComponent<EnemyHandler>().isPoisoned = true;
                    }
                    if (kangarooSpecial)
                    {
                        StartCoroutine(knockBack(enemy, 3));
                    }
                    if (dragonSpecial)
                    {
                        enemy.GetComponent<EnemyHandler>().enemyDamage -= 4;
                    }
                    GameObject.Find("Combat Log").GetComponent<Text>().text = "You hit an enemy for " + monsterDamage + " damage! It now has " + enemy.GetComponent<EnemyHandler>().enemyHealth + " health remaining!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
                    currentActions--;
                    if (dragonSpecial)
                    {
                        GameObject.Find("Combat Log").GetComponent<Text>().text = "You weakened an enemy!" + "\n" + GameObject.Find("Combat Log").GetComponent<Text>().text;
                    }
                }
            }
            
        }
    }

    public IEnumerator knockBack(GameObject closestMonster, int power)
    {
        if (closestMonster.transform.position == transform.position + Vector3.left)
        {
            for (int i = 0; i < power; i++)
            {
                closestMonster.GetComponent<EnemyHandler>().moveLeft();
                yield return new WaitForSeconds(0.33f);
            }

        }
        else if (closestMonster.transform.position == transform.position + Vector3.right)
        {
            for (int i = 0; i < power; i++)
            {
                closestMonster.GetComponent<EnemyHandler>().moveRight();
                yield return new WaitForSeconds(0.33f);
            }
        }
        else if (closestMonster.transform.position == transform.position + Vector3.up)
        {
            for (int i = 0; i < power; i++)
            {
                closestMonster.GetComponent<EnemyHandler>().moveUp();
                yield return new WaitForSeconds(0.33f);
            }
        }
        else if (closestMonster.transform.position == transform.position + Vector3.down)
        {
            for (int i = 0; i < power; i++)
            {
                closestMonster.GetComponent<EnemyHandler>().moveDown();
                yield return new WaitForSeconds(0.33f);
            }
        }
    }

    public bool isMoveable(Vector3 destination)
    {
        bool result = true;
        if (isSpaceOccupiedByWall(destination) || isSpaceOccupiedByEnemy(destination) || isSpaceOccupiedByAlly(destination))
        {
            result = false;
        }
        return result;
    }

    public void selectMonster()
    {
        isSelected = true;

        List<Vector3> moveableTiles = new List<Vector3>();

        Vector3 currentPosition = transform.position;
        moveableTiles.Add(currentPosition);

        for (int i = 0; i < currentMovement; i++)
        {
            List<Vector3> moveableTilesClone = new List<Vector3>(moveableTiles);

            foreach(Vector3 position in moveableTilesClone)
            {
                if (isMoveable(position + Vector3.left) && !moveableTiles.Contains(position + Vector3.left))
                {
                    moveableTiles.Add(position + Vector3.left);
                }
                if (isMoveable(position + Vector3.right) && !moveableTiles.Contains(position + Vector3.right))
                {
                    moveableTiles.Add(position + Vector3.right);
                }
                if (isMoveable(position + Vector3.up) && !moveableTiles.Contains(position + Vector3.up))
                {
                    moveableTiles.Add(position + Vector3.up);
                }
                if (isMoveable(position + Vector3.down) && !moveableTiles.Contains(position + Vector3.down))
                {
                    moveableTiles.Add(position + Vector3.down);
                }
            }   

        }

        moveableTiles.Remove(currentPosition);

        foreach (Vector3 tile in moveableTiles)
        {
            Instantiate(possibleMovement, tile, Quaternion.identity);
        }

    }
    public static List<GameObject> moveTiles;

    public void deselectMonster()
    {
        isSelected = false;

        moveTiles = new System.Collections.Generic.List<GameObject>();
        moveTiles.AddRange(GameObject.FindGameObjectsWithTag("Movement"));

        foreach (GameObject tile in moveTiles)
        {
            tile.GetComponent<MovementTiles>().die();
        }
    }

    public void die()
    {
        MonsterList.monsterList.Remove(this.gameObject);
        TurnHandler.allies.Remove(this.gameObject);
        Destroy(this.gameObject);
        Destroy(monsterHealthBar);
    }

    public void reset()
    {
        currentMovement = maxMovement;
        currentActions = maxActions;
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
        if (Input.GetMouseButtonDown(0) && InputEnabled.isInputEnabled)
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
                        ally.GetComponent<MonsterHandler>().deselectMonster();
                    }
                    //Sets this unit to be selected
                    selectMonster();
                }
            }
        }
        //When a unit runs out of movement it turns translucent
        if ((currentMovement == 0 && currentActions == 0)|| !TurnHandler.isPlayerTurn)
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