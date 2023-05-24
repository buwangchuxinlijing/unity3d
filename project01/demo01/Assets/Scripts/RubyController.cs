using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    // Start is called before the first frame update  
    //在第一次帧更新之前调用 Start
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    // 每帧调用一次 Update
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical=Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        position.y = position.y + 3.0f * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
}
