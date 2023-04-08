using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // abstractly, which board tile they are on
    public int boardX;
    public int boardY;

    // the object holding all the tiles together
    public GameObject boardParent;

    // max and min heights where they can attack. any lower (or higher) than their values and they will not be able to attack the knight
    public float attackMaxHeight;
    public float attackMinHeight;

    // when to destroy this object (after going south of the camera)
    public float killHeight;

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

    // abstract fo testingAttack
    public virtual bool testAttack(GameObject knight)
    {
        return false;
    }

    // abstract fo testingConflict
    public virtual bool testConflict(GameObject enemy)
    {
        return false;
    }

    // testing if this piece should be killed
    public bool testDie(GameObject knight)
    {
        int knightX = knight.GetComponent<KnightScript>().boardX;
        int knightY = knight.GetComponent<KnightScript>().boardY;

        // if this is on the same board tile as the knight, we destroy
        if (knightX == boardX && knightY == boardY)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    // testing to see if we should naturally destroy ourselves (in case we've gone off the camera)
    public bool testDestroy()
    {
        float actualHeight = this.transform.TransformPoint(this.transform.position).y;

        // if we're too low, destroy
        if (actualHeight <= this.killHeight)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    // this is the "physical" act of jumping on top of the player
    public void takeKnight(GameObject knight)
    {
        // go to knights position
        this.transform.position = new Vector3(knight.GetComponent<Transform>().position.x, 
                                              knight.GetComponent<Transform>().position.y,
                                              this.transform.position.z);
    }
}
