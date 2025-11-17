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

    public Slider SpawnXSLide;
    public Slider SpawnYSLide; 



    private void Awake()
    {
        Debug.Log("UI getting alive");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


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

        settingMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void showMainMenu()
    {
        settingMenu.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void LoadHumanScene()
    {
        SceneManager.LoadScene("GameScene");
    }



}
