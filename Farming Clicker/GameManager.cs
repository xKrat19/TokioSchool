using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; private set; }

    [Header("Dinero")]
    public int dinero; 
    public TextMeshProUGUI t_Dinero;

    [Header("Score Cultivos")]
    public int numCultivos;
    public TextMeshProUGUI t_NumCutlivos;
    public int objetivoCultivos;
    public TextMeshProUGUI t_objetivosCultivos;
    public Image img;

    [System.Serializable]
    public struct Niveles
    {
        public Sprite img;
        public int limit;
        public GameObject semilla;
    }


    [Header("Stats")]
    public int varClicks; //Variacion de lo que cuenta cada click dependiendo del los articulos de la tienda
    public int varCantidadCultivos; //Variacion del numero de cultivos que pueden salir al recolectar
    public int varPrecio;

    [Header("")]
    public int nivelActual;
    public GameObject semillaSeleccionada; //Prefab de la semilla qu se planta
    public GameObject semillaCultivando; //la semilla con la que se interactua en la escena
    public Niveles[] niveles;
    GameObject[] cultivos;





    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<GameStats>(); //Añade el scripts de GameStats para ir actualizando las estadisticas del juego
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destruimos el duplicado
        }

    }

    void Start()
    {
        UpdateTextoDinero();
        CheckStatusObjetivo();
    }

    public void NuevoCultivo()
    {
        semillaCultivando = Instantiate(semillaSeleccionada);
    }

    public void VenderCultivo(int dinero,int id)
    {
        AddDinero(dinero);
        if(id == nivelActual) numCultivos++; //Si el cultivo recogido cuadra con el nivel actual, se añade al score de cultivos
        UpdateTextoDinero();
        UpdateTextoScore();
        CheckStatusObjetivo();
    }

    public void AddDinero(int value)
    {

        dinero += value;
        UpdateTextoDinero();
    }

    public void RemoveDinero(int value)
    {
        dinero -= value;
        t_Dinero.text = dinero.ToString("D4");
    }

    private void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name == "MainGame")
        {

            if (semillaCultivando == null) NuevoCultivo();
        }
    }

    //Comprueba cada vez que se coge un cultivo cuantos cultivos se lleva recogidos del objetivo
    //si se tiene más cambia el objetivo.
    void CheckStatusObjetivo()
    {
        if (numCultivos >= objetivoCultivos) 
        {
            nivelActual++;
            numCultivos = 0;
            if(nivelActual >= niveles.Length)
            {
                SceneManager.LoadScene("MainGame");
            }
        }

        semillaSeleccionada = niveles[nivelActual].semilla;
        objetivoCultivos = niveles[nivelActual].limit;
        img.sprite = niveles[nivelActual].img;
        UpdateTextoScore();

    }

    public void GenerarEvento()
    {
        int rng = UnityEngine.Random.Range(0, 100);
        if(rng < 25)
        {
            cultivos = GameObject.FindGameObjectsWithTag("cultivo");
            foreach ( var i in cultivos)
            {
                DontDestroyOnLoad(i.gameObject);
                i.SetActive(false);
                GetComponent<GameStats>().AddDesafios(1);
            }

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SceneManager.LoadScene("Moscas");
        }
    }
    public void ActivarCultivos()
    {
        foreach (var i in cultivos)
        {
            i.SetActive(true);
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Invoke("GetTexts", .1f);
    }

    private void GetTexts()
    {
        t_Dinero = GameObject.Find("t_money").GetComponent<TextMeshProUGUI>();
        t_NumCutlivos = GameObject.Find("scoreCultivo").GetComponent<TextMeshProUGUI>();
        t_objetivosCultivos = GameObject.Find("TotalCultivoEtapa").GetComponent<TextMeshProUGUI>();
        img = GameObject.Find("icon_cultivo").GetComponent<Image>();

        UpdateTextoDinero();
        CheckStatusObjetivo();
    }

    private void UpdateTextoDinero()
    {
        t_Dinero.text = dinero.ToString("D4");
    }
    private void UpdateTextoScore()
    {
        t_NumCutlivos.text = numCultivos.ToString("D3");
        t_objetivosCultivos.text = objetivoCultivos.ToString("D3");
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            gameObject.GetComponent<GameStats>().SaveStats();
            UnityEngine.Debug.Log("GameManager destruido");
        }
    }
}
