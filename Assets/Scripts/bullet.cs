using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    static public System.Action Clash;
    public float speed = 10f;
    public int fireLength = 400;
    int length = 0;

    public GameObject ExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //print("start bullet speed:"+speed);

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        //rigidbody2D.velocity = new Vector2(speed, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        length++;
        if (length > fireLength) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Diver" || collision.tag=="Treasure") return;
        if (collision.tag == "Fish")
        {
            Destroy(collision.gameObject);
            FishController.fishList.Remove(collision.gameObject);

            //print("FishController.fishList.Count:"+FishController.fishList.Count);
            var Explosion = GameObject.Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
            Clash?.Invoke();
            if (FishController.fishList.Count == 0)
            {
                GameManager.Instance.CreateSchoolOfFish(Random.Range(1, 10));
            }
        }

        //Destroy(gameObject);

    }
}
