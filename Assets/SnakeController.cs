using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Prefab objects")]
    [SerializeField]
    private GameObject snakeHeadPrefab;
    [SerializeField]
    private GameObject snakeBodyPrefab;
    [SerializeField]
    private GameObject snakeFoodPrefab;


    [Header("gameplay specifics customazation..!")]
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private int gridWidth = 40; //40
    [SerializeField]
    private int gridHeight = 30; //30
    [SerializeField]
    private int initialSpawnX = 10; // within the gridf   
    [SerializeField]
    private int initialSpawnY = 15;   //within the grid


    //input 
    float horizontal;
    float vertical;


    //il instnace
    float worldHeight;
    float worldWidth;



    //grid related  ;
    int[,] grid ;  //    5 head    3 body   10 food  15 special fodd  0 empty    -1 barrier
    private float cellSizeX; //calcultated not given 
    private float cellSizeY;

    // runtime thingies

    [SerializeField]
    MyDirection direction = MyDirection.right;

    float timer = 0;
    int timeUntilMove = 100;
    List<(int x , int y)> snakeArray = new List<(int x, int y)>();


    GameObject SnakeHead;
    private void Awake()
    {
        worldHeight= Camera.main.orthographicSize * 2f;
        worldWidth = worldHeight* Camera.main.aspect;

        cellSizeX = worldWidth / gridWidth; 
        cellSizeY = worldHeight/ gridHeight;

        grid = new int[gridHeight , gridWidth]; 

    }

    private void Start()
    {
        
        SnakeHead = Instantiate(snakeHeadPrefab , CellToWorld(0 , 0) , Quaternion.identity );
        grid[0, 0] = 1;
        snakeArray.Add((0 , 0)); 
        SnakeHead.transform.localScale = new Vector3(cellSizeX , cellSizeY, 0);

    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (direction == MyDirection.right || direction == MyDirection.left)
        {
            if (vertical == 1) 
            { 
                
                direction = MyDirection.up;
                GenerateFood();
            
            }


            if (vertical == -1) direction = MyDirection.down;
        }

        if (direction == MyDirection.up || direction == MyDirection.down)
        {
            if (horizontal == 1) direction = MyDirection.right;
            if (horizontal == -1) direction = MyDirection.left;
        }

        Debug.Log(direction); 
    }
    private void FixedUpdate()
    {
        MoveSnake(SnakeHead); 
    }

    private Vector3 CellToWorld( int x , int y)
    {
        Vector3 vec3 = Vector3.zero;

        vec3.x = x * cellSizeX - (float)worldWidth/2  + cellSizeX/2;
        vec3.y = y * cellSizeY - (float)worldHeight/2 +  cellSizeY/2 ; 

        return vec3 ;
    }

    private void MoveSnake(GameObject obj)
    {

        if (timer * speed < timeUntilMove)
        {
            timer++;
        }
        else
        {
            timer = 0;
            if (direction == MyDirection.right) obj.transform.Translate(cellSizeX, 0, 0);
            else if (direction == MyDirection.left) obj.transform.Translate(-cellSizeX, 0, 0);
            else if (direction == MyDirection.up) obj.transform.Translate(0, cellSizeY, 0);
            else if (direction == MyDirection.down) obj.transform.Translate(0, -cellSizeY, 0);
            
        }
    }

    private void GenerateFood()
    {
        List< ( int x , int y )>  zeroList = new List< ( int x , int y )>();

        for ( int i = 0; i < gridHeight; i++)
        {
        {
            for ( int j = 0; j < gridWidth; j++)
            {
                if(grid[i, j] == 0) zeroList.Add( ( i , j ) );
            }
        }

        int randomIndex = UnityEngine.Random.Range( 0, zeroList.Count );

        var ( x , y) = zeroList[randomIndex];

        grid[x, y] = 2;

            GameObject food = Instantiate(snakeFoodPrefab, CellToWorld(x, y), Quaternion.identity);

            food.transform.localScale = new Vector3(cellSizeX, cellSizeY, 0);

        }


    }

} 

public enum MyDirection
{
    up,
    down,
    left,
    right
}
