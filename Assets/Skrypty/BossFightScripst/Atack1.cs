using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack1 : MonoBehaviour {
    public float speed;
    public float disappearTime;
    private float elapsedTime = 0f;
    private bool boom = false;
    public Sprite sprite;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        if(!boom) transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
        if (!boom && transform.position.y < 297)
        {
            boom = true;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            transform.localScale = new Vector3(2, 2, 2);
            AudioManager.instance.PlaySmallExplosion();
        }
        if(boom)
        {
            elapsedTime += Time.fixedDeltaTime;
            if(elapsedTime >= disappearTime)
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
        if(collision.tag == "Player")
        {
            boom = true;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            transform.localScale = new Vector3(2, 2, 2);
            AudioManager.instance.PlaySmallExplosion();
        }
    }

}
