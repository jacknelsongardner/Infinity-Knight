using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int boardX;
    public int boardY;

    public GameObject boardParent;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(boardParent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool testAttack(GameObject knight)
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
}
