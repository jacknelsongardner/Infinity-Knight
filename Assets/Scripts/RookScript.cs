using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookScript : EnemyScript
{

    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(boardParent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool testAttack(GameObject knight)
    {
        int knightX = knight.GetComponent<KnightScript>().boardX;
        int knightY = knight.GetComponent<KnightScript>().boardY;

        if (knightX == boardX && knightY != boardY)
        {
            Debug.Log("rook attacking horizontally");
            return true;
        }
        else if (knightX != boardX && knightY == boardY)
        {
            Debug.Log("rook attacking vertically");
            return true;
        }

        return false;
    }
}
