using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI t_record;
	private void Start()
	{
		if (t_record != null)
		{
			float Hi_Score = PlayerPrefs.GetFloat("Hi_Score");
			t_record.text = "Record: " + Hi_Score.ToString("F2") + "m";
		}
	}

	public void LoadScene(int level)
	{
		SceneManager.LoadSceneAsync(level);
	}
	
	public void ExitApplication()
	{
		Application.Quit();
	}
}
