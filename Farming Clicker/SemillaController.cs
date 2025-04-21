using UnityEngine;

public class SemillaController : MonoBehaviour
{
    public int maxClicks;       //Numero de clicks que se tienen que dar para recolectar
    public GameObject cultivo;  //El cultivo que produce

    [System.Serializable]
    public struct EtapasSemilla
    {
        public Sprite img;
        public int minClick; //Minimo de clicks que se tienen que dar para obtener el estado

    }

    public EtapasSemilla[] etapas; //Etapas del cultivo

    int clicks = 0; //Clicks que se han dado


    private void OnMouseDown() //Cada vez que se presiona el boton azul se cosecha la semilla
    {
        GameManager.Instance.GetComponent<GameStats>().AddNumRegadas(1+ GameManager.Instance.varClicks);

        clicks += 1 + GameManager.Instance.varClicks;
        gameObject.GetComponent<Animator>().Play("clickCultivo", 0, 0f);
        //Debug.Log("Click en cultivo");

        if (clicks >= maxClicks)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            int rng = Random.Range(0, 100);
            if (rng <= 25)
            {
                for (int i = 0; i < GameManager.Instance.varCantidadCultivos; i++) Instantiate(cultivo);

            }
            Instantiate(cultivo);
            Destroy(gameObject);
        } //Cuando se riega por completo el cultivo saca un rng de si va a sacar mas de 1 cultivo

        foreach (var e in etapas)
        {
            if (clicks > e.minClick)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = e.img;
            }
        }
    }

    
}
