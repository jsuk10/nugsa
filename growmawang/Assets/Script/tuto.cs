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
    [SerializeField] Sprite[] image = new Sprite[4];
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Changeimage()
    {
        if (count < 3)
            bt1.image.sprite = image[1+count++];
        else
        {
            count = 0;
            bt1.image.sprite = image[0];
            howtoplay.SetActive(false);
        }
        Debug.Log("실행중");
    }
}
