using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    private void Awake()
    {
        Debug.Log("UI getting alive");
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






}
