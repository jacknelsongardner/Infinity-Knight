using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    public GameObject gameBrain;
    public GameObject boardParent;

    public int boardX;
    public int boardY;

    public int highestBoardY;

    public int status;

    public bool canMove;

    public float deathHeightMin;
    public float deathHeightMax;

    // Start is called before the first frame update
    void Start()
    {
        highestBoardY = 0;
        status = 1;

        canMove = true;

        this.transform.SetParent(boardParent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (highestBoardY < boardY)
        {
            highestBoardY = boardY;
        }
    }

    public bool testHeightDeath()
    {
        float actualHeight = this.transform.TransformPoint(this.transform.position).y;

        if (actualHeight <= deathHeightMin || actualHeight >= deathHeightMax)
        {
            Debug.Log(actualHeight);
            this.canMove = false;

            gameBrain.GetComponent<BrainScript>().Lose();
            return true;
        }

        return false;
        
    }

    public bool knightCanMove(int tileY, int tileX)
    {
        if (canMove == true)
        {
            // checking move up 2
            if (tileY == this.boardY + 2)
            {
                // checking move right 1
                if (tileX == this.boardX + 1)
                {
                    return true;
                }
                // checking move left 1
                else if (tileX == this.boardX - 1)
                {
                    return true;
                }
            }
            // checking move down 2
            else if (tileY == this.boardY - 2)
            {
                // checking move right one
                if (tileX == this.boardX + 1)
                {
                    return true;
                }
                // checking move left one
                else if (tileX == this.boardX - 1)
                {
                    return true;
                }
            }
            // checking move up one
            else if (tileY == this.boardY + 1)
            {
                // checking move right 2
                if (tileX == this.boardX + 2)
                {
                    return true;
                }
                // checking move left 2
                else if (tileX == this.boardX - 2)
                {
                    return true;
                }
            }
            // checking move down one
            else if (tileY == this.boardY - 1)
            {
                // checking move right 2
                if (tileX == this.boardX + 2)
                {
                    return true;
                }
                // checking move left 2
                else if (tileX == this.boardX - 2)
                {
                    return true;
                }
            }
        }
        else if (canMove == false)
        {
            // we must not be allowed to move ANYWHERE :/
            Debug.Log("can't move ANYWHERE!");
            return false;
        }

        // we can't move to that spot (not compatable) 
        Debug.Log("can't go there!!! pick someplace else silly :P" + tileY);
        
        return false;
    }
}
