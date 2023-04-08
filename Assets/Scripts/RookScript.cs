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
        testDestroy();
    }

    // testing if we can attack the player
    public override bool testAttack(GameObject knight)
    {
        int knightX = knight.GetComponent<KnightScript>().boardX;
        int knightY = knight.GetComponent<KnightScript>().boardY;

        // if we aren't too high or too low
        if (this.transform.position.y <= attackMaxHeight && this.transform.position.y >= attackMinHeight)
        {
            // if we can attack vertically
            if (knightX == boardX && knightY != boardY)
            {
                // go to the knights location!
                Debug.Log("rook attacking horizontally");
                takeKnight(knight);
                return true;
            }
            // if we can attack horizontally
            else if (knightX != boardX && knightY == boardY)
            {
                // go to the knights location!
                Debug.Log("rook attacking vertically");
                takeKnight(knight);
                return true;
            }
        }

        return false;
    }

    // testing if we're in conflict with another enemy
    public override bool testConflict(GameObject enemy)
    {
        int enemyX = enemy.GetComponent<EnemyScript>().boardX;
        int enemyY = enemy.GetComponent<EnemyScript>().boardY;

        if (enemyX == boardX || enemyY == boardY)
        {
            return true;
        }
        
        return false;
    }
}
