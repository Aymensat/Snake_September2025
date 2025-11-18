using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    
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


    //settings UI
    //Sliders
    public Slider SpeedSlide;
    public Slider SpawnXSLide;
    public Slider SpawnYSLide;
    public Slider GridXSlide;
    public Slider GridYSlide;
    //cehckbox
    public Toggle AI_BF;
    public Toggle AI_A;





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

        LoadCurrentSettings();

    }



    public void showMainMenu()
    {
        settingMenu.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void showAISEttings()
    {
        //Debug.Log("ai setting on");
        AISettings.SetActive(true);
    }

    public void showHumanSettings()
    {
        //Debug.Log("ai setting off");
        AISettings.SetActive(false);
    }


    public void LoadHumanScene()
    {
        SceneManager.LoadScene("GameScene");
    }



    private void LoadCurrentSettings()
    {   
        Debug.Log("Load getting called..."); 

        SpawnXSLide.maxValue =SettingsManager.Instance.gridWidth -1;
        SpawnYSLide.maxValue = SettingsManager.Instance.gridHeight-1;

        SpeedSlide.value = (SettingsManager.Instance.speed);

        GridXSlide.value = (SettingsManager.Instance.gridWidth);
        GridYSlide.value = (SettingsManager.Instance.gridHeight);

        SpawnXSLide.value = (SettingsManager.Instance.initialSpawnX);
        SpawnYSLide.value = (SettingsManager.Instance.initialSpawnY);
    }


    public void SetSpeed(float speed) { SettingsManager.Instance.speed = speed; }
    public void SetGridWidth(float gridWidth)
    {
        SettingsManager.Instance.gridWidth = Mathf.RoundToInt(gridWidth);
        SpawnXSLide.maxValue = gridWidth;
    }

    public void SetGridHeight(float gridHeight)
    {
        SettingsManager.Instance.gridHeight = Mathf.RoundToInt(gridHeight);
        SpawnYSLide.maxValue = gridHeight;
    }
    public void SetInitialSpawnX(float initialSpawnX)
    {


        SettingsManager.Instance.initialSpawnX = Mathf.RoundToInt(initialSpawnX);
        SpawnXSLide.maxValue = SettingsManager.Instance.gridWidth - 1;
    }
    public void SetInitialSpawnY(float initialSpawnY)
    {
        SettingsManager.Instance.initialSpawnY = Mathf.RoundToInt(initialSpawnY);
        Debug.Log("??" + initialSpawnY); 
        SpawnYSLide.maxValue = SettingsManager.Instance.gridHeight-1;
    }



    public void SetAiMode(int id)
    {

        if (id == 1) SettingsManager.Instance.AIMode = "BF";
        else if (id == 2) SettingsManager.Instance.AIMode = "A*";
        return;

    }



}
