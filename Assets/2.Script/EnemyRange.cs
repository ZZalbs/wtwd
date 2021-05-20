using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyTemplate
{
    // Start is called before the first frame update
    void Start()
    {
        this.speed = Cspeed;
        visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(firstMove * speed * Time.deltaTime);
        if (this.visible)
        {
            LockOn();
            EnemyMove();
            if (this.targetFinal != null)
            {
                if (Vector2.Distance(this.gameObject.transform.position, this.targetFinal.transform.position) < 0.3) speed = 0;
                else speed = Cspeed;
            }
        }
        Die();
    }

    public override void EnemyMove()
    {
        if (this.targetFinal != null)
        {
            Debug.DrawRay(targetFinal.position, transform.position - targetFinal.position, Color.red, 0.1f); // 레이 그리기
            Quaternion lookRotation = Quaternion.LookRotation(targetFinal.position);
            Vector3 euler = Quaternion.FromToRotation(Vector3.up, gameObject.transform.position - targetFinal.position).eulerAngles; // 적이 있는 각도
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
            firstMove = (targetFinal.position - transform.position).normalized;
        }
    }

}
