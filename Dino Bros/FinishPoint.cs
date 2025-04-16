using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.NextLevel();
        }
    }
}
