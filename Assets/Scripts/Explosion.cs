using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //static List<GameObject> InsExplosions = new List<GameObject>();
    //public GameObject ExplosionPrefab;
    float ellipsedTime=0;
    public float animationTime = 4f;


    // Start is called before the first frame update
    void Start()
    {
        ellipsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ellipsedTime+=Time.deltaTime;
        if (ellipsedTime>=animationTime)
            Destroy(gameObject);
    }

    //public  void Fish_Explosion(Vector3 pos)
    //{
    //    InsExplosions.Add(GameObject.Instantiate(ExplosionPrefab, pos, Quaternion.identity) as GameObject);
    //    //Invoke("Dead", 2f);
    //    Invoke("StopExplosionAnimation", 1f);

    //}



    //void StopExplosionAnimation()
    //{
    //    //if (InsExplosion!=null)
    //    {
    //        while (InsExplosions.Count > 0)
    //        {
    //            Destroy(InsExplosions[0]);
    //            InsExplosions.RemoveAt(0);
    //        }
    //    }
    //}
}
