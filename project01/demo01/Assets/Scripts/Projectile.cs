using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2D;

   
    float maxFlightTime = 3.0f;


    
    //子弹发射音频
    public AudioClip projectileClip;

    // 在第一次帧更新之前调用
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, maxFlightTime);

    }

    //对象创建时调用
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


        //子弹轨迹
        rigidbody2D.AddForce(direction * force);
       
    }

    void OnCollisionEnter2D(Collision2D collistion) 
    {

        EnemyController enemyController =collistion.gameObject.GetComponent<EnemyController>();
        if (null != enemyController) 
        {
            //修复碰撞对象
            enemyController.Fix();
        }

        Projectile otherProjectile = collistion.gameObject.GetComponent<Projectile>();
        if (null != otherProjectile) 
        {
            ;
            //碰撞的是自己弹药 自己不抵消
        }
        else
        {
            


            //发生碰撞后删除子弹
            Destroy(this.gameObject);
        }
        
        //Destroy(collistion.gameObject);
        //Destroy(this.gameObject);
    }

}
