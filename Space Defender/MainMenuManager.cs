using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI t_Player1, t_Player2, t_Player3;

    void Start()
    {
		//DefaultPlayerPref();
		Time.timeScale = 1.0f;
		t_Player1.text = "1." + PlayerPrefs.GetInt("Player_1").ToString("D7");
		t_Player2.text = "2." + PlayerPrefs.GetInt("Player_2").ToString("D7");
		t_Player3.text = "3." + PlayerPrefs.GetInt("Player_3").ToString("D7");

 	}

	public void DefaultPlayerPref()
	{
		PlayerPrefs.SetInt("Player_1", 10000);
		PlayerPrefs.SetInt("Player_2", 500);
		PlayerPrefs.SetInt("Player_3", 35);
		t_Player1.text = "1." + PlayerPrefs.GetInt("Player_1").ToString("D7");
		t_Player2.text = "2." + PlayerPrefs.GetInt("Player_2").ToString("D7");
		t_Player3.text = "3." + PlayerPrefs.GetInt("Player_3").ToString("D7");
	}
	public void NewGame()
	{
		SceneManager.LoadSceneAsync(1);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
