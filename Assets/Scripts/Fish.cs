using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour
{
    public float Speed;

    public static event System.Action<Vector3> Explosion;
    
    public Transform[] waypoints;

    public GameObject hp;

    int curWaypointIndex = 0;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //public string gateTag = "gate1";
    //void Start()
    //{
    //    waypoints = GameObject.FindGameObjectWithTag(gateTag).GetComponent<WaveSpawn>().WayPoints;
    //}
    void Update()
    {
        if (curWaypointIndex < waypoints.Length)
        {
            //transform.position = Vector3.Lerp(transform.position, waypoints[curWaypointIndex].position, Time.deltaTime * Speed);
             float dirx=(waypoints[curWaypointIndex].position-transform.position).x;
            if (dirx < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[curWaypointIndex].position, Time.deltaTime * Speed);

            //если достигли одной из точек, то переходим к другой точке
            if (Vector3.Distance(transform.position, waypoints[curWaypointIndex].position) < 0.5f)
            {
                curWaypointIndex++;
            }
        }        
        else curWaypointIndex = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Diver")
        {
            //Controller.ExplosionExecute(collision.gameObject.transform);
            Explosion(collision.transform.position);
            Controller.gameStatus = GameStatus.GameOver;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }


}