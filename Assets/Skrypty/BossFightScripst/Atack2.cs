using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack2 : MonoBehaviour {
    bool adjustPosition = true, wait = false, seeking = false, boom = false;
    double goRight;
    float destinationX, destinationY;
    public float speed;
    public float waitTime;
    public float seekingTime;
    private float elapsedTime = 0f;
    Vector3 destination;
    System.Random rand;
    public Sprite sprite;
    // Use this for initialization
    void Start () {
        rand = new System.Random(Guid.NewGuid().GetHashCode());
        goRight = rand.NextDouble();
        destinationX = (float)(rand.NextDouble() * 5 + 3.2);
        destinationY = (float)(rand.NextDouble() * 2 + 302);
        if (goRight > 0.5) destinationX = transform.position.x + destinationX;
        else destinationX = transform.position.x - destinationX;
        destination = new Vector3(destinationX, destinationY, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		if(adjustPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position == destination)
            {
                adjustPosition = false;
                wait = true;
            }
        }
        else if(wait)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= waitTime)
            {
                wait = false;
                elapsedTime = 0f;
                seeking = true;
            }
        }
        else if(seeking)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime <= seekingTime) destination = Player.instance.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, destination, speed / 2 * Time.deltaTime);
            if (transform.position == destination)
            {
                elapsedTime = 0f;
                seeking = false;
                boom = true;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                transform.localScale = new Vector3(2, 2, 2);
                AudioManager.instance.PlaySmallExplosion();
            }
        }
        if (boom)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= waitTime)
            {
                Destroy(gameObject);
            }
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a -= 0.1f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.instance.PlaySmallExplosion();
            elapsedTime = 0f;
            seeking = false;
            boom = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            transform.localScale = new Vector3(2, 2, 2);
        }
    }
}
