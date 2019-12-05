using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    [SerializeField] Slider Soundbar;
    [SerializeField] AudioSource audio;

    [SerializeField] public float Volum = 1f;

    public void SoundSlider() {
        audio.volume = Soundbar.value;
        Volum = Soundbar.value;
        PlayerPrefs.SetFloat("SoundVolum", Volum) ;


    }
    // Start is called before the first frame update
    void Start()
    {
        Volum = PlayerPrefs.GetFloat("SoundVolum",1f);
        Soundbar.value = Volum;
        audio.volume = Soundbar.value;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Volum);
        SoundSlider();
    }
}
