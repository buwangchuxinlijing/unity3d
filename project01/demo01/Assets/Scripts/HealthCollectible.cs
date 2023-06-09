using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{

    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {

            if (! controller.CurrentHealthIsMax()) {
                controller.ChangeHealth(1);
                Destroy(gameObject);

                //播放音乐 通过ruby的音频资源播放当前音频，不在当前游戏对象创建音频资源是因为当前对象会被销毁，
                //如果创建的话每个都要创建音频资源。 但是为什么要使用clip对象呢？
                controller.PlayMisic(clip);

            }


        }
    }
}
