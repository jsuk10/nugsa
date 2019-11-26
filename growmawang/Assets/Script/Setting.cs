using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [SerializeField] GameObject CreaditGO;
    [SerializeField] GameObject SettingGO;
    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void Ingame()
    {
        SceneManager.LoadScene("Ingame");
    }
    public void SettingMenuOn()
    {
        SettingGO.SetActive(true);
    }
    public void SettingMenuOFF()
    {
        SettingGO.SetActive(false);
    }
    public void CreaditOn()
    {
        CreaditGO.SetActive(true);
    }

    public void CreaditOFF()
    {
        CreaditGO.SetActive(false);
    }
}
