using UnityEngine;

public class CultivoController : MonoBehaviour
{

    bool isRecolectado;

    //Movimiento del cultivo a la "tienda"
    Vector3 startPosition;
    Vector3 targetPosition = new Vector3(-4f, 3.24f,0f);
    float movSpeed = 10;
    float sinTime;

    public int id; //Para saber que tipo de cultivo es
    public int coste; //Lo que se gana cada vez que se cultiva

    void Start()
    {
        //Hace que el cultivo obtenido se desplace para que se reparta por el sitio
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)) * 5f, ForceMode2D.Impulse);
        Invoke("ActivarToque", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRecolectado && (transform.position != targetPosition))
        {
            sinTime += Time.deltaTime * movSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
            float t = Evaluate(sinTime);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        }
        if (transform.position == targetPosition) {

            GameManager.Instance.VenderCultivo(coste, id);
            Destroy(gameObject); //Le dice al gameManager que genere un nuevo cultivo
        }

    }

    private void OnDestroy()
    {
        GameManager.Instance.GetComponent<GameStats>().AddNumRecolectados(1);
        GameManager.Instance.GenerarEvento();
    }
    private void OnMouseDown()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        startPosition = transform.position;
        isRecolectado = true;
    }

    public void ActivarToque()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public float Evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2) + 0.5f;
    }

}
