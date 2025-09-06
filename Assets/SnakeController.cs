using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Prefab objects")]
    [SerializeField]
    private GameObject snakeHeadPrefab;
    [SerializeField]
    private GameObject snakeBodyPrefab;


    [Header("gameplay specifics customazation..!")]
    [SerializeField] 
    private float speed = 1.0f;
     
    [SerializeField]
    private int gridWidth = 40; //40
    [SerializeField]
    private int  gridHeight = 30; //30
    [SerializeField]
    private int initialSpawnX = 10 ; // within the gridf   
    [SerializeField]
    private int initialSpawnY = 15;   //within the grid


    //il instnace
    float worldHight;
    float worldWidth;



    //grid related  ;
    int[,] grid ;  //    5 head    3 body   10 food  15 special fodd  0 empty    -1 barrier
    private float cellSizeX ; //calcultated not given 
    private float cellSizeY;

    //runtime intances
    private GameObject snakeHeadInstance;

    //runtime varibales
    private MyDirection direction = MyDirection.right;
    private MyDirection bufferDirection = MyDirection.right;
    private float horizontal;
    private float vertical;

    private (float x, float y) HeadPosition = (0, 0);

    private (float x, float y) oldHeadPosition  ;  

    float step = 0; 



    private void Awake()
    {


        grid = new int[gridWidth, gridHeight];

        //getting world settings

        Camera camera = Camera.main;

        worldHight = camera.orthographicSize * 2f;
        worldWidth = worldHight * camera.aspect;

        cellSizeX = worldWidth / gridWidth;
        cellSizeY = worldHight / gridHeight;
    }
        
    private void Start()
    {
        HeadPosition = (initialSpawnX, initialSpawnY);
        snakeHeadInstance = Instantiate(snakeHeadPrefab, CellToWorld(HeadPosition.x , HeadPosition.y), Quaternion.identity);
        snakeHeadInstance.transform.localScale = new Vector3(cellSizeX, cellSizeY, 1);  
        //Debug.Log($"{worldHight}+   {worldWidth}");
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        if ((direction == MyDirection.up || direction == MyDirection.down) && horizontal != 0)
        {

            bufferDirection = (horizontal == 1) ? MyDirection.right : MyDirection.left;
        }

        if ((direction == MyDirection.left || direction == MyDirection.right) && vertical != 0)
        {

            bufferDirection = (vertical == 1) ? MyDirection.up : MyDirection.down;
        }


        if (direction == MyDirection.right) HeadPosition.x += 1 * Time.fixedDeltaTime;
        if (direction == MyDirection.left) HeadPosition.x -= 1 * Time.fixedDeltaTime;
        if (direction == MyDirection.up) HeadPosition.y += 1 * Time.fixedDeltaTime;
        if (direction == MyDirection.down) HeadPosition.y -= 1 * Time.fixedDeltaTime;





        //step += Time.fixedDeltaTime;
        //if (step >= 1)
        //{
        //    step = 0;
        //    if (direction == MyDirection.right)  HeadPosition.x += 1;
        //    if (direction == MyDirection.left) HeadPosition.x -= 1;
        //    if (direction == MyDirection.up) HeadPosition.y += 1;
        //    if (direction == MyDirection.down) HeadPosition.y -= 1;
        //}


//super comments ??

        if (Mathf.Abs(HeadPosition.x -oldHeadPosition.x) >= 1f ){

            Debug.Log(" Cell passed horiz "); 
            HeadPosition.x = (int)Math.Round(HeadPosition.x);

            oldHeadPosition.x = HeadPosition.x; 
            SwitchDirection(); }



        if (Mathf.Abs(HeadPosition.y - oldHeadPosition.y) >= 1f)
        {
            Debug.Log(" Cell passed vert ");
            HeadPosition.y = (int)Math.Round(HeadPosition.y);

            oldHeadPosition.y = HeadPosition.y;
            SwitchDirection();
        }

        Vector3 currentPost = (Vector3)CellToWorld(HeadPosition.x, HeadPosition.y);

        snakeHeadInstance.transform.position = new Vector3(currentPost.x , currentPost.y, 0); 



    }

    private void SwitchDirection()
    {
        Debug.Log("Trying to change direction ?? " + direction + "  buffered =  " + bufferDirection); 
        direction = bufferDirection; 
    }



    //helper methods
    private Vector2 CellToWorld(float x, float y) //noramlent int ema injetbo
    {
        float x1 = x - (gridWidth-1)/2f  ;
        //Debug.Log("wiw " + (gridWidth - 1) / 2);
        float y1 = y - (gridHeight-1)/2f ;
        //Debug.Log($"cell size x:    {cellSizeX} y: {cellSizeY}");
        return new Vector2(x1 * cellSizeX , y1 * cellSizeY ) ;
    }

}

public enum MyDirection
{
    up,
    down,
    left,
    right
}
