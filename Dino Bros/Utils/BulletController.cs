using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletController : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
            other.GetComponent<PlayerController>().TakeDamage();
            
        Destroy(gameObject);
        //if (other.GetComponent<TilemapCollider2D>() == true) Destroy(gameObject);
    }
}
