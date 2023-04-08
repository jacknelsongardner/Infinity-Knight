using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoard : MonoBehaviour
{
    public GameObject gameBrain;
    
    public GameObject tilePrefab;

    public List<GameObject> enemies;

    public GameObject rookPrefab;
    public GameObject bishopPrefab;
    public GameObject queenPrefab;

    public GameObject knightPrefab;
    public GameObject knight;

    public bool canMove;

    public List<GameObject> tiles;

    public float baseMovementSpeed;
    public float speedIncreaseFactor;

    public int boardWidth;
    
    private int spawnHeight;
    public int spawnDistance;

    public float spaceMoved;

    public int rowsSpawned;

    // Start is called before the first frame update
    void Start()
    {

        spaceMoved = 0;
        speedIncreaseFactor = 1;

        rowsSpawned = 0;

        // spawning 6 rows without enemies (give the player some breathing room)
        SpawnRowNoEnemies(0.0f);
        SpawnRowNoEnemies(1.0f);
        SpawnRowNoEnemies(2.0f);
        SpawnRowNoEnemies(3.0f);
        SpawnRowNoEnemies(4.0f);
        SpawnRowNoEnemies(5.0f);

        // spawning 2 rows WITH enemies (we can't make this too easy ;) )
        SpawnRowWithEnemies(6.0f);
        SpawnRowWithEnemies(7.0f);



        // placing knight on bottom left corner
        KnightScript knightScript = knightPrefab.GetComponent<KnightScript>();
        
        // setting knight boardX and Y to the the bottom left tile
        knightScript.boardX = tiles[0].GetComponent<TileScript>().boardX;
        knightScript.boardY = tiles[0].GetComponent<TileScript>().boardY;

        // setting physical position of knight
        Vector3 newPosition = new Vector3(tiles[0].GetComponent<Transform>().position.x,
                                    tiles[0].GetComponent<Transform>().position.y,
                                    knightPrefab.GetComponent<Transform>().position.z);

        // creating new instance of knight
        GameObject newKnight = Instantiate(knightPrefab, newPosition, Quaternion.Euler(0, 0, 0));
        newKnight.GetComponent<KnightScript>().boardParent = this.gameObject;
        this.knight = newKnight;

        gameBrain.GetComponent<BrainScript>().knight = this.knight;
        this.knight.GetComponent<KnightScript>().gameBrain = this.gameBrain;
    }

    // Update is called once per frame
    void Update()
    {
        // checking if knight has gone too high or too low (and ought to be dead)
        KnightCheckHeightDeath();

        // parsing through every enemy on the board and doing attack checks and death checks
        EnemyChecks();

        // moving the entire gameboard
        if (canMove == true)
        {
            // move the entire game board (including peices) down
            Vector3 position = this.gameObject.transform.position;
            position.y -= baseMovementSpeed * speedIncreaseFactor * Time.deltaTime;
            spaceMoved += baseMovementSpeed * speedIncreaseFactor * Time.deltaTime;

            this.gameObject.transform.position = position;

            // checking to see if we need to spawn a new row of tiles
            CheckNeedSpawnRow();
        }
        else 
        { 
            // do nothing   
        }
    }

    public void EnemyChecks()
    {
        // checking all enemy units stats
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            // checking to see if an enemy can attack
            if (enemy.GetComponent<EnemyScript>().testAttack(knight) == true)
            {
                this.canMove = false;
                knight.GetComponent<KnightScript>().canMove = false;
                this.gameBrain.GetComponent<BrainScript>().Lose();
            }

            // checking to see if an enemy has been killed by player
            if (enemy.GetComponent<EnemyScript>().testDie(knight))
            {
                enemiesToRemove.Add(enemy);
            }
        }

        // removing all dead enemies from enemy list
        foreach (GameObject enemyToRemove in enemiesToRemove)
        {
            enemies.Remove(enemyToRemove);
        }
    }

    public void KnightCheckHeightDeath()
    {
        // checking if knight has fallen behind
        if (knight.GetComponent<KnightScript>().testHeightDeath())
        {
            this.canMove = false;
            Debug.Log("died in spikes!");
        }
    }

    private void CheckNeedSpawnRow()
    {
        // checking how much we've moved
        if (spaceMoved >= 1.0f)
        {
            SpawnRowWithEnemies((float)rowsSpawned);
            spaceMoved -= 1.0f;

        }
    }

    private bool CheckEnemyConflicts(GameObject testEnemy)
    {
        // checking to make sure no enemies overlap one another (this would make the game too hard)
        foreach(GameObject enemy in enemies)
        {
            if (testEnemy.GetComponent<EnemyScript>().testConflict(enemy))
            {
                return true;
            }
        }

        return false;
    }

    // spawning a row of tiles with NO chance of spawning enemies
    private void SpawnRowNoEnemies(float atHeight)
    {
        // spawning clones until we've filled the board width (determined by level designer)
        for(float i = 0.0f; i < (float)boardWidth; i++)
        {
            Vector3 clonePosition = new Vector3(this.transform.position.x + i,
                                        this.transform.position.y + atHeight,
                                        tilePrefab.GetComponent<Transform>().position.z);

            GameObject newTile = Instantiate(tilePrefab, clonePosition, Quaternion.Euler(0, 0, 0));

            newTile.GetComponent<TileScript>().boardX = (int)i + 1;
            newTile.GetComponent<TileScript>().boardY = (int)atHeight +1;
            
            newTile.GetComponent<TileScript>().boardParent = this.gameObject;

            tiles.Add(newTile);
        }

        rowsSpawned++;
    }

    // spawning a row of tiles with a chance of having enemies
    private void SpawnRowWithEnemies(float atHeight)
    {
        bool spawnedEnemyInRow = false;

        // spawning clones until we've filled the board width (determined by level designer)
        for (float i = 0.0f; i < (float)boardWidth; i++)
        {
            Vector3 clonePosition = new Vector3(this.transform.position.x + i,
                                        this.transform.position.y + atHeight,
                                        tilePrefab.GetComponent<Transform>().position.z);

            GameObject newTile = Instantiate(tilePrefab, clonePosition, Quaternion.Euler(0, 0, 0));

            newTile.GetComponent<TileScript>().boardX = (int)i + 1;
            newTile.GetComponent<TileScript>().boardY = (int)atHeight + 1;

            newTile.GetComponent<TileScript>().boardParent = this.gameObject;

            // checking to see if we've already spawned a piece in this row (we don't want more than one)
            if (spawnedEnemyInRow == false)
            {

                if (TrySpawnRook(newTile, .8f) == true)
                { spawnedEnemyInRow = true; }
                else if (TrySpawnBishop(newTile, .3f) == true)
                { spawnedEnemyInRow = true; }
            }

            tiles.Add(newTile);
        }

        rowsSpawned++;
    }

    // try to spawn a rook
    private bool TrySpawnRook(GameObject tile, float chance)
    {
        // Generate a random number between 0 and 1
        float randomNumber = Random.Range(0f, 1f);

        // Check if the random number is less than the chance and that we are not directly above the knight
        if (randomNumber < chance && tile.GetComponent<TileScript>().boardX != knight.GetComponent<KnightScript>().boardX)
        {
            // placing knight on bottom left corner
            RookScript rookScript = rookPrefab.GetComponent<RookScript>();

            rookScript.boardX = tile.GetComponent<TileScript>().boardX;
            rookScript.boardY = tile.GetComponent<TileScript>().boardY;
            
            rookScript.GetComponent<RookScript>().boardParent = this.gameObject;

            Vector3 newPosition = new Vector3(tile.GetComponent<Transform>().position.x,
                                        tile.GetComponent<Transform>().position.y,
                                        rookScript.GetComponent<Transform>().position.z);

            if (this.CheckEnemyConflicts(rookPrefab) == true)
            {
                return false;
            }

            GameObject newRook = Instantiate(rookPrefab, newPosition, Quaternion.Euler(0, 0, 0));
            
            enemies.Add(newRook);

            return true;
        }

        return false;
        
    }

    // try to spawn a bishop
    private bool TrySpawnBishop(GameObject tile, float chance)
    {
        // Generate a random number between 0 and 1
        float randomNumber = Random.Range(0f, 1f);

        // Check if the random number is less than the chance
        if (randomNumber < chance)
        {
            // placing knight on bottom left corner
            BishopScript bishopScript = bishopPrefab.GetComponent<BishopScript>();

            bishopScript.boardX = tile.GetComponent<TileScript>().boardX;
            bishopScript.boardY = tile.GetComponent<TileScript>().boardY;

            bishopScript.GetComponent<BishopScript>().boardParent = this.gameObject;

            Vector3 newPosition = new Vector3(tile.GetComponent<Transform>().position.x,
                                        tile.GetComponent<Transform>().position.y,
                                        bishopScript.GetComponent<Transform>().position.z);

            
            if (this.CheckEnemyConflicts(bishopPrefab) == true)
            {
                return false;
            }
            

            GameObject newBishop = Instantiate(bishopPrefab, newPosition, Quaternion.Euler(0, 0, 0));

            enemies.Add(newBishop);

            return true;
        }

        return false;

    }

    // stop the board from moving
    public void Stop()
    {
        canMove = false;
    }

    // if a tile got clicked, we decide what to do
    public void tileGotClicked(GameObject sender)
    {
        var knightScript = knight.GetComponent<KnightScript>();
        var senderScript = sender.GetComponent<TileScript>();

        // if it's in the knights movement range, we move him to the tiles position...otherwise, we do nothing
        if (knightScript.knightCanMove(senderScript.boardY, senderScript.boardX) == true)
        {

            knightScript.boardX = senderScript.boardX;
            knightScript.boardY = senderScript.boardY;

            Vector3 newPosition = new Vector3(sender.GetComponent<Transform>().position.x,
                                        sender.GetComponent<Transform>().position.y,
                                        knight.GetComponent<Transform>().position.z);

            knight.GetComponent<Transform>().position = newPosition;
        }
    }
}
