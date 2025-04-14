using UnityEngine;

public class Enemy0 : EnemyBase
{
	//Enemigo que solo avanza

	[Header("Atack Property")]
	private float nextFireTime;
	public float fireRate = 2f;

	//[Header("Enemy Property")]
	//private float speed = 1f;

	public override void Move()
	{
		GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1 * speed);
	}

	public override void Fire()
	{
		
	}
}
