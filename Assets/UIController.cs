using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    private static UIController instance; 

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private TMP_Text score;

    [SerializeField]
    private GameObject settingMenu;

    [SerializeField]
    private GameObject mainMenu;



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

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }



}
