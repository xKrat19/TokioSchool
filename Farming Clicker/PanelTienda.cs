using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelTienda : MonoBehaviour
{
    [Header("Propiedades Panel")]
    public string nombre;
    public int coste;
    public float variacionCoste;
    public int limite;

    [Header("")]
    public TextMeshProUGUI t_coste;
    int cantidadObtenidas;
    public TextMeshProUGUI t_cantidadObtenida;

    public enum TipoBoton {toque,cultivo, dinero };
    public TipoBoton accion;

    public void Start()
    {
        switch (accion)
        {
            case TipoBoton.toque:
                cantidadObtenidas = GameManager.Instance.varClicks;
                break;
            case TipoBoton.cultivo:
                cantidadObtenidas = GameManager.Instance.varCantidadCultivos;
                break;
            case TipoBoton.dinero:
                cantidadObtenidas = GameManager.Instance.varPrecio;
                break;
        }
        for (int i = 0; i < cantidadObtenidas; i++)
        {
           coste = (int)(coste * variacionCoste);
        }
        UpdateTextos();
    }

    private void OnEnable()
    {
        UpdateTextos();
    }

    public void AccionBoton()
    {
        if (GameManager.Instance.dinero >= coste)
        {
            GameManager.Instance.RemoveDinero(coste);
            cantidadObtenidas++;

            switch (accion)
            {
                case TipoBoton.toque:
                    GameManager.Instance.varClicks++;
                    break;
                case TipoBoton.cultivo:
                    GameManager.Instance.varCantidadCultivos++;
                    break;
                    case TipoBoton.dinero:
                    break;
            }

            coste = (int)(coste * variacionCoste);
        }
        UpdateTextos();
        CheckLimite();
    }

    void UpdateTextos()
    {
        t_coste.text = coste.ToString();
        t_cantidadObtenida.text = nombre + " lvl:" + cantidadObtenidas.ToString();
    }

    void CheckLimite()
    {
        if((limite!=-1) && (cantidadObtenidas>=limite))
        {
            gameObject.GetComponent<Button>().interactable = false;
            t_cantidadObtenida.text = nombre + " lvl:MAX";
        }
    }
}

