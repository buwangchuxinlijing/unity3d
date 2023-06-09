using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    //刚体变量 （物理碰撞体积）
    Rigidbody2D rigidbody2d;
    //水平方向键 键入量 
    float horizontal;
    //垂直方向键 键入量 
    float vertical;

    //最大血量
    public int maxHealth = 5;

    //当前血量
    private int currentHealth = 0;

    //c# 获取私有变量的变量 get方法
    public int health { get { return this.currentHealth; } }


    //移动倍数
    public float speed = 5.0f;

    //无敌时间
    public float timeInvincible = 2.0f;
    //是否扣血
    private bool isInvincible = true ;
    //无敌剩余时间
    float invincibleTimer;

    //标准化向量 代表方向
    Vector2 lookDirection = new Vector2(1, 0);
    //动画参数
    Animator animator;

    //持有一个弹药预制件
    public GameObject projectilePrefab;

    //音频资源
    AudioSource audioSource;

    //ruby脚步声
    public AudioClip footstepsClip;

    //脚步声存在时间 和脚步声音频时间相等
    float footstepsMisicTime = 1.25f ;
    //脚步声倒计时
    float footstepsTime;

    // Start is called before the first frame update  
    //在第一次帧更新之前调用 Start
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        
        //初始化血量
        //this.currentHealth = this.maxHealth;
        this.currentHealth = this.maxHealth;

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        footstepsTime = 0.0f;
    }

    // Update is called once per frame
    // 每帧调用一次 Update
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
                //播放脚步声
                PlayMisicStartFootRuby();
            }
        }
        else
        {
            //停止移动后立马停止播放脚步声
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
            //在当前ruby的碰撞体的上方0.2个单位 方向为目光方向 ，测试在1.5个单位内是否有NPC图层碰撞对象
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
                //NPC 游戏对象
                GameObject npc = hit.collider.gameObject;
                NonPlayerCharacter text=npc.GetComponent<NonPlayerCharacter>();
                text.Display();
            }
            

        }



    }

    //刚体每帧调用一次，
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    //发射子弹
    void Launch() 
    {

        GameObject cogBullet;
        //根据预制件实例化出游戏对象
        /*if (lookDirection.y < 0)
        {
          //开启图层不碰撞后就没必要判断了
           cogBullet = Instantiate(
            projectilePrefab,
            rigidbody2d.position + Vector2.down * 0.2f,
            Quaternion.identity);
        }*/ 
        cogBullet = Instantiate(
        projectilePrefab,
        rigidbody2d.position + Vector2.up * 0.5f,
        Quaternion.identity);
       
        //根据游戏对象获取弹药脚本对象
        Projectile projectile=cogBullet.GetComponent<Projectile>();
        //子弹发射声音
        this.PlayMisic(projectile.projectileClip);
        //调用弹药脚本的Lunch方法 ,使用面向作为方向，使出300N 的力
        projectile.Launch(lookDirection, 500);

        //动画
        animator.SetTrigger("Launch");

    }


    public void ChangeHealth(int amount)
    {
        if (amount < 0) 
        {
            //无敌时间不扣血 过了无敌才扣血
            if ( isInvincible)
            {
                this.currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
                Debug.Log("当前血量: " + currentHealth + "/" + maxHealth);
               


                this.isInvincible = false;
                this.invincibleTimer = this.timeInvincible;

                //血量UI
                UIHealthBar uIHealthBar = this.gameObject.GetComponent<UIHealthBar>();
                uIHealthBar.SetValue((float)currentHealth / maxHealth);

                //受伤动画
                animator.SetTrigger("Hit");
            }
        }
        else 
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log("当前血量: " + currentHealth + "/" + maxHealth);

            //血量UI
            UIHealthBar uIHealthBar = this.gameObject.GetComponent<UIHealthBar>();
            uIHealthBar.SetValue((float)currentHealth / maxHealth);
        }

       
        
    }

    public bool CurrentHealthIsMax() 
    {
        return this.currentHealth == 5;
    }

    //方法用于播放一个音频剪辑，但与其他方法不同的是，
    //它可以同时播放多个音频剪辑而无需等待之前的播放完成。
    //这对于需要同时播放多个音频效果非常有用
    public void PlayMisic(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }


    //开启Ruby脚步音效
    void PlayMisicStartFootRuby()
    {
        audioSource.clip = footstepsClip; // 设置要播放的音频剪辑
        audioSource.Play();
    }
    //停止Ruby脚步音效
    void PlayMisicStopFootRuby() { 
        audioSource.clip = null;
    }

}
