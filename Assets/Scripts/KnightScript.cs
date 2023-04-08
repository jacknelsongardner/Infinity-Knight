using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    public int boardX;
    public int boardY;

    private int DEAD;
    private int ALIVE;

    public int status;

    // Start is called before the first frame update
    void Start()
    {
        DEAD = 0;
        ALIVE = 1;

        status = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool knightCanMove(int tileY, int tileX)
    { 
        if (tileY == this.boardY + 2)
        {
            if (tileX == this.boardX + 1)
            {
                return true;
            }
            else if (tileX == this.boardX - 1)
            {
                return true;
            }
        }
        else if (tileY == this.boardY - 2)
        {
            if (tileX == this.boardX + 1)
            {
                return true;
            }
            else if (tileX == this.boardX - 1)
            {
                return true;
            }
        }
        else if (tileY == this.boardY + 1)
        {
            if (tileX == this.boardX + 2)
            {
                return true;
            }
            else if (tileX == this.boardX - 2)
            {
                return true;
            }
        }
        else if (tileY == this.boardY - 1)
        {
            if (tileX == this.boardX + 2)
            {
                return true;
            }
            else if (tileX == this.boardX - 2)
            {
                return true;
            }
        }

        return false;
    }
}
