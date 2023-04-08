using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject gameBrain;

    public int boardX;
    public int boardY;

    public bool isObstacle;

    public Color darkTileColor;
    public Color lightTileColor;
    public Color obstacleColor;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (isObstacle == false)
        {
            gameBrain.GetComponent<BrainScript>().tileGotClicked(this.gameObject);
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
