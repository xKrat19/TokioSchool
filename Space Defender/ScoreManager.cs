using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance { get; private set; }
	private int hi_Score;
	private int score = 0;
	[SerializeField] public int multiply = 1;

	public bool gameOver = false;
	public GameObject panel_GameOver;
	private int player_Score = 3;
	public TextMeshProUGUI t_score_GO;
	public TextMeshProUGUI t_score, t_hiScore, t_life;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}
	void Start()
    {
		//Obtiene el puntuaje mayor guardado del nivel "nameLevel_score"
		hi_Score = PlayerPrefs.GetInt("Player_" + player_Score.ToString());
		t_hiScore.text = hi_Score.ToString("D7");
		t_score.text = score.ToString("D7");
		InvokeRepeating("Contador", 1f, 1f);
    }
    void FixedUpdate()
    {   
		if (score > hi_Score && !gameOver)
		{
			Debug.Log(score + " - " + hi_Score);
			if (player_Score > 0) //Pone en pantalla el hi score más cercano que tenga
			{
				player_Score--;
				Debug.Log("Paso al hi_score del Player_" + player_Score);
				hi_Score = PlayerPrefs.GetInt("Player_" + player_Score.ToString());
				t_hiScore.text = hi_Score.ToString("D7");
			}
			if (player_Score <= 0) //Significa que ha sobrepasado al top 1
			{
				hi_Score = score;
				t_hiScore.text = hi_Score.ToString("D7");
			}
		}
    }
	public void PlayerGameOver()
	{
		StartCoroutine(GameOver());
	}
	public IEnumerator GameOver()
	{
		Debug.Log("GAME OVER");
		gameOver = true;
		yield return new WaitForSeconds(1);
		panel_GameOver.SetActive(true);
		t_score_GO.text = score.ToString("D7");

		//Comprueba el score de la partida y checkea si se ha conseguido algun record
		if (score > hi_Score)
		{
			PlayerPrefs.SetInt("Player_" + player_Score.ToString(), score);
		} else
		{
			player_Score++;
			if (player_Score <= 3)
			{
				Debug.Log("Guardado Player_" + player_Score.ToString() + " Puntuacion: " + score);
				PlayerPrefs.SetInt("Player_" + player_Score.ToString(), score);
			}
		}
		Time.timeScale = 0f;
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadSceneAsync(0);
	}

	public void AddScore (int points)
	{
		score += points * multiply;
		t_score.text = score.ToString("D7");
	}
	public void UpdateLife(int lifes)
	{
		if (lifes < 0)
			t_life.text = "0";
		else 
			t_life.text = lifes.ToString();
	}
	private void Contador()
	{
		if (!gameOver)
			AddScore(10);
	}
}
