using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
	public void Lobby()
	{
		SceneManager.LoadScene("Lobby");
    }
    public void Ingame()
    {
        SceneManager.LoadScene("Ingame");
    }
    public void SettingMenu()
    {
        SceneManager.LoadScene("Setting");
    }
}
