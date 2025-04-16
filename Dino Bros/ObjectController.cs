using System;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    Animator anim;
    public enum t_Object {coin, emerald, powerup, life};
    public t_Object type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        switch (type) 
        {
            case t_Object.coin:
                anim.Play("coin");
                break;
            case t_Object.emerald:
                anim.Play("emerald");
                break;
            case t_Object.life:
                anim.Play("life");
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (type)
            {
                case t_Object.coin:
                    GameManager.Instance.UpdateCoin(1);
                    break;
                case t_Object.emerald:
                    GameManager.Instance.UpdateCoin(10);
                    break;
                case t_Object.life:
                    GameManager.Instance.UpdateLife(1);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
