using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour
{
    public float Speed;
    Rigidbody2D rigidbody2d;
    int lenPath = 0, lenPathBeforeRotate;
    
    public Transform[] waypoints;

    public GameObject hp;

    int curWaypointIndex = 0;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        lenPathBeforeRotate=1000+500*Random.Range(1,5);
    }
    //public string gateTag = "gate1";
    //void Start()
    //{
    //    waypoints = GameObject.FindGameObjectWithTag(gateTag).GetComponent<WaveSpawn>().WayPoints;
    //}
    void Update()
    {
        lenPath++;
        if (lenPath > lenPathBeforeRotate)
        {
            lenPath = 0;
            rigidbody2d.velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(-0.5f, .5f));
            if (rigidbody2d.velocity.x < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
        if (transform.position.y >= 2.71)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, Mathf.Abs(rigidbody2d.velocity.y) * -1f);
            
        }
        if (transform.position.y <= -4.67) rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, Mathf.Abs(rigidbody2d.velocity.y) * 1f);


        //if (curWaypointIndex < waypoints.Length)
        //{
        //    //transform.position = Vector3.Lerp(transform.position, waypoints[curWaypointIndex].position, Time.deltaTime * Speed);
        //     float dirx=(waypoints[curWaypointIndex].position-transform.position).x;
        //    if (dirx < 0)
        //    {
        //        spriteRenderer.flipX = true;
        //    }
        //    else
        //    {
        //        spriteRenderer.flipX = false;
        //    }
        //    transform.position = Vector3.MoveTowards(transform.position, waypoints[curWaypointIndex].position, Time.deltaTime * Speed);

        //    //если достигли одной из точек, то переходим к другой точке
        //    if (Vector3.Distance(transform.position, waypoints[curWaypointIndex].position) < 0.5f)
        //    {
        //        curWaypointIndex++;
        //    }
        //}        
        //else curWaypointIndex = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
//        if (collision.gameObject.tag == "Diver")
//        {
//            Controller.ExplosionExecute(collision.gameObject.transform);
////            Explosion(collision.transform.position);
//            Controller.gameStatus = GameStatus.GameOver;
//        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
  //      Explosion(collision.transform.position);
        //Controller.gameStatus = GameStatus.GameOver;
    }


}