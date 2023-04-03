using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAround : MonoBehaviour
{
    private Camera mainCamera;
    private float screenWidth;

    void Start()
    {
        mainCamera = Camera.main;
        screenWidth = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - mainCamera.ScreenToWorldPoint(Vector3.zero).x;
    }

    void Update()
    {
        if (transform.position.x < mainCamera.ScreenToWorldPoint(Vector3.zero).x)
        {
            transform.position = new Vector3(mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x)
        {
            transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Vector3.zero).x, transform.position.y, transform.position.z);
        }
    }
}

