using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class EnemyController : MonoBehaviour
{
    //刚体变量 （物理碰撞体积）
    Rigidbody2D rigidbody2D;

    public float changeTime = 3.0f;

    public float speed = 5.0f;

    float timer;

    //int direction = 1;

    public bool vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        vertical = true;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            vertical = !vertical;
            timer = changeTime;
        }

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        //x轴正向移动
        if (vertical)
        {
            position.x = position.x + speed * Time.deltaTime;
        }
        //x轴反向移动
        else
        {
            position.x = position.x - speed * Time.deltaTime;
        }
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

}*/

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    //动画参数对象
    Animator animator;


    //是否损坏
    bool broken=true;

    //粒子系统对象
    public ParticleSystem  smokeEffect;

    //修复子弹碰撞声音
    public AudioClip clip;

    //音频资源
    AudioSource audioSource;

    // 在第一次帧更新之前调用 Start
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {


        if (!broken) {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }


        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction; 
            //垂直移动
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            //水平移动
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    //OnCollisionEnter2D 碰撞
    //OnCollisionStay2D 持续碰撞
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix() 
    {
        if (this.broken) {

            this.PlayMisic();

            this.broken = false;
            //物理系统模拟（假装）删除，因此系统不会将刚体视为碰撞对象，并且修好的机器人不会再阻止飞弹，也不能伤害主角
            rigidbody2D.simulated = false;
            animator.SetTrigger("Fixed");

            //停止烟雾特效
            if (null != this.smokeEffect) 
            {
                this.smokeEffect.Stop();
            }

            
        }
    }

     void PlayMisic()
    {
        audioSource.PlayOneShot(clip);
    }
}
