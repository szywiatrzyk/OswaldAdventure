using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Rigidbody2D ruch; //
    Vector3 startPosition;
    public bool isactive = false;
    public bool goRight = false;       // Czy wróg idzie w prawo
    public float distance;    // Dystans od pozycji startowej, o który oddala się wróg (zarówno w prawo jak i w lewo)
    public float speed;       // Prędkość wroga
    public bool axis = false;
    /// <summary>
    /// Platforma ruszająca się
    /// </summary>
    void Start()
    {
        ruch = GetComponentInChildren<Rigidbody2D>();
        startPosition = new Vector3(transform.position.x, transform.position.y, 0);

    }

    /// <summary>
    /// Funkcja wywoływana co ustalony framerate.
    /// </summary>
    private void FixedUpdate()
    {

        if (isactive == true)
        {

            if (axis == false)
            {
                // Ruch w prawo i w lewo
                if (goRight == true)
                {
                    transform.position = new Vector3(transform.position.x + speed, transform.position.y, 0);
                    if (Mathf.Abs(startPosition.x - transform.position.x) >= distance && transform.position.x > startPosition.x)
                    {
                        goRight = false;
                    }
                }
                else
                {
                    transform.position = new Vector3(transform.position.x - speed, transform.position.y, 0);
                    if ((startPosition.x - transform.position.x) >= distance)
                    {
                        goRight = true;
                    }
                }
            }
            if (axis == true)
            {
                // Ruch w górę i w dół
                if (goRight == true)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0);
                    if (Mathf.Abs(startPosition.y - transform.position.y) >= distance && transform.position.y > startPosition.y)
                    {
                        goRight = false;
                    }
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed, 0);
                    if ((startPosition.y - transform.position.y) >= distance)
                    {
                        goRight = true;
                    }
                }
            }
        }
    }


}