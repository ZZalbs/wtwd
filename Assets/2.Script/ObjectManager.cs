using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트풀링 함수
/// </summary>




public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public GameObject enemySPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemyLPrefab;
    public GameObject robotPrefab;
    public GameObject bulletPrefab;
    public GameObject boxPrefab;
    public GameObject bulletEnemyPrefab;
    /*
     * public GameObject bulletPlayerBPrefab;
    public GameObject bulletPlayerCPrefab;
    public GameObject bulletEnemyBPrefab;
    */

    GameObject[] enemyS;
    GameObject[] enemyM;
    GameObject[] enemyL;
    GameObject[] robot;
    GameObject[] bullet;
    GameObject[] box;
    GameObject[] bulletEnemy;

    /*
    GameObject[] bulletPlayerB;
    GameObject[] bulletPlayerC;
    
    GameObject[] bulletEnemyB;
    */

    GameObject[] targetPool; // 풀링할 타겟 설정

    void Awake()
    {
        if(instance != this)
            instance = this;
        //else if (instance != this)
            //Destroy(gameObject);
        enemyS = new GameObject[20];
        enemyM = new GameObject[10];
        enemyL = new GameObject[10];
        robot = new GameObject[10];
        bullet = new GameObject[200];
        box = new GameObject[20];
        bulletEnemy = new GameObject[200];
        /*bulletPlayerB = new GameObject[200];
        bulletPlayerC = new GameObject[200];
        
        bulletEnemyB = new GameObject[200];
        */
        StartCoroutine("Generate");
        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }


    IEnumerator Generate()
    {

        for (int i = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].SetActive(false);
            //yield return new WaitForSeconds(0.005f);
        }

        for (int i = 0; i < enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
            //yield return new WaitForSeconds(0.005f);
        }

        for (int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
            //yield return new WaitForSeconds(0.005f);
        }
        for (int i = 0; i < robot.Length; i++)
        {
            robot[i] = Instantiate(robotPrefab);
            robot[i].SetActive(false);
            //yield return new WaitForSeconds(0.005f);
        }
        
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i] = Instantiate(bulletPrefab);
            bullet[i].SetActive(false);
            //yield return new WaitForSeconds(0.005f);
        }
        for (int i = 0; i < box.Length; i++)
        {
            box[i] = Instantiate(boxPrefab);
            box[i].SetActive(false);
            //yield return new WaitForSeconds(0.005f);
        }
        for (int i = 0; i < bulletEnemy.Length; i++)
        {
            bulletEnemy[i] = Instantiate(bulletEnemyPrefab);
            bulletEnemy[i].SetActive(false);
            //yield return new WaitForSeconds(0.005f);
        }
        /*
        for (int i = 0; i < bulletPlayerB.Length; i++)
        {
            bulletPlayerB[i] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[i].SetActive(false);
            yield return new WaitForSeconds(0.005f);
        }

        for (int i = 0; i < bulletPlayerC.Length; i++)
        {
            bulletPlayerC[i] = Instantiate(bulletPlayerCPrefab);
            bulletPlayerC[i].SetActive(false);
            yield return new WaitForSeconds(0.005f);
        }


        for (int i = 0; i < bulletEnemyB.Length; i++)
        {
            bulletEnemyB[i] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[i].SetActive(false);
            yield return new WaitForSeconds(0.005f);
        }
        */
        yield return new WaitForSeconds(0.005f);
    }


    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "enemyS" :
                targetPool = enemyS;
                break;
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyL":
                targetPool = enemyL;
                break;
            case "robot":
                targetPool = robot;
                break;
            case "bullet":
                targetPool = bullet;
                break;
            case "box":
                targetPool = box;
                break;
            case "bulletEnemy":
                targetPool = bulletEnemy;
                break;
                /*case "bulletPlayerB":
                    targetPool = bulletPlayerB;
                    break;
                case "bulletPlayerC":
                    targetPool = bulletPlayerC;
                    break;
                    */
        }
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }

    


}
