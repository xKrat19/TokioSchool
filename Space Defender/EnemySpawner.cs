using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]private int round = 0;

	[SerializeField] GameObject[] pref_enemy;
	//Guardo la posicion del ultimo enemigo para no solapar enemigos
    private Vector2 lastSpawnedPos;

	public List<GameObject> spawnedEnemy  = new List<GameObject>();
	private bool endLevel = false;
	public bool emptyLevel = true;

    private void Start()
    {
		NewEnemyRound();
		InvokeRepeating("NewEnemyRound", 30f, 9f);
		Debug.Log("END GAME");
    }

	private void Update()
	{
		if (endLevel && !emptyLevel) 
		{
			emptyLevel = true;
			foreach (GameObject go in spawnedEnemy) 
			{
				if (go != null)
					emptyLevel = false;
			}
		}
		if (endLevel && emptyLevel)
		{
			StartCoroutine(ScoreManager.Instance.GameOver());
		}
	}
	
	public void EndLevel() //Funcion para el timeline de la animacion
	{
		endLevel = true;
	}

	private void AddEnemy(GameObject enemy)
	{
		spawnedEnemy.Add(enemy);
	}

	public void NewEnemyRound()
	{
		round++;
		int i = Random.Range(2, 10);

		//Spawnea un enemigo cada x(0.5"/1.5") tiempo i veces;
		InvokeRepeating("SpawnEnemy", Random.Range(0.5f, 1.5f),	i);
	}
	void SpawnEnemy()
	{
		AddEnemy(Instantiate(ChooseEnemy(), ChoosePosition(), Quaternion.identity));
	}

	private GameObject ChooseEnemy()
	{
		return pref_enemy[Random.Range(0, 3)];
	}

	private Vector2 ChoosePosition()
	{
		Vector2 pos;
		pos.x = (int)Random.Range(-7,7);
		pos.y = 4.5f;
		return pos;
	}
}
