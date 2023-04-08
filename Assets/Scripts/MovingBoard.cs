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

        // spawning 8 rows in the beginning
        SpawnRowNoEnemies(0.0f);
        SpawnRowNoEnemies(1.0f);
        SpawnRowNoEnemies(2.0f);
        SpawnRowNoEnemies(3.0f);
        SpawnRowNoEnemies(4.0f);
        SpawnRowNoEnemies(5.0f);
        SpawnRowNoEnemies(6.0f);
        SpawnRowNoEnemies(7.0f);
        //SpawnRowNoEnemies(8.0f);



        // placing knight on bottom left corner
        KnightScript knightScript = knightPrefab.GetComponent<KnightScript>();
        
        knightScript.boardX = tiles[0].GetComponent<TileScript>().boardX;
        knightScript.boardY = tiles[0].GetComponent<TileScript>().boardY;

        Vector3 newPosition = new Vector3(tiles[0].GetComponent<Transform>().position.x,
                                    tiles[0].GetComponent<Transform>().position.y,
                                    knightPrefab.GetComponent<Transform>().position.z);

        GameObject newKnight = Instantiate(knightPrefab, newPosition, Quaternion.Euler(0, 0, 0));
        newKnight.GetComponent<KnightScript>().boardParent = this.gameObject;
        this.knight = newKnight;


        gameBrain.GetComponent<BrainScript>().knight = this.knight;
        this.knight.GetComponent<KnightScript>().gameBrain = this.gameBrain;
    }

    // Update is called once per frame
    void Update()
    {
        
        // checking if knight has fallen behind
        if (knight.GetComponent<KnightScript>().testHeightDeath())
        {
            this.canMove = false;
            Debug.Log("died in spikes!");
        }


        // checking all enemy units stats
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyScript>().testAttack(knight))
            {
                this.canMove = false;
                knight.GetComponent<KnightScript>().canMove = false;
            }

            if (enemy.GetComponent<EnemyScript>().testDie(knight))
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (GameObject enemyToRemove in enemiesToRemove)
        {
            enemies.Remove(enemyToRemove);
        }

        if (canMove == true)
        {
            // move the entire game board (including peices) down
            Vector3 position = this.gameObject.transform.position;
            position.y -= baseMovementSpeed * speedIncreaseFactor * Time.deltaTime;
            spaceMoved += baseMovementSpeed * speedIncreaseFactor * Time.deltaTime;

            this.gameObject.transform.position = position;

            CheckNeedSpawnRow();
        }
        else 
        { 
            // do nothing   
        }
    }

    public void IncrementSpeedIncreaseFactor()
    {
        speedIncreaseFactor = speedIncreaseFactor * speedIncreaseFactor + 0.3f;
    }

    public void IncrementSpeedIncreaseFactor(float increaseToIncrease)
    {
        speedIncreaseFactor = speedIncreaseFactor * speedIncreaseFactor + increaseToIncrease;
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
        foreach(GameObject enemy in enemies)
        {
            if (testEnemy.GetComponent<EnemyScript>().testConflict(enemy))
            {
                return true;
            }
        }

        return false;
    }

    private void SpawnRowNoEnemies(float atHeight)
    {

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

    private void SpawnRowWithEnemies(float atHeight)
    {
        bool spawnedEnemyInRow = false;

        for (float i = 0.0f; i < (float)boardWidth; i++)
        {
            Vector3 clonePosition = new Vector3(this.transform.position.x + i,
                                        this.transform.position.y + atHeight,
                                        tilePrefab.GetComponent<Transform>().position.z);

            GameObject newTile = Instantiate(tilePrefab, clonePosition, Quaternion.Euler(0, 0, 0));

            newTile.GetComponent<TileScript>().boardX = (int)i + 1;
            newTile.GetComponent<TileScript>().boardY = (int)atHeight + 1;

            newTile.GetComponent<TileScript>().boardParent = this.gameObject;

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

    private bool TrySpawnRook(GameObject tile, float chance)
    {
        // Generate a random number between 0 and 1
        float randomNumber = Random.Range(0f, 1f);

        // Check if the random number is less than the chance
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

    public void Stop()
    {
        canMove = false;
    }

    public void tileGotClicked(GameObject sender)
    {
        var knightScript = knight.GetComponent<KnightScript>();
        var senderScript = sender.GetComponent<TileScript>();


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
