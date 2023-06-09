using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    //����ϵͳ����
    Rigidbody2D rigidbody2D;
    //�ƶ��ٶ�����
    float speed = 5.0f;


    //����
    Vector2 lookPostion = new Vector2(0.0f, 0.0f);

    //����
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
        //ˮƽ
        float horizontal = Input.GetAxis("Horizontal");
        //��ֱ
        float vertical = Input.GetAxis("Vertical");

        if (!Mathf.Approximately(horizontal, 0.0f)
            || !Mathf.Approximately(vertical, 0.0f)) {

            Vector2 position = rigidbody2D.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
            //vector2 ��һ��ֵ���ͱ����������޸������굫�ǲ����޸ĵ�����ϵͳ��
            rigidbody2D.MovePosition(position);


            lookPostion.Set(horizontal, vertical);
            //�����ƶ�������СΪ�����ƶ��ٶȣ�
            animator.SetFloat("Speed", lookPostion.magnitude);
            //��׼����������Ϊ1
            lookPostion.Normalize();
            //����׼�����������ำֵ������x �� y����
            animator.SetFloat("X", lookPostion.x);
            animator.SetFloat("Y", lookPostion.y);




        }
    
    }


}
