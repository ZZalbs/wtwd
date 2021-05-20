using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour {

    public int score;
    float life=1000;

    public Image playerhpbar;

    public GameObject buttonTurn; // 좌우버튼
    public Sprite[] buttonState = new Sprite[2];
    Image buttonSprite;

    public bool isTop=false; //위에 닿았는지 체크
    public bool isBottom=false; // 뒤에 닿았는지 체크
    public bool isLeft=false; // 왼쪽인지 체크
    public bool isRight=false; // 오른쪽인지 체크


    private Touch touch; // 터치개수
    private Vector2 sPos; // 손 대는 위치
    private Vector2 fPos; // 손 떼는 위치
    private float xChange, yChange; // f포스 s포스의 차이
    public Vector2 curPos, nextPos; // 현위치, 다음위치
    public bool moveCheck=true;

    public float speed;
    public int energy;

    public GameObject gameOver; // 좌우버튼



    void Awake()
    {
        gameObject.SetActive(true);
        //tileMapLogic = tileMap.GetComponent<rayTilemap>();
        nextPos = Vector2.up * speed;
        //Move();
        playerhpbar.fillAmount = life / 1000;
        PlayerPrefs.SetInt("M_R_L", 1);

    }


    void Update()
    {
        //TouchCheck();
        Move();
    }
    
    public void Move()
    {
        curPos = transform.position;
        //TouchCheck();
        //Debug.Log(nextPos);
        transform.position = curPos + nextPos;
    }
    public void Onhit(int dmg)
    {
        life -= dmg;
    }
    /*
    void TouchCheck()
    {
        if(Time.timeScale==0)
        {
            nextPos = new Vector2(0, 0);
        }
        if (!moveCheck)
            return;
        if(Input.touchCount >0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began) // 터치가 시작되면 s포스에 첫위치 저장
            {
                sPos = Camera.main.ScreenToWorldPoint(touch.position);
            }

            if(touch.phase == TouchPhase.Ended) // 두번째 위치를 저장받아 이동
            {
                fPos = Camera.main.ScreenToWorldPoint(touch.position);
                xChange = fPos.x - sPos.x;
                yChange = fPos.y - sPos.y;
                if (Mathf.Abs(yChange) > Mathf.Abs(xChange) && yChange > 0.5 && !isTop)
                {
                    nextPos = Vector2.up * speed;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    //Debug.Log("up");
                }
                if (Mathf.Abs(yChange) > Mathf.Abs(xChange) && yChange < 0.5 && !isBottom)
                {
                    nextPos = Vector2.down * speed;
                    transform.rotation = Quaternion.Euler(0, 0,180);
                    //Debug.Log("down");
                }
                if (Mathf.Abs(yChange) < Mathf.Abs(xChange) && xChange > 0.5 && !isRight)
                {
                    nextPos = Vector2.right * speed;
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    //Debug.Log("right");
                }
                if (Mathf.Abs(yChange) < Mathf.Abs(xChange) && xChange < 0.5 && !isLeft)
                {
                    nextPos = Vector2.left * speed;
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                    //Debug.Log("left");
                }
            }
        }
        
            
    }
    */
    //임시로 만든 키마 제어

    public void Up()
    {
        nextPos = Vector2.up * speed;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void Down()
    {
        nextPos = Vector2.down * speed;
        transform.rotation = Quaternion.Euler(0, 0, 180);
    }
    public void Right()
    {
        nextPos = Vector2.right * speed;
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }
    public void Left()
    {
        nextPos = Vector2.left * speed;
        transform.rotation = Quaternion.Euler(0, 0, 270);
    }

    public void Restart()
    {
        gameOver.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "die")
        {
            life -= 100;
            playerhpbar.fillAmount = life / 1000;
            if (life <= 0)
            {
                gameObject.SetActive(false);
                gameOver.SetActive(true);
                Time.timeScale = 0;
                
            }
        }
        if (collision.gameObject.tag == "wall")
        {
            nextPos = new Vector2(0,0);
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTop = true;
                    break;
                case "Bottom":
                    isBottom = true;
                    break;
                case "Left":
                    isLeft = true;
                    break;
                case "Right":
                    isRight = true;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTop = false;
        isBottom = false;
        isLeft = false;
        isRight = false;
    }

    


}
