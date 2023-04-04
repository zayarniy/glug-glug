using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ExplosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Fish")
        {
            print("Controller on TriggerEnter2D " + collision.gameObject.tag);
            var Explosion = GameObject.Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
//            print(Explosion.name);
            // Controller.ExplosionExecute(collision.gameObject.transform);

            //Controller.gameStatus = GameStatus.GameOver;
        }

    }

    void Dead()
    {
        //gameStatus = GameStatus.Play;

    }

  
}
