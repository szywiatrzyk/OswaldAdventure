using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa <c>EnemyWalker</c> [??] obsługuje przeciwnika chodzącego po swoim obszarze.
/// Gracz umiera po zderzeniu się z nim.
/// </summary>
public class EnemyWalker : MonoBehaviour
{
    private Rigidbody2D ruch; //
    Vector3 startPosition;    // Pozycja startowa przeciwnika
    public bool goRight=false;             // Czy wróg idzie w prawo
    public float distance;    // Dystans od pozycji startowej, o który oddala się wróg (zarówno w prawo jak i w lewo)
    public float speed;       // Prędkość wroga
    public bool axis = false;
    /// <summary>
    /// Inicjacja przeciwnika typu Walker.
    /// </summary>
    void Start ()
    {
        ruch = GetComponentInChildren<Rigidbody2D>();
        startPosition = new Vector3(transform.position.x, transform.position.y, 0);
       
    }

    /// <summary>
    /// Funkcja wywoływana co ustalony framerate.
    /// </summary>
    private void FixedUpdate()
    {


        transform.Rotate(0, 0, 6.0f * 30.0f * Time.deltaTime);
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
            // Ruch w prawo i w lewo
            if (goRight == true)
            {
                transform.position = new Vector3(transform.position.x , transform.position.y + speed, 0);
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
