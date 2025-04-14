using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    private Rigidbody2D rb;


	[Header("Enemy Properties")]
    public float speed;
	[SerializeField] private GameObject explosion;
	public int life;
	public int points;
	public GameObject bullet, powerUp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
		Move();
		Fire();
    }

	//Estos dos metodos se van a sobreescribir en su respectivo enemigo
	public virtual void Move() { }
	public virtual void Fire() { }

	public void TakeDamage(int damage)
	{
		life -= damage;

		if (life <= 0)
		{
			ScoreManager.Instance.AddScore(points);
			Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
			SpawnPowerUp();
			Destroy(gameObject);
		}
	}

	void SpawnPowerUp()
	{
		if (Random.Range(0, 3) == 0)  // 33,33% probabilidad de aparecer PowerUp
		{
			Instantiate(powerUp, transform.position, Quaternion.identity);
		}
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.CompareTag("Bullet"))
		{
			TakeDamage(other.GetComponent<Bullet>().bulletDamage);
			
		}
    }
	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
