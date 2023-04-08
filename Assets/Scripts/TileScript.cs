using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject boardParent;
    
    public int boardX;
    public int boardY;

    public Color darkTileColor;
    public Color lightTileColor;

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

    // if it's out of range of the camera, we destroy this cloned tile (too many tiles offscreen will cause the game to crash)
    public void testKill()
    {
        if (this.transform.position.y < killHeight)
        {
            Destroy(gameObject);
        }
    }

    // checking if this should be black or white tile (based on if it's odd, even, x, y, etc)
    void CheckType()
    {
            // checking to see if it should be light grey
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
            // checking to see if it should be dark grey
            else if (boardX % 2 != 0 && boardY % 2 == 0)
            {
                // setting dark
                this.GetComponent<SpriteRenderer>().material.color = darkTileColor;
            }
            else if (boardX % 2 == 0 && boardY % 2 != 0)
            {
                // setting dark
                this.GetComponent<SpriteRenderer>().material.color = darkTileColor;
            }
            else
            {
                // do nothing for now :/
            }
        
    }

    // when this object is clicked
    void OnMouseDown()
    {
        // telling the gameBrain that we got CLICKED! woohoo!
        boardParent.GetComponent<MovingBoard>().tileGotClicked(this.gameObject);
        Debug.Log("clicked on a tile");
        
    }
}
