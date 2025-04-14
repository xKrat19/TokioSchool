using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 5f;
	public GameObject explosion;
	public int bulletDamage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
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
