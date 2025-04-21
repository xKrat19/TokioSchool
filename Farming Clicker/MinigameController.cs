using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    public int moscas;

    public TextMeshProUGUI t_moscas;
    int moscasObjetivo;
    public TextMeshProUGUI t_moscasObjetivo;
    public GameObject prefabMoscas;


    void Start()
    {
        moscasObjetivo = Random.Range(5, 10) * 1;

        for (int i = 0; i < moscasObjetivo; i++)
        {
            Instantiate(prefabMoscas, RandomPointInBox(), Quaternion.identity);
        }
        UpdateTextos();
    }

    public Vector3 RandomPointInBox()
    {
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        Vector2 size = boxCollider.size;
        return new Vector3(
                    Random.Range(-size.x, size.x),
                    Random.Range(-size.y, size.y),
                    0);
    }

    public void UpdateTextos()
    {
        t_moscasObjetivo.text = moscasObjetivo.ToString("D3");
        t_moscas.text = moscas.ToString("D3");

        if(moscas >= moscasObjetivo)
        {
            Invoke("LoadMainGame", 1f);
        }
    }

    void LoadMainGame() 
    {
        GameManager.Instance.AddDinero(25);
        SceneManager.LoadScene("MainGame");
    }

    void OnDestroy()
    {
        GameManager.Instance.ActivarCultivos();
    }

}
