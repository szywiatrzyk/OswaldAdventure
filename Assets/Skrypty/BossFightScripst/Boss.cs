using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private bool goDown = true;
    private bool goUp = false;
    private bool resting = false;
    private bool stay = false;
    private bool tired = false;
    public int attackCount;
    public int howManyAttackBefore ;
    public int hp;
    private bool hit = true;
    private float restTime =0;
    private bool goRight = false;
    public bool attackMode = false;
    private bool readyToAttack = false;
    private bool attack3 = false;
    private SpriteRenderer character;
    public float speed;
    public float attack1Speed;
    private float currentX;
    private float attack2X;
    public float elapsedTime = 0f;
    public float attack2Interval;
    private float attack2Elapsed = 0f;
    private int ufoCounter = 0;
    public float positionSaveTime;
    public int attackType = 0;
    private Vector3 restPosition;
    private Vector3 startPosition;
    UnityEngine.Object rocketPrefab;
    UnityEngine.Object ufoPrefab;
    UnityEngine.Object beamPrefab;
    UnityEngine.Object laserPrefab;
    System.Random rand;
    BoxCollider2D col;
    public static Boss instance;
    public Sprite explosion;
    // Use this for initialization
    void Start () {
        instance = this;
        startPosition = transform.position;
        character = GetComponentInChildren<SpriteRenderer>();
        attack2X = currentX = transform.position.x;
        rocketPrefab = Resources.Load("Atack1");
        ufoPrefab = Resources.Load("Atack2");
        beamPrefab = Resources.Load("Atack3");
        laserPrefab = Resources.Load("Laser");
        rand = new System.Random(Guid.NewGuid().GetHashCode());
        attackCount = 0;
        restPosition = new Vector3(attack2X, transform.position.y - 6, transform.position.z);
        col = gameObject.GetComponent<BoxCollider2D>();
        col.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameLogic.instance.currentLevel == 4 && Time.timeScale != 0)
        {
            if (Player.instance.life == 0) Reset();

            if (hp > 0)
            {
                if (!attackMode && !stay)
                {
                    if (goRight)
                    {
                        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                        if (transform.position.x >= 1168.9)
                        {
                            goRight = false;
                            character.flipX = false;
                        }
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                        if (transform.position.x <= 1144.5)
                        {
                            goRight = true;
                            character.flipX = true;
                        }
                    }
                }
                else if (attackMode)
                {
                    switch (attackType)
                    {
                        case 1:
                            if (transform.position.x != Player.instance.transform.position.x)
                            {
                                Vector3 destination = new Vector3(Player.instance.transform.position.x, transform.position.y, transform.position.z);
                                transform.position = Vector3.MoveTowards(transform.position, destination, attack1Speed * Time.deltaTime);
                                if (Player.instance.transform.position.x < transform.position.x) character.flipX = false;
                                else character.flipX = true;
                                if (Mathf.Abs(Player.instance.transform.position.x - transform.position.x) < 0.03f) readyToAttack = true;
                            }
                            if (readyToAttack)
                            {
                                Instantiate(rocketPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
                                attackType = 0;
                                readyToAttack = false;
                                attackMode = false;
                                attackCount++;
                            }
                            break;

                        case 2:
                            if (transform.position.x != attack2X)
                            {
                                Vector3 destination = new Vector3(attack2X, transform.position.y, transform.position.z);
                                transform.position = Vector3.MoveTowards(transform.position, destination, attack1Speed * Time.deltaTime);
                                if (attack2X < transform.position.x) character.flipX = false;
                                else character.flipX = true;
                                if (Mathf.Abs(attack2X - transform.position.x) < 0.01f) readyToAttack = true;
                            }
                            if (readyToAttack)
                            {
                                attack2Elapsed += Time.deltaTime;
                                if (attack2Elapsed >= attack2Interval)
                                {
                                    AudioManager.instance.PlayUfoSpawn();
                                    Instantiate(ufoPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                                    attack2Elapsed = 0f;
                                    ufoCounter++;
                                }
                                if (ufoCounter == 4)
                                {
                                    attackType = 0;
                                    readyToAttack = false;
                                    attackMode = false;
                                    ufoCounter = 0;
                                    attackCount++;
                                }
                            }
                            break;

                        case 3:
                            if (transform.position.x != Player.instance.transform.position.x && !attack3)
                            {
                                Vector3 destination = new Vector3(Player.instance.transform.position.x, transform.position.y, transform.position.z);
                                transform.position = Vector3.MoveTowards(transform.position, destination, attack1Speed * Time.deltaTime);
                                if (Player.instance.transform.position.x < transform.position.x) character.flipX = false;
                                else character.flipX = true;
                                if (Mathf.Abs(Player.instance.transform.position.x - transform.position.x) < 0.03f) readyToAttack = true;
                            }
                            if (readyToAttack)
                            {
                                Instantiate(beamPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y - 5.5f, transform.position.z), transform.rotation);
                                readyToAttack = false;
                                attack3 = true;
                            }
                            if (attack3 && GameObject.FindGameObjectsWithTag("beam").Length == 0)
                            {
                                attackType = 0;
                                attackMode = false;
                                attack3 = false;
                                attackCount++;
                            }
                            break;
                    }
                    if (attackCount == howManyAttackBefore) speed /= 2;

                }
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= positionSaveTime)
                {
                    elapsedTime = 0f;
                    currentX = transform.position.x;
                    attackMode = true;
                    if (attackCount == howManyAttackBefore) tired = true;
                    if (attackType == 0) attackType = rand.Next(1, 4);
                    if (attackType == 1 && !tired) AudioManager.instance.PlayAlarm();
                }
                
                if (attackCount == howManyAttackBefore && tired)
                {
                    //odpoczywa :D
                    stay = true;
                    attackMode = false;
                    if(goDown)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, restPosition, attack1Speed * Time.deltaTime);
                        if (transform.position == restPosition)
                        {
                            resting = true;
                            goDown = false;
                            col.enabled = true;
                        }
                        
                    }
                    else if(resting)
                    {
                        restTime += Time.deltaTime;
                        if (restTime >= 5.0f || hit == false)
                        {
                            resting = false;
                            goUp = true;
                            col.enabled = false;
                        }
                    }
                    else if(goUp)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, startPosition, attack1Speed * Time.deltaTime);
                        if (transform.position == startPosition)
                        {
                            attackMode = false;
                            restTime = 0f;
                            attackCount = 0;
                            hit = true;
                            elapsedTime = 0f;
                            goUp = false;
                            goDown = true;
                            stay = false;
                            tired = false;
                            speed *= 2;
                        }
                    }
                }
            }
            else
            {
                
                Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a -= 0.02f;
                gameObject.GetComponent<SpriteRenderer>().color = tmp;
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= 5)
                {
                    Instantiate(laserPrefab, new Vector3(Player.instance.transform.position.x, 304.1f, transform.position.z), transform.rotation);
                    GameLogic.instance.cameraLock = true;
                    Player.instance.movementAllowed = false;
                    if (GameObject.FindWithTag("HUD"))
                    Destroy(GameObject.FindWithTag("HUD"));
                    Player.instance.ruch.simulated = false;
                    Destroy(gameObject);
                }
            }
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && hit)
        {
            hp = hp - 1;
            hit = false;
            if (hp == 0)
            {
                AudioManager.instance.PlaySmallExplosion();
                gameObject.GetComponent<SpriteRenderer>().sprite = explosion;
                transform.localScale = new Vector3(4, 4, 3);
                elapsedTime = 0f;
                col.enabled = false;
            }
            else
            {
                AudioManager.instance.PlayBossHit();
            }
        }
    }
    public void Reset()
    {
        attackCount = 0;
        hp = 4;
        goRight = false;
        attackMode = false;
        readyToAttack = false;
        attack3 = false;
        restTime = 0;
    }

}
