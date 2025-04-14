using UnityEngine;
	public enum PowerUp_Type { 
		Heal,
		Shield,
		TripleShot,
		Strength,
		x2
	}

public class PowerUp : MonoBehaviour
{
	public PowerUp_Type type;

	private float speed = 2f;

	Animator anim;

	private void Start()
	{
		type = (PowerUp_Type)Random.Range(0, 4);
		anim = GetComponent<Animator>();
		SpritePowerUp(type);
	}

	private void Update()
	{
		GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1 * speed);
	}

	//Selecciona el sprite correspondiente con el powerup escogido.
	private void SpritePowerUp(PowerUp_Type type)
	{
		anim.Play(type.ToString());
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) 
		{
			other.GetComponent<PlayerController>().PowerUpON(type);
			Debug.Log("PowerUpRecogido");
			Destroy(gameObject);
		}
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
