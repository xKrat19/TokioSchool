using Unity.VisualScripting;
using UnityEngine;

public class Enemy2 : EnemyBase
{
	//Enemigo que avanza en linea recta pero dispara dos proyectiles
	//con angulo.

	[Header("Atack Property")]
	private float nextFireTime;
	private float fireRate = 3f;

	[Header("Enemy Property")]
	//private float speed = .5f;
	private float angle_Offset = 10f;

	public override void Move()
	{
		GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -1 * speed);
	}

	public override void Fire()
	{
		if (Time.time > nextFireTime)
		{
			Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.Euler(0, 0, angle_Offset));
			Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.Euler(0, 0, -angle_Offset));
			nextFireTime = Time.time + fireRate;
		}
	}
}
