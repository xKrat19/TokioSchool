using Unity.VisualScripting;
using UnityEngine;

public class BoxController : MonoBehaviour
{
	private Rigidbody2D rb;

	//Estados
	private bool onCollision = false;
	private bool isFalling; //Para saber si está cayendo o no

	private float borderX; //borde que no puede superar los bloques al moverse antes de caer
	//Movimiento
	private int direction; //Direccion hacia donde se movera el bloque
	private float speed; //Velocidad del bloque en movimiento

	private void Start()
	{

		//Obligas a que el valor sea 1 o -1
		while (direction == 0)
			direction = Random.Range(-1, 1);

		isFalling = false;
		speed = Random.Range(.75f, 1.5f);

		rb = GetComponent<Rigidbody2D>();

		borderX = CalculateMovementBorder();
	}

	private void Update()
	{
		if (!isFalling)
		{
			MoveBox();

			if (Input.GetKeyDown(KeyCode.Space))
			{
				DropBox();
			}
		}
	}
	private void FixedUpdate()
	{
		//Si ha caido bien y encima de un "box" suponemos que esta bien puesto
		if (rb.linearVelocity.magnitude < 0.1f && rb.linearVelocity.x < 0.1f && onCollision)
		{
			//CamController.Instance.SetLastBox(gameObject);
			float Height = transform.position.y + (transform.localScale.y / 2);
			CamController.Instance.MoveCamera(Height);
			ScoreManager.Instance.SetScore(Height);
			BlockSpawner.Instance.NewBlock();
			//SolidBox();
			Destroy(this); //Destruimos el scrip porque ya no hace falta
		}
	}

	void MoveBox()
	{
		transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

		if (transform.position.x <= -borderX || transform.position.x >= borderX)
		{
			direction *= -1;
		}
	}
	//Cambia el tipo de body del rb para que le afecte la gravedad
	void DropBox() 
	{
		isFalling = true;
		rb.bodyType = RigidbodyType2D.Dynamic;

		//Descomentar para una caida "realista"
		float force = .3f; //<- fuera del impulso
		Vector2 impulse = new Vector2(direction * speed * force, 0f);
		rb.AddForce(impulse, ForceMode2D.Impulse);
	}
	void SolidBox()
	{
		//rb.bodyType = RigidbodyType2D.Static;
		rb.sharedMaterial.friction = .4f;
		rb.sharedMaterial.bounciness = 0f;
	}

	//Obtien los borders de movimiento del cubo
	float CalculateMovementBorder()
	{
		//2.815f
		return (Camera.main.orthographicSize / 2) - (transform.localScale.x / 2);
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		//Si colisiona con un bloque suponemos que ha caido bien
		if (other.gameObject.CompareTag("Box"))
			onCollision = true;	
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
