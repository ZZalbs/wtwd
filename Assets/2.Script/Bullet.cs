using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    public int type;
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "enemyBorder" || collision.gameObject.layer == type)
        {  
            this.gameObject.SetActive(false); 
        }
    }
}
