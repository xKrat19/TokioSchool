using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonManager : MonoBehaviour
{
    public GameObject panelTienda;

    
    public void CargarEscena(string nombreEscena)
    {
        Debug.Log("Carga la escena " + nombreEscena);

        if (nombreEscena == "MainMenu") Destroy(GameObject.Find("GameManager"));
        
        SceneManager.LoadScene(nombreEscena);
    }

    public void PanelTienda(bool estado)
    {
        panelTienda.SetActive(estado);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
