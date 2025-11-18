using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public UIController MyUI;


    public float speed = 1.0f;


    public int gridWidth = 40;

    public int gridHeight = 30;


    public int initialSpawnX = 0;

    public int initialSpawnY = 0;


    public string AIMode = "BF";



    private void Awake()
    {   
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
