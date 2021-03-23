using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake1 : MonoBehaviour
{

    [SerializeField] GameObject tailPrefab;
    private Vector2 gridMoveDirection;
    private Vector2 lastMoveDirection;
    private Vector2 gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimeMax;
    bool appleExist = false;
    bool ate = true;
    private int snakebodySize;
    private List<Vector2> snakeMovePosList;
    private List<Transform> snakeBodyTransformList;
    private List<Transform> tail = new List<Transform>();


    private void Awake()
    {
        gridPosition = new Vector2Int(1, 1);
        gridMoveTimeMax = 0.1f;
        gridMoveTimer = gridMoveTimeMax;
        gridMoveDirection = new Vector2(0.1f, 0);
        lastMoveDirection = new Vector2(0.1f, 0);
        snakeMovePosList = new List<Vector2>();
        snakebodySize = 1;
        snakeBodyTransformList = new List<Transform>();
    }

    private void Update()
    {
        HandlerGridMovement();
        FoodEating();
        //AddingSnakeTail();
        
    }

    public void UpButton()
    {
        if (lastMoveDirection.y != -0.1f)
        {
            gridMoveDirection.x = 0;
            gridMoveDirection.y = 0.1f;
        }
    }
    public void DownButton()
    {
        if (lastMoveDirection.y != 0.1f)
        {
            gridMoveDirection.x = 0;
            gridMoveDirection.y = -0.1f;
        }
    }
    public void LeftButton()
    {
        if (lastMoveDirection.x != 0.1f)
        {
            gridMoveDirection.x = -0.1f;
            gridMoveDirection.y = 0;
        }
    }
    public void RightButton()
    {
        if (lastMoveDirection.x != -0.1f)
        {
            gridMoveDirection.x = 0.1f;
            gridMoveDirection.y = 0;
        }
    }

    private void HandlerGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimeMax)
        {
            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimeMax;
            snakeMovePosList.Insert(0, gridPosition);
            gridPosition += gridMoveDirection;
            

            if (snakeMovePosList.Count >= snakebodySize + 1)
            {
                snakeMovePosList.RemoveAt(snakeMovePosList.Count - 1);
            }

            for(int i = 0; i < snakeMovePosList.Count; i++)
            {
                Vector2 snakeMovePos = snakeMovePosList[i];
                
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) + 90);
            lastMoveDirection = new Vector2(gridMoveDirection.x, gridMoveDirection.y);
        }
    }

    private float GetAngleFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    private void FoodEating()
    {
        while(appleExist == false)
        {
            FindObjectOfType<FoodSpawner>().FoodSpawn();
            appleExist = true;
        }
    }
    private void AddingSnakeTail()
    {
        Vector2 v = transform.position;


        // Ate something? Then insert new Element into gap
        if (ate == true)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                  v,
                                                  Quaternion.identity);
            tail.Insert(0, g.transform);

            ate = false;
        }
        
        else if (tail.Count > 0)
        {
            tail.Last().position = v;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        appleExist = false;
        ate = true;
        Destroy(collision.gameObject);
    }

}
