using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    
    public RectTransform Background;
    public RectTransform Cursor;
    public GameObject Player;
    public GameObject hpbar;
 
    float radius = 0, speed = 0.04f;
    bool isTouch = false;
    Vector3 movePosition = new Vector3(0, 0, 0);
    Vector2 value = new Vector2(0, 0);
    Vector2 curPos ,nextPos;

    void Start()
    {
        radius = Background.rect.width * 0.5f;
        nextPos = new Vector2(0, 0);
        nextPos = Vector2.up * speed;
    }

    void Update()
    {
        movePlayer();
        hpbar.transform.position = Player.transform.position-new Vector3(0,0.35f,0);
    }
    
    public void movePlayer()
    {
        curPos = Player.transform.position;
        int angle = 0;

        if(movePosition.x==0 && movePosition.y == 0)
        {
            nextPos = Vector2.up * 0;
        }

        if (movePosition.x == 0)
        {
            if (movePosition.y > 0)
            {
                angle = 0;
                nextPos = Vector2.up * speed;
            }
            else if (movePosition.y < 0)
            {
                angle = 180;
                nextPos = Vector2.down * speed;
            }
        }
        else if (movePosition.y == 0)
        {
            if (movePosition.x < 0)
            {
                angle = 90;
                nextPos = Vector2.left * speed;
            }
            else if (movePosition.x < 0)
            {
                angle = 270;
                nextPos = Vector2.right * speed;
            }
        }
        else if (movePosition.x < 0)
        {
            if (movePosition.y > 0)
            {
                if (movePosition.x * -1 == movePosition.y || movePosition.x * -1 < movePosition.y)
                {
                    angle = 0;
                    nextPos = Vector2.up * speed;
                }
                else if (movePosition.x * -1 > movePosition.y)
                {
                    angle = 90;
                    nextPos = Vector2.left * speed;
                }

            }
            else if (movePosition.y < 0)
            {
                if (movePosition.x  == movePosition.y || movePosition.x  < movePosition.y)
                {
                    angle = 90;
                    nextPos = Vector2.left * speed;
                }
                else if (movePosition.x  > movePosition.y)
                {
                    angle = 180;
                    nextPos = Vector2.down * speed;
                }
                
            }
        }
        else if (movePosition.x > 0)
        {
            if (movePosition.y > 0)
            {
                if (movePosition.x== movePosition.y || movePosition.x < movePosition.y)
                {
                    angle = 0;
                    nextPos = Vector2.up * speed;
                }
                else if (movePosition.x> movePosition.y)
                {
                    angle = 270;
                    nextPos = Vector2.right * speed;
                }
            }
            else if (movePosition.y < 0)
            {
                if (movePosition.x == movePosition.y*-1 || movePosition.x < movePosition.y*-1)
                {
                    angle = 180;
                    nextPos = Vector2.down * speed;
                }
                else if (movePosition.x > movePosition.y*-1)
                {
                    angle = 270;
                    nextPos = Vector2.right * speed;
                }
            }
        }
        if (isTouch)
            Player.transform.rotation = Quaternion.Euler(0, 0, angle);
        Player.transform.position = curPos + nextPos;
    }
    public void OnDrag(PointerEventData eventData)
    {
        value = eventData.position - new Vector2(100, 100); //방향벡터 계산

        value = Vector2.ClampMagnitude(value, radius); //커서 가두기 : value 크기
        Cursor.localPosition = value;

        value = value.normalized; //단위벡터 변환
        movePosition = new Vector3(value.x * speed * Time.deltaTime, value.y * speed * Time.deltaTime, 0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        Cursor.localPosition = new Vector3(0, 0, 0);
        movePosition = Vector3.zero;
    }
}