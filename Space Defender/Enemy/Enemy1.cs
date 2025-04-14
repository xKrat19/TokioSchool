using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : EnemyBase
{
	//Enemigo que se mueve en zigzag y dispara en linea recta

	[Header("Atack Property")]
	private float nextFireTime;
	private float fireRate = 3f;

	[Header("Enemy Property")]
	//private float speed = 1f;
	private float h_Movm;
	private float zigzagFrec = 3f;

	public override void Move()
	{
		h_Movm = Mathf.Sin(Time.time * zigzagFrec) * -5f;
		transform.Translate(new Vector2(h_Movm, -1f) * speed * Time.deltaTime);
	}

	public override void Fire()
	{
		if (Time.time > nextFireTime)
		{
			Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.identity);
			nextFireTime = Time.time + fireRate;
		}
	}
}
