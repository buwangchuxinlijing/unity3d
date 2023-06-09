using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{

    //遮罩对象
    public Image mask;
    float originalSize;

    //实例化当前对象



    // Start is called before the first frame update
    void Start()
    {
        //获取屏幕上的遮罩大小
        originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //使用 SetSizeWithCurrentAnchors 从代码中设置大小和锚点
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
