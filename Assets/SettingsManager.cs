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

    
    public void SetSpeed(float speed) { this.speed = speed; }
    public void SetGridWidth(float gridWidth) 
    { 
        this.gridWidth = Mathf.RoundToInt(gridWidth);
        MyUI.SpawnXSLide.maxValue = gridWidth;
    }

    public void SetGridHeight(float gridHeight)
    {
        this.gridHeight = Mathf.RoundToInt(gridHeight);
        MyUI.SpawnYSLide.maxValue = gridHeight;
    }
    public void SetInitialSpawnX(float initialSpawnX) { this.initialSpawnX = Mathf.RoundToInt(initialSpawnX); }
    public void SetInitialSpawnY(float initialSpawnY) { this.initialSpawnY = Mathf.RoundToInt(initialSpawnY); }
        
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
