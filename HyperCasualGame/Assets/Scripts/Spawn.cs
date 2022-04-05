using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Range(1, 10)]
    public int widht, height;
    Vector3 origin;

    public GameObject[] cubes;
    void Start()
    {
        for (int i = -5; i < widht; i++)
        {
            for (int j = -5; j < height; j++)
            {
                              
                int randomValue = Random.Range(0, 2);
                GameObject obj = Instantiate(cubes[randomValue], origin, transform.rotation,transform);
                origin = new Vector3(transform.position.x + i, 0.3f, transform.position.z + j);
            }
        }
    }
}
