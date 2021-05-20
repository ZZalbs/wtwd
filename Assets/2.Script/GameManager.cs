using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    //유니티 배열 ,랜덤
    public string[] enemyType; // 적
    public Transform[] spawnPoint;// 위치
    double spawnDelay;
    public double maxDelay;
    private double realMaxDelay;
    private int hardness=1;
    public GameObject player;
    PlayerControl plc; // 플레이어 스크립트

    GameObject enemy; // 적군로봇
    Robot r; // 적군로봇 코드
    GameObject robot; // 아군로봇
    GameObject tool;
    EnemyTemplate enemyLogic;
    Rigidbody2D rigidEnemy;

    private Touch touchTile; // 타일 클릭한 터치

    int indSpawn, indPos;

    public int resource=0;

    
    public Text resourceText;

    public bool skillOn;
    int robotNum;

    public GameObject tile;    //타일맵오브젝트
    public Tilemap tilemap;    //타일맵
    Vector2 tilemapPos; // 타일맵 위치


    // Start is called before the first frame update
    void Awake()
    {
        enemyType = new string[] { "enemyS", "enemyM", "enemyL" };
        resourceText.text = "Resource : " + string.Format("{0:D4}", resource);
        skillOn = true;
        plc = player.GetComponent<PlayerControl>();
        realMaxDelay = maxDelay;
        
    }
    void Start()
    {
        StartCoroutine("toolSpawn");
    }

    // Update is called once per frames
    void Update()
    {
        Spawn();
        Reload();
        
        if (tile.activeSelf)
        {
            tileOn();
        }
    }

    void Spawn()
    {
        if (spawnDelay < maxDelay)
            return;

        indPos = Random.Range(0, 15);
        indSpawn = Random.Range(0, 3);

        switch (indSpawn)
        {
            case 0:
                StartCoroutine("SmallEnemy");  // 코루틴
                break;
            case 1:
                SpawnEnemy2();
                break;
            case 2:
                //SpawnEnemy2();
                break;
        }
        spawnDelay = 0;
    }

    void SpawnEnemy2()
    {
        enemy = ObjectManager.instance.MakeObj(enemyType[indSpawn]);
        enemy.transform.position = spawnPoint[indPos].position;  //~~라는 게임오브젝트에 복제를 합니다
        enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        rigidEnemy = enemy.GetComponent<Rigidbody2D>();
        enemyLogic = enemy.GetComponent<EnemyTemplate>();
        enemyLogic.player = player;
        EnemyMove(enemy, indPos);
    }


    void Reload() // curShotdelay 체크함
    {
        spawnDelay += Time.deltaTime; // Time.deltaTime : 시간 단위
    }

    void EnemyMove(GameObject enemy, int pos)
    {
        //적을 받음, 적의 소환위치 받음
        switch (pos)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                enemyLogic.firstMove = new Vector2(1, 0);
                //enemy.transform.Rotate(new Vector3(0, 0, 90));
                break;
            case 5:
            case 6:
            case 7:
                enemyLogic.firstMove = new Vector2(0, -1);
                //enemy.transform.Rotate(new Vector3(0, 0, 0));
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                enemyLogic.firstMove = new Vector2(-1,0);
                //enemy.transform.Rotate(new Vector3(0, 0, -90));
                break;
            case 13:
            case 14:
            case 15:
                enemyLogic.firstMove = Vector2.up;
                break;
        }
    }


    public void spawnClick()
    {

        if (skillOn)
        {
            if (resource < 200)
                return;
            skillOn = false;
            robot = ObjectManager.instance.MakeObj("robot");
            r = robot.GetComponent<Robot>();
            plc.nextPos = new Vector2(0, 0);
            plc.moveCheck = false;

            StartCoroutine("BulletTime");
        }
    }
    void spawnEnd()
    {
        Time.timeScale = 1.0f;
        tile.SetActive(false);
        plc.moveCheck = true;
        skillOn = true;
    }

    public void tileOn()
    {
        if (Input.touchCount > 0)
        {
            touchTile = Input.GetTouch(0);
            Vector3 sPos = Camera.main.ScreenToWorldPoint(touchTile.position);
            Ray ray = new Ray();// 터치 위치로 레이 생성
            ray.origin = sPos;
            ray.direction = Vector3.forward;
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 3.5f); // 레이 그리기
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero); // 맞는걸 받아옴
            if (this.tilemap == hit.transform.GetComponent<Tilemap>()) // 맞았는데 그게 타일맵일 경우, 해당 타일맵의 색깔 바꿔줌
            {
                this.tilemap.RefreshAllTiles();
                int x, y;
                x = this.tilemap.WorldToCell(ray.origin).x;
                y = this.tilemap.WorldToCell(ray.origin).y;
                Vector3Int v3 = new Vector3Int(x, y, 0);
                this.tilemap.SetTileFlags(v3, TileFlags.None);
                this.tilemap.SetColor(v3, (Color.blue));
                tilemapPos = this.tilemap.CellToWorld(v3);

                r.pos = tilemapPos;
            }
            if (touchTile.phase == TouchPhase.Ended)
            {
                spawnEnd();
            }
        }
        else
        {
            this.tilemap.RefreshAllTiles();
        }
    }



    IEnumerator SmallEnemy() // 코루틴 기본 형식
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy2();
            yield return new WaitForSeconds(1.0f); // 3개가 생성되게 해보세요!
        }
    }

    IEnumerator BulletTime()
    {
        for (int i = 1; i <= 50; i++)
        {
            Time.timeScale -= 0.02f;
            yield return new WaitForSeconds(0.0002f);
            if (i == 49) tile.SetActive(true);
        }
    }

    IEnumerator toolSpawn()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            tool = ObjectManager.instance.MakeObj("box");
            tool.transform.localScale = new Vector3(1, 1, 1);
            ToolBox t = tool.GetComponent<ToolBox>();
            t.gm = this;
            t.player = player;
            t.mode = false;
            tool.transform.position = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(-5.0f, 5.0f));
            
            
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        }
    }
}
