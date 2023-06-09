using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2D;

   
    float maxFlightTime = 3.0f;


    
    //�ӵ�������Ƶ
    public AudioClip projectileClip;

    // �ڵ�һ��֡����֮ǰ����
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, maxFlightTime);

    }

    //���󴴽�ʱ����
    void Awake() 
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       

        

    }

    void FixedUpdate() 
    {
        
    }

    public void Launch(Vector2 direction,int force) 
    {


        //�ӵ��켣
        rigidbody2D.AddForce(direction * force);
       
    }

    void OnCollisionEnter2D(Collision2D collistion) 
    {

        EnemyController enemyController =collistion.gameObject.GetComponent<EnemyController>();
        if (null != enemyController) 
        {
            //�޸���ײ����
            enemyController.Fix();
        }

        Projectile otherProjectile = collistion.gameObject.GetComponent<Projectile>();
        if (null != otherProjectile) 
        {
            ;
            //��ײ�����Լ���ҩ �Լ�������
        }
        else
        {
            


            //������ײ��ɾ���ӵ�
            Destroy(this.gameObject);
        }
        
        //Destroy(collistion.gameObject);
        //Destroy(this.gameObject);
    }

}
