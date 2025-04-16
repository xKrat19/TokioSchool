using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum Enemy {blueSlime, redSlime, bird, canion};
    public Enemy typeEnemy;

    Animator anim;
    int life;
    float height;

    public Transform a, b;
    public float speed;
    bool mov_To_B;

    BoxCollider2D trigger;
    GameObject target;
    Vector3 pos_target;

    public GameObject pref_Bullet;
    public float timeWaitShoot;
    public float shootRate = 2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        switch (typeEnemy)
        {
            case Enemy.blueSlime:
                anim.Play("blueSlime");
                life = 1;
                height = .3f;
                break;
            case Enemy.redSlime:
                anim.Play("redSlime");
                life = 2;
                height = .3f;
                break;
            case Enemy.bird:
                anim.Play("bird");
                life = 1;
                height = .3f;
                trigger = GetComponent<BoxCollider2D>();
                break;
            case Enemy.canion:
                InvokeRepeating("ShotCanion", timeWaitShoot, shootRate);
                break;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (typeEnemy != Enemy.canion)
        {
            if (mov_To_B && target == null)
            {
				Debug.Log("moviendo a A");
                transform.Translate((b.position - transform.position).normalized * speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, b.position) < 0.1f)
                {
                    mov_To_B = false;
                    gameObject.transform.localScale = Vector3.one;
                    if (typeEnemy == Enemy.bird) trigger.enabled = true;
                }
            }
            else if (!mov_To_B && target == null)
            {
				Debug.Log("moviendo a B");
				transform.Translate((a.position - transform.position).normalized * speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, a.position) < 0.1f)
                {
                    mov_To_B = true;
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                    if (typeEnemy == Enemy.bird) trigger.enabled = true;
                }
            }
            else if (target != null && typeEnemy == Enemy.bird)
            {
                transform.position = Vector2.MoveTowards(transform.position, pos_target, (speed * 3.5f) * Time.deltaTime);

                if (Vector3.Distance(transform.position, pos_target) < 0.1f)
                {
                    target = null;
                    anim.Play("bird");
                }
            }
        }
    }

    void ShotCanion() 
    {
        //Instanciar la bala que vaya a disparar
        anim.Play("ShootCanion", 0, 0f);
        Instantiate(pref_Bullet, transform.position + new Vector3(-0.18f, 0.62f, 0), Quaternion.identity);
    }
    void TakeDamage(Collision2D other)
    {
        life--;
        StartCoroutine(DamageWarning());

        if (life <= 0)
            Destroy(transform.parent.gameObject);
    }

    IEnumerator DamageWarning()
    {
        float elapsedTime = 0f;
        float dur = 0.3f;

        while (elapsedTime < dur)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, elapsedTime / dur);
            elapsedTime += Time.deltaTime * 3;
            yield return null;
        }
        elapsedTime = 0f;
        while (elapsedTime < dur)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, elapsedTime / dur);
            elapsedTime += Time.deltaTime * 3;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            Vector2 pos_player = other.transform.position;
            Vector2 pos_enemy = transform.position;

            if (pos_player.y > (pos_enemy.y + height))
            {
               player.Jump(20);
               TakeDamage(other);
            }
            else 
            {
                player.TakeDamage();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            trigger.enabled = false;
            target = other.gameObject;
            pos_target = other.gameObject.transform.position;
            anim.Play("birdAtack");
        }
    }
}
