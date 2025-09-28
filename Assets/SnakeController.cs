using System;
using System.Collections;
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
    private GameObject snakeFoodPrefab;


    [Header("gameplay specifics customazation..!")]
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private int gridWidth = 40; //40
    [SerializeField]
    private int gridHeight = 30; //30
    [SerializeField]
    private int initialSpawnX = 10; // no used , delibertly starting at 0   
    [SerializeField]
    private int initialSpawnY = 15;   //  no used , delibertly starting at 0   


    //input 
    float horizontal;
    float vertical;


    //il instnace
    float worldHeight;
    float worldWidth;



    //grid related  ;
    int[,] grid ;  //    1 snake  5 food  15 special fodd  0 empty    -1 barrier
    private float cellSizeX; //calcultated not given 
    private float cellSizeY;

    // runtime thingies

    [SerializeField]
    MyDirection direction = MyDirection.right;

    float timer = 0;
    int timeUntilMove = 1;  // in seconds
    List<(int x , int y)> snakeArray = new List<(int x, int y)>();
    List <GameObject> renderedSnakeArray = new List<GameObject> ();


    GameObject SnakeHead;
    private GameObject currentFood;

    private void Awake()
    {
        worldHeight= Camera.main.orthographicSize * 2f;
        worldWidth = worldHeight* Camera.main.aspect;

        cellSizeX = worldWidth / gridWidth; 
        cellSizeY = worldHeight/ gridHeight;

        grid = new int[gridWidth, gridHeight];  // not sure about this , shoudld i rather do grid = new int[gridWidth , gridHeight]; bcz i will like to accss grid[x,y ] ,
                                                 // bcz usually the standard , like unity inspector starts with x then y , but then again usally in arrays and
                                                 // matrix we start with lines then columsn , this is confusing 
                                                 

    }

    private void Start()
    {
        
        SnakeHead = Instantiate(snakeHeadPrefab , CellToWorld(0 , 0) , Quaternion.identity );
        grid[0, 0] = 1;
        snakeArray.Add((0 , 0)); 
        renderedSnakeArray.Add( SnakeHead );
        SnakeHead.transform.localScale = new Vector3(cellSizeX , cellSizeY, 0);
        GenerateFood();


    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (direction == MyDirection.right || direction == MyDirection.left)
        {
            if (vertical == 1)  direction = MyDirection.up;

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
        MoveSnake(); 

    }

    private Vector3 CellToWorld( int x , int y)
    {
        Vector3 vec3 = Vector3.zero;

        vec3.x = x * cellSizeX - (float)worldWidth/2  + cellSizeX/2;
        vec3.y = y * cellSizeY - (float)worldHeight/2 +  cellSizeY/2 ; 

        return vec3 ;
    }

    private void MoveSnake()
    {
        //reletex to fix updated which is standard to 0.02 s across all  hardwards 
        if (timer * speed < timeUntilMove)
        {
            timer += Time.fixedDeltaTime;   //0.02 s = 20ms
            Debug.Log(timer);
        }
        else
        {
            timer = 0;

            if (direction == MyDirection.right)
            {


                if (grid[snakeArray[^1].x + 1, snakeArray[^1].y] == 0) ResolveEmptyAhead(snakeArray[^1].x + 1 , snakeArray[^1].y);      // the head always last 

                else if (grid[snakeArray[^1].x + 1, snakeArray[^1].y] == 1) ResolveSnakeAhead(snakeArray[^1].x + 1, snakeArray[^1].y);

                else if (grid[snakeArray[^1].x + 1, snakeArray[^1].y] == 5) ResolveFoodAhead(snakeArray[^1].x + 1, snakeArray[^1].y);


            }


            else if (direction == MyDirection.left) 
            {

                if (grid[snakeArray[^1].x - 1, snakeArray[^1].y] == 0) ResolveEmptyAhead(snakeArray[^1].x - 1, snakeArray[^1].y);      // the head always last 

                else if (grid[snakeArray[^1].x - 1, snakeArray[^1].y] == 1) ResolveSnakeAhead(snakeArray[^1].x - 1, snakeArray[^1].y);

                else if (grid[snakeArray[^1].x - 1, snakeArray[^1].y] == 5) ResolveFoodAhead(snakeArray[^1].x - 1, snakeArray[^1].y);

            }

            else if (direction == MyDirection.up) 
            {

                if (grid[snakeArray[^1].x , snakeArray[^1].y + 1] == 0) ResolveEmptyAhead(snakeArray[^1].x , snakeArray[^1].y +1);      // the head always last 

                else if (grid[snakeArray[^1].x , snakeArray[^1].y+1] == 1) ResolveSnakeAhead(snakeArray[^1].x , snakeArray[^1].y+1);

                else if (grid[snakeArray[^1].x, snakeArray[^1].y+1] == 5) ResolveFoodAhead(snakeArray[^1].x , snakeArray[^1].y+1);
            }

            else if (direction == MyDirection.down) 
            {
                if (grid[snakeArray[^1].x, snakeArray[^1].y - 1] == 0) ResolveEmptyAhead(snakeArray[^1].x, snakeArray[^1].y - 1);      // the head always last 

                else if (grid[snakeArray[^1].x, snakeArray[^1].y - 1] == 1) ResolveSnakeAhead(snakeArray[^1].x, snakeArray[^1].y - 1);

                else if (grid[snakeArray[^1].x, snakeArray[^1].y - 1] == 5) ResolveFoodAhead(snakeArray[^1].x, snakeArray[^1].y - 1);
            }


        }
    }



    private void GenerateFood()
    {
        Debug.Log("food getting called "); 
        List< ( int x , int y )>  zeroList = new List< ( int x , int y )>();

        for ( int i = 0; i < gridWidth; i++)
        
        {
            for ( int j = 0; j < gridHeight; j++)
            {
                if(grid[i, j] == 0) zeroList.Add( ( i , j ) );
            }
        }

        int randomIndex = UnityEngine.Random.Range( 0, zeroList.Count );

        var ( x , y) = zeroList[randomIndex];

        grid[x, y] = 5; // 5 for food

            GameObject food = Instantiate(snakeFoodPrefab, CellToWorld(x, y), Quaternion.identity);

            food.transform.localScale = new Vector3(cellSizeX, cellSizeY, 0);

            currentFood = food;
    }



    private void ResolveEmptyAhead(int x, int y)
    {
        //grid update

        grid[x, y] = 1;
        grid[snakeArray[0].x , snakeArray[0].y] = 0;



        //case of just head moving 
        if (snakeArray.Count == 1)
        {

            snakeArray[0] = (x, y);

            renderedSnakeArray[0].transform.position =  CellToWorld(x, y) ;

            return; //how to test ResolveEmptyAhead before implementing Resolve food ?

        }


        // logic array management

        snakeArray.Add((x,y));
        snakeArray.RemoveAt(0);


        // GO manamgemnt


        GameObject tail = renderedSnakeArray[0];   //getting the  tail to reuse it 
        renderedSnakeArray.RemoveAt(0);    

        tail.transform.position = CellToWorld(x, y);
        tail.GetComponent<SpriteRenderer>().color = Color.red;
        renderedSnakeArray.Add(tail);

        renderedSnakeArray[^2].GetComponent<SpriteRenderer>().color = Color.blue; // rechagint the old head into normal body



        



    }
    private void ResolveFoodAhead(int x, int y)
    {
        //grid management
        grid[x,y] = 1;

        //array management

        snakeArray.Add((x,y));

        //GO management

        GameObject newHead = Instantiate(snakeHeadPrefab, CellToWorld(x, y), Quaternion.identity);
        renderedSnakeArray[^1].GetComponent<SpriteRenderer>().color = Color.blue;
        renderedSnakeArray.Add(newHead);

        //generate food 

        Destroy(currentFood);
        GenerateFood(); 
    }

    private void ResolveBarrierAhead(int x, int y)
    {
        // TO DO
    }

    private void ResolveSnakeAhead(int x, int y)
    {
        // TO DO
    }

    //TO DO warping/OOB manamagent


} 

public enum MyDirection
{
    up,
    down,
    left,
    right
}
