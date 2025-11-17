using UnityEngine;

public class GameConfig : MonoBehaviour
{

    static private GameConfig instance;


    int GridWidth =40;
    int GridHeight = 30;

    int speed = 4;

    bool AiMode = false;

    bool warpless = false;

    int initialSpawnX = 0;
    int initialSpawnY = 0;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

    }


}
