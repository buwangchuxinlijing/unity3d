using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    //������� ��������ײ�����
    Rigidbody2D rigidbody2d;
    //ˮƽ����� ������ 
    float horizontal;
    //��ֱ����� ������ 
    float vertical;

    //���Ѫ��
    public int maxHealth = 5;

    //��ǰѪ��
    private int currentHealth = 0;

    //c# ��ȡ˽�б����ı��� get����
    public int health { get { return this.currentHealth; } }


    //�ƶ�����
    public float speed = 5.0f;

    //�޵�ʱ��
    public float timeInvincible = 2.0f;
    //�Ƿ��Ѫ
    private bool isInvincible = true ;
    //�޵�ʣ��ʱ��
    float invincibleTimer;

    //��׼������ ������
    Vector2 lookDirection = new Vector2(1, 0);
    //��������
    Animator animator;

    //����һ����ҩԤ�Ƽ�
    public GameObject projectilePrefab;

    //��Ƶ��Դ
    AudioSource audioSource;

    //ruby�Ų���
    public AudioClip footstepsClip;

    //�Ų�������ʱ�� �ͽŲ�����Ƶʱ�����
    float footstepsMisicTime = 1.25f ;
    //�Ų�������ʱ
    float footstepsTime;

    // Start is called before the first frame update  
    //�ڵ�һ��֡����֮ǰ���� Start
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        
        //��ʼ��Ѫ��
        //this.currentHealth = this.maxHealth;
        this.currentHealth = this.maxHealth;

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        footstepsTime = 0.0f;
    }

    // Update is called once per frame
    // ÿ֡����һ�� Update
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical=Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();

            footstepsTime -= Time.deltaTime;
            if (footstepsTime > 0.0f)
            {
                ;
            }
            else
            {
                footstepsTime = footstepsMisicTime;
                //���ŽŲ���
                PlayMisicStartFootRuby();
            }
        }
        else
        {
            //ֹͣ�ƶ�������ֹͣ���ŽŲ���
            PlayMisicStopFootRuby();
        }






        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer < 0) 
        {
            this.isInvincible = true;
        }

       
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            this.Launch();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //�ڵ�ǰruby����ײ����Ϸ�0.2����λ ����ΪĿ�ⷽ�� ��������1.5����λ���Ƿ���NPCͼ����ײ����
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
                //NPC ��Ϸ����
                GameObject npc = hit.collider.gameObject;
                NonPlayerCharacter text=npc.GetComponent<NonPlayerCharacter>();
                text.Display();
            }
            

        }



    }

    //����ÿ֡����һ�Σ�
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    //�����ӵ�
    void Launch() 
    {

        GameObject cogBullet;
        //����Ԥ�Ƽ�ʵ��������Ϸ����
        /*if (lookDirection.y < 0)
        {
          //����ͼ�㲻��ײ���û��Ҫ�ж���
           cogBullet = Instantiate(
            projectilePrefab,
            rigidbody2d.position + Vector2.down * 0.2f,
            Quaternion.identity);
        }*/ 
        cogBullet = Instantiate(
        projectilePrefab,
        rigidbody2d.position + Vector2.up * 0.5f,
        Quaternion.identity);
       
        //������Ϸ�����ȡ��ҩ�ű�����
        Projectile projectile=cogBullet.GetComponent<Projectile>();
        //�ӵ���������
        this.PlayMisic(projectile.projectileClip);
        //���õ�ҩ�ű���Lunch���� ,ʹ��������Ϊ����ʹ��300N ����
        projectile.Launch(lookDirection, 500);

        //����
        animator.SetTrigger("Launch");

    }


    public void ChangeHealth(int amount)
    {
        if (amount < 0) 
        {
            //�޵�ʱ�䲻��Ѫ �����޵вſ�Ѫ
            if ( isInvincible)
            {
                this.currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
                Debug.Log("��ǰѪ��: " + currentHealth + "/" + maxHealth);
               


                this.isInvincible = false;
                this.invincibleTimer = this.timeInvincible;

                //Ѫ��UI
                UIHealthBar uIHealthBar = this.gameObject.GetComponent<UIHealthBar>();
                uIHealthBar.SetValue((float)currentHealth / maxHealth);

                //���˶���
                animator.SetTrigger("Hit");
            }
        }
        else 
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log("��ǰѪ��: " + currentHealth + "/" + maxHealth);

            //Ѫ��UI
            UIHealthBar uIHealthBar = this.gameObject.GetComponent<UIHealthBar>();
            uIHealthBar.SetValue((float)currentHealth / maxHealth);
        }

       
        
    }

    public bool CurrentHealthIsMax() 
    {
        return this.currentHealth == 5;
    }

    //�������ڲ���һ����Ƶ��������������������ͬ���ǣ�
    //������ͬʱ���Ŷ����Ƶ����������ȴ�֮ǰ�Ĳ�����ɡ�
    //�������Ҫͬʱ���Ŷ����ƵЧ���ǳ�����
    public void PlayMisic(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }


    //����Ruby�Ų���Ч
    void PlayMisicStartFootRuby()
    {
        audioSource.clip = footstepsClip; // ����Ҫ���ŵ���Ƶ����
        audioSource.Play();
    }
    //ֹͣRuby�Ų���Ч
    void PlayMisicStopFootRuby() { 
        audioSource.clip = null;
    }

}
