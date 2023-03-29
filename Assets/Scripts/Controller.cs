using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Transform Ship;
    Transform Diver;
    public Vector3 DiverOffset;

    void Start()
    {
        Ship = transform;
        Diver = GameObject.FindGameObjectsWithTag("Diver")[0].transform;
        print(Diver);
    }

    // Update is called once per frame
    void Update()
    {

        float move = speed * Time.deltaTime;
        float dx = Input.GetAxis("Horizontal");
        print(Ship.position.x);
        

        if (Ship.position.x+move*dx >= -7.8f && Ship.position.x+move*dx <= 7.73f)
        {
            //Ship.Translate(move * Input.GetAxis("Horizontal"), 0, 0);
            Ship.position=new Vector3((float)Ship.position.x+move * dx, (float)Ship.position.y, (float)Ship.position.z);
            Diver.Translate(0, move * Input.GetAxis("Vertical"), 0, 0);
        }
        //else Ship.position=new Vector3(Mathf.Clamp(Ship.position.x,- 5.59f, 7.73f), Ship.position.y,0);
        

        //Vector3 DiverPosition = Ship.position + DiverOffset+new Vector3(0,Input.GetAxis("Vertical"),0);
        //Diver.SetPositionAndRotation(DiverPosition, Quaternion.identity);

        //transform.position=new Vector3(move,0,0);
    }
}
