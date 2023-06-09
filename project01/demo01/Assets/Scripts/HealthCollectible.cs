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

                //�������� ͨ��ruby����Ƶ��Դ���ŵ�ǰ��Ƶ�����ڵ�ǰ��Ϸ���󴴽���Ƶ��Դ����Ϊ��ǰ����ᱻ���٣�
                //��������Ļ�ÿ����Ҫ������Ƶ��Դ�� ����ΪʲôҪʹ��clip�����أ�
                controller.PlayMisic(clip);

            }


        }
    }
}
