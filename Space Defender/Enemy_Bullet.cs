using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    float speed = 3f;

	public GameObject explosion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
