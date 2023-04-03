using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    GameObject prefab;
    Vector2 speed, dir;

    public bullet(Vector2 speed, Vector2 dir)
    {
        this.dir = dir;
        this.speed = speed; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir);
    }
}
