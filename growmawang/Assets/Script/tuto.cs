using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class tuto : MonoBehaviour
{
    [SerializeField] GameObject howtoplay;
    [SerializeField] Button bt1;
    [SerializeField] Text tx;
    [SerializeField] Sprite[] image = new Sprite[4];
    int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(count);
    }

    public void Changeimage()
    {
        if (count == 1)
        {
            bt1.image.sprite = image[1];
            tx.text = "자동으로 식물의 위치쪽으로 이동합니다.";
            count =2;
            return;
        }
        if (count == 2)
        {
            bt1.image.sprite = image[2];
            tx.text = "식물의 앞에 오면 수확 버튼을 눌러 수확합니다.";
            count = 3;
            return;
        }
        if (count == 3)
        {
            bt1.image.sprite = image[3];
            tx.text = "정확하게 클릭하지 않으면 사망합니다." +"\n" + " 다른 식물을 수확시 추가 효과가 있습니다.";
            count = 4;
            return;
        }
        else
        {
            count = 1;
            bt1.image.sprite = image[0];
            howtoplay.SetActive(false);
            tx.text = "스마트 폰의 오른쪽을 클릭하여 식물쪽으로 이동합니다.";
            return;
        }
    }
}
