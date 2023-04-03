using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{ 
    GameOver, Play, Pause
}

public class Controller : MonoBehaviour
{
    

    static public GameObject holdTreasure = null;
    static public GameStatus gameStatus=GameStatus.Play;
    // Start is called before the first frame update
    public float speed;
    Transform Ship;
    Transform Diver;
    public Vector3 DiverOffset;
    public Vector3 LineOffset, LineOffset2;
    LineRenderer lineRenderer;
    public Vector3 leftBound, rightBound;
    Vector3 DiverScale;
    Animator DiverAnimation;
    public GameObject ExplosionPrefab;
    GameObject InsExplosion;

    public static void ExplosionExecute(Transform transform)
    {
        
       // Instantiate(Explosion, transform.position, Quaternion.identity);
    }

    void Start()
    {
        Ship = transform;
        Diver = GameObject.FindGameObjectsWithTag("Diver")[0].transform;
        DiverScale = Diver.localScale;
        lineRenderer=GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        DiverAnimation = GetComponentInChildren<Animator>();
        Fish.Explosion += Fish_Explosion;
        //Controller.Explosion = GameObject.Find("Explosion");

        //print(Diver);
    }

    private void Fish_Explosion(Vector3 pos)
    {
        InsExplosion=GameObject.Instantiate(ExplosionPrefab, pos, Quaternion.identity) as GameObject;
        Invoke("Dead", 2f);
        Invoke("StopExplosionAnimation", 2f);

    }

    void StopExplosionAnimation()
    {
        //if (InsExplosion!=null)
        {
            Destroy(InsExplosion);
        }
    }
    void Dead()
    {
        gameStatus=GameStatus.Play;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameStatus)
        {
            case GameStatus.GameOver:
                {
                    break;
                }
                case GameStatus.Play:
                {
                    lineRenderer.SetPosition(0, Ship.position + LineOffset);
                    lineRenderer.SetPosition(1, Diver.position + LineOffset2);
                    float move = speed * Time.deltaTime;
                    float dx = Input.GetAxis("Horizontal");
                    float dy = Input.GetAxis("Vertical");
                    //if (Input.GetButtonDown("left")) Diver.localScale = new Vector3(-DiverScale.x, DiverScale.y, 0);
                    //if (Input.GetButtonDown("right")) Diver.localScale = new Vector3(DiverScale.x, DiverScale.y, 0);
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) Diver.localScale = new Vector3(-DiverScale.x, DiverScale.y, 0);
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) Diver.localScale = new Vector3(DiverScale.x, DiverScale.y, 0);


                    //print(Diver.position.y);

                    //   Debug.DrawLine(Ship.position, Diver.position);
                    //if (Ship.position.x + move * dx >= leftBound.x && Ship.position.x + move * dx <= rightBound.x)
                    {
                        //Ship.Translate(move * Input.GetAxis("Horizontal"), 0, 0);
                        Ship.position = new Vector3((float)Ship.position.x + move * dx, (float)Ship.position.y, (float)Ship.position.z);
                        if (Diver.position.y + move * dy < -4.098 && dx != 0)
                            DiverAnimation.enabled = true;
                        if (Diver.position.y + move * dy > -4.098 || dx == 0)
                            DiverAnimation.enabled = false;
                        if (Diver.position.y + move * dy <= -4.1)
                        {
                            Diver.position = new Vector3((float)Ship.position.x, -4.1f, 0);
                        }

                        if (Diver.position.y + move * dy <= 2.02)
                            //Diver.Translate(0, move * Input.GetAxis("Vertical"), 0, 0);
                            Diver.position = new Vector3((float)Ship.position.x, (float)Diver.position.y + move * dy, 0);
                        else
                        if (holdTreasure != null)
                        {
                            print("holdTreasure arrived" + Diver.transform.position);

                            Destroy(holdTreasure);
                            holdTreasure = null;
                        }
                        if (holdTreasure != null)
                            holdTreasure.transform.position = Diver.position - Treasure.delta;
                    }
                    //else Ship.position=new Vector3(Mathf.Clamp(Ship.position.x,- 5.59f, 7.73f), Ship.position.y,0);


                    //Vector3 DiverPosition = Ship.position + DiverOffset+new Vector3(0,Input.GetAxis("Vertical"),0);
                    //Diver.SetPositionAndRotation(DiverPosition, Quaternion.identity);

                    //transform.position=new Vector3(move,0,0);
                    break;
                }
    }
    }
}
