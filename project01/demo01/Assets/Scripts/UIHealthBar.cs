using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{

    //���ֶ���
    public Image mask;
    float originalSize;

    //ʵ������ǰ����



    // Start is called before the first frame update
    void Start()
    {
        //��ȡ��Ļ�ϵ����ִ�С
        originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ʹ�� SetSizeWithCurrentAnchors �Ӵ��������ô�С��ê��
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
