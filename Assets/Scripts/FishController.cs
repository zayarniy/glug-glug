using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fishPrefab;
    static public List<GameObject> fishList = new List<GameObject>();
    


    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateSchoolOfFish(int n=10)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject fish = GameObject.Instantiate(fishPrefab, new Vector2(-11f+i*0.2f, i * 0.1f), Quaternion.identity) as GameObject;
            Rigidbody2D rigidbody2d = fish.GetComponent<Rigidbody2D>();
            rigidbody2d.velocity=new Vector2(2f, i*0.1f);
            fishList.Add(fish);

        }
        
    }
}
