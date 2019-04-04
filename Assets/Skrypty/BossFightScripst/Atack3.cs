using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack3 : MonoBehaviour {

    public Sprite[] sprites;
    private float elapsedTime = 0f;
    public float greenTime, yellowTime, redTime;
    bool green;
    bool yellow;
    bool red;
    private float alpha = 0f;
    // Use this for initialization
    void Start () {
        green = true;
        yellow = red = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        transform.localScale = new Vector3(1.2f, 0.78f, 1);
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = alpha;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (green)
            {
                if (alpha < 255f)
                {
                    Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
                    tmp.a = alpha;
                    gameObject.GetComponent<SpriteRenderer>().color = tmp;
                    alpha += 0.02f;
                }
                elapsedTime += Time.fixedDeltaTime;
                if (elapsedTime > greenTime)
                {
                    green = false;
                    yellow = true;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                    elapsedTime = 0;
                }
            }
            if (yellow)
            {

                elapsedTime += Time.fixedDeltaTime;
                if (elapsedTime > yellowTime)
                {
                    yellow = false;
                    red = true;
                    elapsedTime = 0;
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
                    gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                }
            }
            if (red)
            {
                if (elapsedTime == 0) AudioManager.instance.PlayLaser();
                elapsedTime += Time.fixedDeltaTime;
                if (elapsedTime > redTime)
                {
                    red = false;
                    elapsedTime = 0;
                    Destroy(gameObject);
                }
            }

        }
    }
}
