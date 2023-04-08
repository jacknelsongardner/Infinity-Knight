using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoard : MonoBehaviour
{
    public GameObject gameBrain;
    
    public GameObject tilePrefab;

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
        SpawnRow(0.0f);
        SpawnRow(1.0f);
        SpawnRow(2.0f);
        SpawnRow(3.0f);
        SpawnRow(4.0f);
        SpawnRow(5.0f);
        SpawnRow(6.0f);
        SpawnRow(7.0f);
        SpawnRow(8.0f);



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
        
    }

    // Update is called once per frame
    void Update()
    {
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
            SpawnRow((float)rowsSpawned);
            spaceMoved -= 1.0f;
        }
    }

    private void SpawnRow(float atHeight)
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
