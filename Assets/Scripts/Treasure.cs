using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update

    static public Vector3 delta;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource=GetComponent<AudioSource>();
        //print("AudioSource:" + audioSource);
    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Diver" && Controller.holdTreasure == null)
        {
            Controller.holdTreasure = this.gameObject;
            delta = collision.transform.position - this.gameObject.transform.position;
            audioSource.Play();
        }
        //transform.position = collision.transform.position;
    }
}
