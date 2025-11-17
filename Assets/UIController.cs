using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private TMP_Text score;

    [SerializeField]
    private GameObject settingMenu;

    [SerializeField]
    private GameObject mainMenu;

    public GameObject AISettings;
    public GameObject HumanSetting; //not used for now , just ON.OFf the AI one

    public Slider SpawnXSLide;
    public Slider SpawnYSLide;




    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        Debug.Log(gameOverPanel);
        gameOverPanel.SetActive(false);
    }

    public void AddScore()
    {
        int value = int.Parse(score.text);
        value++;
        score.text = value.ToString();
    }

    public void ResetScore()
    {

        int value = int.Parse(score.text);
        value = 0;
        score.text = value.ToString();

    }

    public void showSettingsMenu()
    {
        showHumanSettings();
        settingMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void showMainMenu()
    {
        settingMenu.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void showAISEttings()
    {
        Debug.Log("ai setting on");
        AISettings.SetActive(true);
    }

    public void showHumanSettings()
    {
        Debug.Log("ai setting off");
        AISettings.SetActive(false);
    }


    public void LoadHumanScene()
    {
        SceneManager.LoadScene("GameScene");
    }



}
