using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopScript : EnemyScript
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

        float actualHeight = this.transform.TransformPoint(this.transform.position).y;

        // making sure we are in attacking range (not too high, not too low)
        if (actualHeight <= attackMaxHeight && actualHeight >= attackMinHeight)
        {
            // checking diagonal up left and down right
            if (boardX - knightX == boardY - knightY && boardX != knightX && boardY != knightY)
            {
                takeKnight(knight);
                return true;
            }
            // checking diagonal up right and down left
            else if (boardX - knightX == -(boardY - knightY) && boardX != knightX && boardY != knightY)
            {
                takeKnight(knight);
                return true;
            }
        }
        return false;
    }

    // tests to see if this enemy is in conflict with another enemy (so that no enemy peices can "back eachother up")
    public override bool testConflict(GameObject enemy)
    {
        int enemyX = enemy.GetComponent<EnemyScript>().boardX;
        int enemyY = enemy.GetComponent<EnemyScript>().boardY;

            // checking diagonal up left and down right
            if (boardX - enemyX == boardY - enemyY)
            {
                return true;
            }
            // checking diagonal up right and down left
            else if (boardX - enemyX == -(boardY - enemyY))
            {
                return true;
            }
        
        return false;
    }
}
