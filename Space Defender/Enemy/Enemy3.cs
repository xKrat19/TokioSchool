using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy3 : EnemyBase
{
	//Enemigo que avanza en linea recta pero dispara dos proyectiles
	//con angulo.

	[Header("Atack Property")]
	private float nextFireTime;
	public float fireRate = 4f;
	public float burstFireTime = .3f;
	public int bulletCount = 5; //Numero de ballas por rafaga

	[Header("Enemy Property")]
	//private float speed = .5f;
	private float h_Movm;
	private float zigzagFrec = 1f;

	public override void Move()
	{
		h_Movm = Mathf.Sin(Time.time * zigzagFrec) * 3f;
		transform.Translate(new Vector2(h_Movm, -1f) * speed * Time.deltaTime);
	}

	public override void Fire()
	{
		if (Time.time > nextFireTime)
		{
			StartCoroutine(FireBurst());
			nextFireTime = Time.time + fireRate;
		}
	}

	private IEnumerator FireBurst()
	{
		for (int i = 0; i < bulletCount; i++)
		{
			Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.identity);
			yield return new WaitForSeconds(burstFireTime);
		}
	}
}
