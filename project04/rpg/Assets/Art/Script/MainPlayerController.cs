using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    //物理系统刚体
    Rigidbody2D rigidbody2D;
    //移动速度因子
    float speed = 5.0f;


    //面向
    Vector2 lookPostion = new Vector2(0.0f, 0.0f);

    //动画
    Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        //水平
        float horizontal = Input.GetAxis("Horizontal");
        //垂直
        float vertical = Input.GetAxis("Vertical");

        if (!Mathf.Approximately(horizontal, 0.0f)
            || !Mathf.Approximately(vertical, 0.0f)) {

            Vector2 position = rigidbody2D.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
            //vector2 是一个值类型变量，所以修改了坐标但是不能修改到物理系统中
            rigidbody2D.MovePosition(position);


            lookPostion.Set(horizontal, vertical);
            //设置移动向量大小为动画移动速度，
            animator.SetFloat("Speed", lookPostion.magnitude);
            //标准化向量长度为1
            lookPostion.Normalize();
            //将标准化的向量分类赋值给动画x 和 y参数
            animator.SetFloat("X", lookPostion.x);
            animator.SetFloat("Y", lookPostion.y);




        }
    
    }


}
