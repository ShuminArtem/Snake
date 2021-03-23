using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] GameObject food;
    [SerializeField] Transform borderLeft;
    [SerializeField] Transform borderRight;
    [SerializeField] Transform borderTop;
    [SerializeField] Transform borderDown;
    [SerializeField] float padding = 0.2f;

    float borderX;
    float borderY;
    public void FoodSpawn()
    {  
        borderX = Random.Range(borderLeft.position.x + padding, borderRight.position.x - padding);
        borderY = Random.Range(borderTop.position.y - padding, borderDown.position.y + padding);
        Instantiate(food, new Vector2(borderX, borderY), Quaternion.identity);
    }

}
