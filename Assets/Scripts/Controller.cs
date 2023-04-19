using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{ 
    GameOver, Play, Pause
}

public class Controller : MonoBehaviour, IListener
{

    int countTreasures = 3;
    public int Score = 0;
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
    AudioSource audioSource, audioSource2;
    public TMPro.TextMeshProUGUI scoreText,energyScore;
    int Energy = 10;
    public AudioClip StartAudioClip, FireAudioClip;
    public GameObject bulletPrefab;
    public GameObject[] Treasures;
    public GameObject GameOverPanel;



    public void UpdateScore()
    {
        scoreText.text = (++Score).ToString();
    }

    public static void ExplosionExecute(Transform transform)
    {
        
       // Instantiate(Explosion, transform.position, Quaternion.identity);
    }

    private void Awake()
    {
        
        var audioSources = GetComponentsInChildren<AudioSource>();
        audioSource = audioSources[0];
        audioSource2=audioSources[1];
        audioSource.clip = FireAudioClip;
        audioSource2.clip = StartAudioClip;
        Score = 0;
        //Bullet.Clash += UpdateScore;
        //DiverController.diverClash = EnergyLow;
        Energy = 10;
        countTreasures = 3;

    }

    private void EnergyLow()
    {
        Energy--;
        energyScore.text = (Energy).ToString();
        if (Energy<=0)
        {
            GameOver();
        }

    }

    void GameOver()
    {
        gameStatus = GameStatus.GameOver;
        GameOverPanel.SetActive(true);
    }

    void Start()
    {
        Ship = transform;
        Diver = GameObject.FindGameObjectsWithTag("Diver")[0].transform;
        DiverScale = Diver.localScale;
        lineRenderer=GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        DiverAnimation = GetComponentInChildren<Animator>();
        EventManager.Instance.AddListener(EVENT_TYPE.HEALTH_CHANGE, this);
        EventManager.Instance.AddListener(EVENT_TYPE.CLASH, this);

        //Fish.Explosion += Fish_Explosion;
        //Controller.Explosion = GameObject.Find("Explosion");

        //print(Diver);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        gameStatus = GameStatus.Play;
        FishController.fishList.Clear();
        /*GameOverPanel.SetActive(false);
        gameStatus = GameStatus.Play;
        Energy = 11;
        EnergyLow();
        NextLevel();
        */
    }

    void Fire(Vector2 pos)
    {
        var bullet = GameObject.Instantiate(bulletPrefab, pos, Quaternion.identity); 
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.velocity = Diver.localScale.x>0?new Vector2(10f,0f): new Vector2(-10f, 0f);
        //audioSource.Stop();
        audioSource.clip = FireAudioClip;
        audioSource.Play();
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
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                        {
                            Fire(Diver.transform.position);
                        }

                    //print(Diver.position.y);

                    //   Debug.DrawLine(Ship.position, Diver.position);
                    //if (Ship.position.x + move * dx >= leftBound.x && Ship.position.x + move * dx <= rightBound.x)
                    {
                        //Ship.Translate(move * Input.GetAxis("Horizontal"), 0, 0);
                        Ship.position = new Vector3((float)Ship.position.x + move * dx, (float)Ship.position.y, (float)Ship.position.z);
                        if (Diver.position.y + move * dy < -4.098 && dx != 0)
                        {
                            //DiverAnimation.enabled = true;
                            DiverAnimation.SetBool("Walk", true);
                        }
                        if (Diver.position.y + move * dy > -4.098 || dx == 0)

                        {
                            //DiverAnimation.enabled = false;
                            DiverAnimation.SetBool("Walk", false);
                        }
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
                            //print("holdTreasure arrived" + Diver.transform.position);
                            //audioSource.Stop();
                            audioSource2.Play();
                            //Destroy(holdTreasure);
                            holdTreasure.SetActive(false);
                            holdTreasure = null;
                            countTreasures--;
                            if (countTreasures == 0)
                            {
                                NextLevel();
                            }
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

    void NextLevel()
    {
        countTreasures = 3;
        for (int i = 0; i < Treasures.Length; i++)
        {
            Treasures[i].transform.position = new Vector2(Random.Range(-10.30f, 10.50f), -4.42f);
            Treasures[i].SetActive(true);
        }
        
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, Object Param = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.GAME_INIT:
                break;
            case EVENT_TYPE.GAME_END:
                break;
            case EVENT_TYPE.HEALTH_CHANGE:
                EnergyLow();
                break;
            case EVENT_TYPE.DEAD:
                break;
            case EVENT_TYPE.CLASH:
                UpdateScore();
                break;
            default:
                break;
        }
    }
}
