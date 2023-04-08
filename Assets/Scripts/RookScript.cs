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


    public override bool testAttack(GameObject knight)
    {
        int knightX = knight.GetComponent<KnightScript>().boardX;
        int knightY = knight.GetComponent<KnightScript>().boardY;

        if (this.transform.position.y <= attackMaxHeight && this.transform.position.y >= attackMinHeight)
        {
            if (knightX == boardX && knightY != boardY)
            {
                Debug.Log("rook attacking horizontally");
                takeKnight(knight);
                return true;
            }
            else if (knightX != boardX && knightY == boardY)
            {
                Debug.Log("rook attacking vertically");
                takeKnight(knight);
                return true;
            }
        }
        return false;
    }

    public override bool testConflict(GameObject enemy)
    {
        int enemyX = enemy.GetComponent<EnemyScript>().boardX;
        int enemyY = enemy.GetComponent<EnemyScript>().boardY;

        if (this.transform.position.y <= attackMaxHeight && this.transform.position.y >= attackMinHeight)
        {
            if (enemyX == boardX || enemyY == boardY)
            {
                return true;
            }
        }
        return false;
    }
}
