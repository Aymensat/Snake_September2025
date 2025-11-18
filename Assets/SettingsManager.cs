using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public UIController MyUI;


    public float speed;
    public int gridWidth;
    public int gridHeight;
    public int initialSpawnX;
    public int initialSpawnY;
    public string AIMode;



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

        speed = 4.0f;
        gridWidth = 40;
        gridHeight = 10;
        initialSpawnX = 0;
        initialSpawnY = 0;
        AIMode = "BF";
    }
}
