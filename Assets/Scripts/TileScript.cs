using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject boardParent;
    
    public int boardX;
    public int boardY;

    public bool isObstacle;

    public Color darkTileColor;
    public Color lightTileColor;
    public Color obstacleColor;


    public float killHeight;

    // Start is called before the first frame update
    void Start()
    {

        this.transform.SetParent(boardParent.transform);

        CheckType();
    }

    // Update is called once per frame
    void Update()
    {
        testKill();
    }

    public void testKill()
    {
        if (this.transform.position.y < killHeight)
        {
            Destroy(gameObject);
        }
    }
    void CheckType()
    {
        // checking if this tile is an obstacle
        if (isObstacle == false)
        {
            if (boardX % 2 == 0 && boardY % 2 == 0)
            {
                // setting light 
                this.GetComponent<SpriteRenderer>().material.color = lightTileColor;
            }
            else if (boardX % 2 != 0 && boardY % 2 != 0)
            {
                // setting light
                this.GetComponent<SpriteRenderer>().material.color = lightTileColor;
            }
            else if (boardX % 2 != 0 && boardY % 2 == 0)
            {
                this.GetComponent<SpriteRenderer>().material.color = darkTileColor;
            }
            else if (boardX % 2 == 0 && boardY % 2 != 0)
            {
                this.GetComponent<SpriteRenderer>().material.color = darkTileColor;
            }
            else
            {
                // do nothing for now :/
            }
        }
        else if (isObstacle == true)
        {
            this.GetComponent<SpriteRenderer>().material.color = obstacleColor;
        }
    }

    void OnMouseDown()
    {
        if (isObstacle == false)
        {
            boardParent.GetComponent<MovingBoard>().tileGotClicked(this.gameObject);
            Debug.Log("clicked on a tile");
        }
        else if (isObstacle == true)
        {
            Debug.Log("clicked on an obstacle");
        }
        else
        {
            // do nothing
        }
            Debug.Log("Object clicked!");
    }
}
