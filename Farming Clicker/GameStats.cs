using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public TextMeshProUGUI t_numRegadas,t_numRecolectados,t_moscas,t_desafios;

    public int numRegadas,numRecolectados,moscas,desafios;


    private void Start()
    {
        GetStats();
        PrintStats();
    }

    public void GetStats()
    {
        numRegadas = PlayerPrefs.GetInt("numRegadas");
        numRecolectados = PlayerPrefs.GetInt("numRecolectados");
        moscas = PlayerPrefs.GetInt("moscas");
        desafios = PlayerPrefs.GetInt("desafios");
    }

    void PrintStats()
    {
        if (t_numRegadas != null)
        {
            t_numRegadas.text = numRegadas.ToString();
            t_numRecolectados.text = numRecolectados.ToString();
            t_moscas.text = moscas.ToString();
            t_desafios.text = desafios.ToString();
        }
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("numRegadas",numRegadas);
        PlayerPrefs.SetInt("numRecolectados", numRecolectados);
        PlayerPrefs.SetInt("moscas", moscas);
        PlayerPrefs.SetInt("desafios", desafios);
    }

    public void AddNumRegadas(int i){ numRegadas += i; }
    public void AddNumRecolectados(int i) { numRecolectados += i; }
    public void AddMoscas(int i) {  moscas += i; }
    public void AddDesafios(int i) { desafios += i; }

}
