using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;

    void Start()
    {
        cam = GetComponentInChildren < Camera > ();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("CamTriggerEnter");
       // cam.enabled = true;
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        print("CamTriggerExit");
        //cam.enabled = false;
    }
}
