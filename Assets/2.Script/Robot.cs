using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Robot : MonoBehaviour
{
    public Vector2 pos;
    public bool robotOn; // 로봇 대기상태 판정

    //적 탐지용 변수
    [SerializeField] float range=0f;
    [SerializeField] float rangeShort = 0f;
    [SerializeField] LayerMask layerM = 0;
    [SerializeField] float fireRate = 0;
    public int bulletDamage = 1;
    public int shortDamage = 1;
    public GameObject shortRange;
    float curFirerate;
    Transform targetFinal = null;
    SpriteRenderer targetSprite;
    Collider2D[] searched2;

    //총알
    GameObject bulletRobot;

    //로봇 기봇 능력치
    public int hp = 50;
    public int atk;
    public int def;



    void Start()
    {
        shortRange.SetActive(false);
        robotOn = false;
        UpgradeRobot.UpgradeEverything += UpgradeHP;
        curFirerate = fireRate;
        StartCoroutine("FirstSpawn");
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            gameObject.transform.position = new Vector3(pos.x+0.25f,pos.y+0.25f,0);
        }
        searched2 = Physics2D.OverlapCircleAll(transform.position, rangeShort, layerM); // 해당 범위 내 들어오는 모든 적 콜라이더 검출
        if (searched2.Length > 0 && robotOn ==true)
        {
            StartCoroutine("Power");
            //Debug.Log("a");
        }
        if (robotOn){
            if (targetFinal != null)
            {
                Debug.DrawRay(targetFinal.transform.position,gameObject.transform.position,  Color.red, 0.1f); // 레이 그리기
                //Quaternion lookRotation = Quaternion.LookRotation(targetFinal.position);
                //Vector3 euler = Quaternion.FromToRotation(Vector3.up,gameObject.transform.position-targetFinal.position).eulerAngles; // 적이 있는 각도
                //Debug.Log(euler);
                curFirerate -= Time.deltaTime;
                if (curFirerate <= 0)
                {
                    curFirerate = fireRate;
                    bulletRobot = ObjectManager.instance.MakeObj("bullet");
                    bulletRobot.transform.position = new Vector2(transform.position.x, transform.position.y);
                    Rigidbody2D bulletRigid = bulletRobot.GetComponent<Rigidbody2D>();
                    bulletRigid.AddForce((targetFinal.position - transform.position) * 200); // 이동해야 하는 벡터값
                }
            }
        }
    }

    void UpgradeHP()
    {
        hp += 25;
    }

    void LockOn()
    {
        Collider2D[] searched = Physics2D.OverlapCircleAll(transform.position, range, layerM); // 해당 범위 내 들어오는 모든 콜라이더 검출
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
                }
            }
        }
        targetFinal = shortestEnemy;

    }

    IEnumerator FirstSpawn()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = new Color(255, 255, 255, 100);
        yield return new WaitForSeconds(1.0f);
        robotOn = true;
        sp.color = new Color(255, 255, 255, 255);
        InvokeRepeating("LockOn", 0f, 0.5f);
    }
    /*IEnumerator Attack()
    {
        
    }*/
    IEnumerator Power()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        shortRange.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        robotOn = false;
        shortRange.SetActive(false);
        sp.color = new Color(255, 255, 255,100);
        yield return new WaitForSeconds(3.0f);
        robotOn = true;
        sp.color = new Color(255, 255, 255,255);

    }
}
