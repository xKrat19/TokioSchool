using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject menuLevelSelector;
    public Button[] mainMenuButtons, levelSelectorButtons;
    int actualLevel;

    private void Start()
    {
        actualLevel = PlayerPrefs.GetInt("Levels");
        Debug.Log(actualLevel);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Levels", 1);
        PlayerPrefs.SetInt("coin", 0);
        PlayerPrefs.SetInt("life", 3);
        SceneManager.LoadSceneAsync(1);
    }

    public void SelectLevel(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void OpenLevelSelector(bool status)
    {
        foreach (var item in mainMenuButtons)
        {
            if (item.interactable == true)
                item.interactable = false;
            else
                item.interactable = true;
        }
        if (status == true)
        {
            for (int i = 1; i < levelSelectorButtons.Length; i++)
                {
                if (i < actualLevel)
                    levelSelectorButtons[i].interactable = true;
                else 
                    levelSelectorButtons[i].interactable = false;
            }
        }
        menuLevelSelector.SetActive(status);
    }

    public void Exit() 
    { 
        Application.Quit(); 
    }
}
