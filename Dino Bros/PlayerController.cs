using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;


    public float borderLevel;

    bool isGround; //Esta tocando suelo o no?
    Vector3 lastPos; //Ultima posicion en la que estaba en el suelo;

    GameObject cam, respawnPoint;
    Animator anim;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cam = Camera.main.gameObject;
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bloquea la camara por debajo para no ver el fondo
        if (transform.position.y < 0) cam.transform.position = new Vector3(cam.transform.position.x, 0/*transform.position.y * -.1f*/, cam.transform.position.z);
        if (transform.position.y > 2.5f) cam.transform.position = new Vector3(cam.transform.position.x, 2.5f, -10);

        if (transform.position.y < borderLevel)
        {
            moveSpeed = 5;
            TakeDamage();
        }


        float dir = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        if(dir > 0.01f)
        {
            transform.localScale = new Vector3(.7f, .7f, .7f);
        } else if (dir < -0.01f)
        {
            transform.localScale = new Vector3(-.7f,.7f,.7f);
        }

        if (Input.GetButtonDown("Jump") && isGround) Jump(jumpForce);

        if (Input.GetKeyUp(KeyCode.LeftShift)) Run(false);
        if (Input.GetKeyDown(KeyCode.LeftShift)) Run(true);

        anim.SetBool("walk", dir != 0); //Si se pulsan las teclas de direccion el valor será true, pero si no se tocan seran false haciendo asi que se cambie el parametro de la animacion
        anim.SetBool("isGround", isGround);
    }

    private void Run(bool value)
    {
        anim.SetBool("run", value);

        if (value && isGround) moveSpeed = 7;
        else if(!value) moveSpeed = 5;
    }

    public void Jump(float jumpForce)
    {
        rb.AddForce(new Vector2(rb.linearVelocity.x, jumpForce * 10));
        isGround = false;
    }

    public void TakeDamage()
    {
        transform.position = respawnPoint.transform.position;
        GameManager.Instance.UpdateLife(-1);
        StartCoroutine(DamageWarning());
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
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
        if (other.gameObject.tag == "Suelo")
        {
            isGround = true;
        }

        if (other.gameObject.tag == "Finish")
        {
            moveSpeed = 0;
            jumpForce = 0;
        }

    }
}
