using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTemplate : MonoBehaviour
{
    [HideInInspector]
    public string type;
    public int Cspeed;
    public int hp;
    public int value;
    SpriteRenderer enemyRender;
    public GameObject player;
    PlayerControl control;
    protected float curFirerate;

    protected Transform targetFinal = null;
    [SerializeField] LayerMask layerM = 0;
    [SerializeField] protected float fireRate = 0;
    public Vector2 firstMove;
    protected int speed;

    Bullet bullet = null;
    Robot rob = null;

    [SerializeField] protected bool visible;

    void Awake()
    {
        speed = Cspeed;
        visible = false;
        enemyRender = GetComponent<SpriteRenderer>();
        enemyRender.color = new Color(255, 255, 255);
        
    }
    
    void Update()
    {
        this.transform.Translate(firstMove * speed * Time.deltaTime,Space.World);
        if (this.visible)
        {
            LockOn();
            EnemyMove();
            if (this.targetFinal != null)
            {
                if (Vector2.Distance(this.gameObject.transform.position, this.targetFinal.transform.position) < 2.0) speed = 0;
                else speed = Cspeed;
            }
        }
        
        
    }
    void OnBecameVisible()
    {
            visible = true;
    }
    public abstract void EnemyMove();
        /*
        if (this.targetFinal != null)
        {
            Debug.DrawRay(targetFinal.position, transform.position - targetFinal.position, Color.red, 0.1f); // 레이 그리기
            Quaternion lookRotation = Quaternion.LookRotation(targetFinal.position);
            Vector3 euler = Quaternion.FromToRotation(Vector3.up,gameObject.transform.position-targetFinal.position).eulerAngles; // 적이 있는 각도
            gameObject.transform.rotation = Quaternion.Euler(euler);
            curFirerate -= Time.deltaTime;
            if (curFirerate <= 0)
            {
                curFirerate = fireRate;
                GameObject bulletEnemy = ObjectManager.instance.MakeObj("bulletEnemy");
                bulletEnemy.transform.position = new Vector2(transform.position.x, transform.position.y);
                Rigidbody2D bulletRigid = bulletEnemy.GetComponent<Rigidbody2D>();
                bulletRigid.AddForce((targetFinal.position - transform.position) * 200); // 이동해야 하는 벡터값
            }
            firstMove =  targetFinal.position-transform.position;
        }*/


    void Onhit(int dmg)
    {
        hp -= dmg;
    }

    public void Die()
    {
        if (hp <= 0)
            gameObject.SetActive(false);
    }
    protected void LockOn()
    {
        Collider2D[] searched = Physics2D.OverlapCircleAll(transform.position, 2, layerM); // 해당 범위 내 들어오는 모든 콜라이더 검출
        Transform shortestEnemy = null;
        if (searched.Length > 0)
        {
            float shortDistance = Mathf.Infinity; // 기준은 최댓값에서 시작
            foreach (Collider2D colTarget in searched) // 콜라이더 값 쭉 찾기.
            {
                float distance = Vector2.SqrMagnitude(transform.position - colTarget.transform.position); // 거리 = 거리계산함수(벡터값의 차)
                if (shortDistance > distance) // 거리가 최솟값 거리보다 작으면
                {
                    shortDistance = distance; // 최솟값을 지금의 거리로 바꾸고
                    shortestEnemy = colTarget.transform; // 이번 콜라이더가 가장 가까운 적
                    GameObject K= colTarget.GetComponent<GameObject>();
                }
            }
        }
        targetFinal = shortestEnemy;
    }




    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            bullet = collision.gameObject.GetComponent<Bullet>();
            Onhit(bullet.dmg);
        }
        if (collision.gameObject.tag == "enemyBorder")
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "robotShort")
        {
            rob = collision.gameObject.GetComponentInParent<Robot>();
            Onhit(rob.shortDamage);
            enemyRender.color = new Color(0, 255, 255);
        }
    }
}
