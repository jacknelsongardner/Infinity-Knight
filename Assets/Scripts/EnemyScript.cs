using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int boardX;
    public int boardY;

    public GameObject boardParent;

    public float attackMaxHeight;
    public float attackMinHeight;

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

    public virtual bool testAttack(GameObject knight)
    {
        return false;
    }

    public virtual bool testConflict(GameObject enemy)
    {
        return false;
    }

    public bool testDie(GameObject knight)
    {
        int knightX = knight.GetComponent<KnightScript>().boardX;
        int knightY = knight.GetComponent<KnightScript>().boardY;

        if (knightX == boardX && knightY == boardY)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public bool testDestroy()
    {
        if (this.transform.position.y <= this.killHeight)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    

    public void takeKnight(GameObject knight)
    {
        // go to knights position
        this.transform.position = new Vector3(knight.GetComponent<Transform>().position.x, 
                                              knight.GetComponent<Transform>().position.y,
                                              this.transform.position.z);
    }
}
