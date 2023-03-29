using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update

    static public Vector3 delta;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Controller.holdTreasure == null)
        {
            Controller.holdTreasure = this.gameObject;
            delta = collision.transform.position - this.gameObject.transform.position;
        }
        //transform.position = collision.transform.position;
    }
}
