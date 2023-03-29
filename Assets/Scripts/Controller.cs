using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Transform Ship;
    public Transform Diver;
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
        //print(new Vector3(0, Input.GetAxis("Vertical") , 0));
        Ship.Translate(move * Input.GetAxis("Horizontal"), 0, 0);
        Diver.Translate(0, move * Input.GetAxis("Vertical"), 0, 0);
        //Vector3 DiverPosition = Ship.position + DiverOffset+new Vector3(0,Input.GetAxis("Vertical"),0);
        //Diver.SetPositionAndRotation(DiverPosition, Quaternion.identity);

        //transform.position=new Vector3(move,0,0);
    }
}
