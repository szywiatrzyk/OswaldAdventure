using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHunter : MonoBehaviour {

    Vector3 whereAttack;
    Vector3 startpos;
    public bool istarget;
    public bool rushingOnPlayer;
    public float speed;
    public float distance;
    public float coliderRadius;
    public bool onStart;
    public GameObject thisobject;
    public Animator anim;
    

	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = true;
        startpos = this.transform.position;
        istarget = false;
        rushingOnPlayer = false;
        onStart = true;
        thisobject = this.gameObject; 
    }
	

	void Update () {
		
	}

    private void FixedUpdate()
    {
        
        if (!rushingOnPlayer && !onStart)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, startpos, speed * Time.deltaTime);
            FlipCharacterSprite(startpos);
            if (transform.position == startpos) { onStart = true; anim.enabled = true; anim.Play("Idle", -1, 0f); }
        }
        
        if (rushingOnPlayer)
        {
            whereAttack = Player.instance.currentPosition;
            distance = Vector3.Distance(transform.position, whereAttack);

            if (distance < coliderRadius)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, whereAttack, speed * Time.deltaTime);
                FlipCharacterSprite(whereAttack);
            }
            else { istarget = false; rushingOnPlayer = false; }
            
        }


    
    }

    public void Target(float radius) {
        if (istarget == false)
        {
            
            anim.enabled = false;
            coliderRadius=radius;
            rushingOnPlayer = true;
            istarget = true;
            onStart = false;
        }
    }

   

    void FlipCharacterSprite(Vector3 direct)
    {
        if (transform.position.x < direct.x) thisobject.GetComponent<SpriteRenderer>().flipX = false;
        else thisobject.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Reset()
    {
        transform.position = startpos;
        thisobject.SetActive(true);
        istarget = false;
        thisobject.GetComponent<SpriteRenderer>().flipX = true;
        rushingOnPlayer = false;
        onStart = true;
        anim.enabled = true;
        anim.Play("Idle", -1, 0f);
    }

}
