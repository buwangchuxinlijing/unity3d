using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class EnemyController : MonoBehaviour
{
    //������� ��������ײ�����
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

        //x�������ƶ�
        if (vertical)
        {
            position.x = position.x + speed * Time.deltaTime;
        }
        //x�ᷴ���ƶ�
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

    //������������
    Animator animator;


    //�Ƿ���
    bool broken=true;

    //����ϵͳ����
    public ParticleSystem  smokeEffect;

    //�޸��ӵ���ײ����
    public AudioClip clip;

    //��Ƶ��Դ
    AudioSource audioSource;

    // �ڵ�һ��֡����֮ǰ���� Start
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
            //��ֱ�ƶ�
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            //ˮƽ�ƶ�
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    //OnCollisionEnter2D ��ײ
    //OnCollisionStay2D ������ײ
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
            //����ϵͳģ�⣨��װ��ɾ�������ϵͳ���Ὣ������Ϊ��ײ���󣬲����޺õĻ����˲�������ֹ�ɵ���Ҳ�����˺�����
            rigidbody2D.simulated = false;
            animator.SetTrigger("Fixed");

            //ֹͣ������Ч
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
