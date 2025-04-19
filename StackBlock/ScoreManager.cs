using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance { get; private set; }

	[Header("Score In Game")]
	[SerializeField] GameObject p_Score;
	[SerializeField] TextMeshProUGUI t_score;
	private float score;

	[Header("GameOver")]
	[SerializeField] GameObject p_GameOver;
	[SerializeField] TextMeshProUGUI t_MaxScore;
	[SerializeField] GameObject t_NewRecord;
	private bool isGameOver = false;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}


	public void SetScore(float i)
	{
		float lastScore = score;
		score = i;

		StartCoroutine(Contador(lastScore, score, .5f, t_score));

	}
	public void SetGameOver(bool b)
	{
		isGameOver = b;
		p_Score.SetActive(false);
		p_GameOver.SetActive(true);

		StartCoroutine(Contador(0, score, 1.5f, t_MaxScore));
		float record = PlayerPrefs.GetFloat("Hi_Score");

		if (score > record)
		{
			t_NewRecord.SetActive(true);
			PlayerPrefs.SetFloat("Hi_Score", score);
		}
	}
	public bool GetIsGameOver()
	{
		return isGameOver;
	}

	IEnumerator Contador(float in_score, float out_score, float duration, TextMeshProUGUI text) 
	{
		float t = 0f;

		while (t < 1f) 
		{
			t += Time.deltaTime / duration;
			in_score = Mathf.Lerp(in_score, out_score, t);
			text.text = in_score.ToString("F2") + "m";
			Debug.Log(text.name + ": " + text.text);
			yield return null;
		}
		text.text = in_score.ToString("F2") + "m";
	}

}
