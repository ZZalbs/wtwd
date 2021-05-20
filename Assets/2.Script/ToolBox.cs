using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : MonoBehaviour
{
    public bool mode;
      
    public GameManager gm;
        
    public GameObject player;
    void Awake()
    {
        
        this.mode=false;
    }

    void Update()
    {
        if(mode)
        {
            this.transform.position = player.transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            this.mode = true;
        }
        if (collision.gameObject.tag == "Tower"&&mode)
        {
            gm.resource += 100;
            gm.resourceText.text = "Resource : " + string.Format("{0:D4}", gm.resource);
            this.transform.position = new Vector2(10, 10);
            this.gameObject.SetActive(false);
        }
    }
}
