using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Componentes del Player
    private Rigidbody2D rb;
	private Animator anim;
	[SerializeField] private GameObject specialAtack;
	[SerializeField] private GameObject explosion;

    //Disparos
    [SerializeField] private GameObject bullet;
	private int bullet_amount = 1;
    public float fireRate; //Cadencia de disparo
    //Tiempo en el que se puede disparar, sirve para limitar el tiempo entre disparos
    float nextFireTime;
	bool canSpecialFire = true;

    //Mov. del player
    private float dir_h, dir_v;
    public float speed = 5f;

	public int life = 3;
	bool canHit = true;


	private float powerUpTime = 10f;
	//Var PowerUp Escudo
	[SerializeField] private GameObject shield;
	public float shieldTimeRemaining = 0f;
	private bool shieldActive = false;

	//Var PowerUp TripleShoot
	public float tripleShootTimeRemaining = 0f;
	private bool tripleShootActive = false;

	//Var PowerUp Strength
	public float strengthTimeRemaining = 0f;
	private bool strengthActive = false;

	//Var PowerUp x2
	public float x2TimeRemaining = 0f;
	private bool x2Active = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		ScoreManager.Instance.UpdateLife(life);
	}

    void FixedUpdate()
    {
        //Movmiento del player
        #region
        dir_h = Input.GetAxis("Horizontal");
        dir_v = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(dir_h * speed, dir_v * speed);
        #endregion

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Fire(bullet_amount);
            nextFireTime = Time.time + fireRate;
        }

		if (Input.GetKey(KeyCode.LeftControl) && canSpecialFire)
		{
			Fire(32);
			StartCoroutine(ReloadSpecialFire());
		}
			
	}

    void Fire(int amount)
    {
		if (amount == 1)
			Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.identity);
		if (amount == 3)
		{
			Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.identity);
			Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.Euler(0, 0, 5));
			Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.Euler(0, 0, -5));
		}
		else
		{
			for (int i = 0; i <= amount; i++)
			{
				float angle = i * (360 / amount);

				Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.Euler(0, 0, angle));
			}
		}
    }

	public void PowerUpON(PowerUp_Type type)
	{
		switch (type)
		{
			case PowerUp_Type.Heal:
				life++;
				ScoreManager.Instance.UpdateLife(life);
				break;
			case PowerUp_Type.Shield:
				if (!shieldActive)
				{
					shieldTimeRemaining = powerUpTime;
					StartCoroutine(PowerUpShield());
				}
				else
					shieldTimeRemaining += powerUpTime;
				break;
			case PowerUp_Type.TripleShot:
				if (!tripleShootActive)
				{
					tripleShootTimeRemaining = powerUpTime;
					StartCoroutine(PowerUpTripleShot());
				}
				else
					tripleShootTimeRemaining += powerUpTime;
				break;
			case PowerUp_Type.Strength:
				if (!strengthActive)
				{
					strengthTimeRemaining = powerUpTime;
					StartCoroutine(PowerUpStrength());
				}
				else
					strengthTimeRemaining += powerUpTime;
				break;
			case PowerUp_Type.x2:
				if (!x2Active)
				{
					x2TimeRemaining = powerUpTime;
					StartCoroutine(PowerUpx2());
				}
				else
					x2TimeRemaining += powerUpTime;
				break;
		}
	}

	#region Logica de los PowerUps
	IEnumerator PowerUpShield()
	{
		shieldActive = true;
		shield.SetActive(true);
		canHit = false;

		while (shieldTimeRemaining > 0)
		{
			if (shieldTimeRemaining == 1)
			{

			}//Parpadeo del escudo porque queda poco tiempo;
			shieldTimeRemaining -= Time.deltaTime; //Quita un "segundo al tiempo restante"
			yield return null; //La corutina pasa al siguiente frame
		}

		shieldActive = false;
		shield.SetActive(false);
		canHit = true;
		shieldTimeRemaining = 0f;
	}
	IEnumerator PowerUpTripleShot()
	{
		bullet_amount = 3;

		while (tripleShootTimeRemaining > 0)
		{
			tripleShootTimeRemaining -= Time .deltaTime;
			yield return null;
		}

		bullet_amount = 1;
		tripleShootActive = false;
		tripleShootTimeRemaining = 0f;
	}
	IEnumerator PowerUpStrength()
	{
		fireRate = 0.25f;

		while (strengthTimeRemaining > 0)
		{
			strengthTimeRemaining -= Time .deltaTime;
			yield return null;
		}

		fireRate = 0.5f;
		strengthActive = false;
		strengthTimeRemaining = 0f;
	}
	IEnumerator PowerUpx2()
	{
		ScoreManager.Instance.multiply = 2;

		while (strengthTimeRemaining > 0)
		{
			strengthTimeRemaining -= Time .deltaTime;
			yield return null;
		}

		ScoreManager.Instance.multiply = 1;
		x2Active = false;
		x2TimeRemaining = 0f;
	}
	#endregion 


	private void TakeDamage()
	{
		life--;
		ScoreManager.Instance.UpdateLife(life);
		if (life > 0)
			StartCoroutine(DamageWarning());
		if (life == 0)
		{
			Instantiate(explosion, transform.position, Quaternion.identity);
			ScoreManager.Instance.PlayerGameOver();
			Debug.Log("PlayerDestruido");
			Destroy(gameObject);
		}
	}

	//Referencia visual de que él player a recibido daño
	IEnumerator DamageWarning()
	{
		canHit = false;
		anim.Play("PlayerHit", 0, 0f);
		canHit = true;
		yield return new WaitForSeconds(1f);
	}

	IEnumerator ReloadSpecialFire()
	{
		canSpecialFire = false;
		specialAtack.SetActive(false);
		yield return new WaitForSeconds(5f);
		canSpecialFire = true;
		specialAtack.SetActive(true);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("EnemyBullet") && canHit == true)
			TakeDamage();
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Enemy") && canHit == true)
			TakeDamage();
	}
}
