using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Player
    int coins;
    int life = 3;

    //UI
    public TextMeshProUGUI t_points;
    public TextMeshProUGUI t_life;
    public TextMeshProUGUI t_level;

    public GameObject pauseMenu;

    //PauseMenu
    bool isMenu = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);

        life = PlayerPrefs.GetInt("life");
        coins = PlayerPrefs.GetInt("coin");
        t_level.text = SceneManager.GetActiveScene().name;
        isMenu = false;
    }
    private void Start()
    {
        UpdateCoin(0);
        UpdateLife(0);
    }

    private void Update()
    {
        if (life == 0)
        {
            MainMenu();
        }

        if (Input.GetKeyDown(KeyCode.P)) PauseMenu(!isMenu);
        if (Input.GetKeyDown(KeyCode.Escape) && isMenu)
        {
            PauseMenu(false);
            MainMenu();
        }
    }
    public void UpdateCoin(int amount)
    {
        coins += amount;
        t_points.text = coins.ToString("000");
    }

    public void UpdateLife(int amount)
    {
        life += amount;
        t_life.text = "x" + life.ToString("00");
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            MainMenu();
        } 
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        t_level.text = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name;
    }
    void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Destroy(gameObject);
    }
    void PauseMenu(bool status)
    {
        if (status) Time.timeScale = 0;
        else Time.timeScale = 1;
        isMenu = status;
        pauseMenu.SetActive(status); //Activa o desactiva el menu de pausa
    }

    private void OnDestroy()
    {
        int levelRecord = PlayerPrefs.GetInt("Levels");
        int levelActual = SceneManager.GetActiveScene().buildIndex;
        if (levelRecord < levelActual)
            PlayerPrefs.SetInt("Levels", levelActual);
    }
}
