using UnityEngine;

public class Border : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Box"))
		{
			ScoreManager.Instance.SetGameOver(true);
		}
	}

	private	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Box"))
		{
			Destroy(other.gameObject);
		}
	}
}
