using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
     FishController fishController;

    static GameManager instance=null;

    static public GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(instance);
            return;
        }
        instance = this;
        fishController=GetComponent<FishController>();
    }
    void Start()
    {
        CreateSchoolOfFish(Random.Range(1, 10));        
    }


    public void CreateSchoolOfFish(int n)
    {
        fishController.CreateSchoolOfFish(n);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
