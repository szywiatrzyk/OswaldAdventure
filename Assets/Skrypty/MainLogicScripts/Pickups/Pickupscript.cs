using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupscript : MonoBehaviour
{
    private bool pick;           // Czy pickup został zebrany
    private GameObject thispick; // Odwołanie do samego siebie
    
    /// <summary>
    /// Inicjalizacja checkpointa.
    /// </summary>
	void Start ()
    {
        pick = false;
        thispick = this.gameObject;
	}

    /// <summary>
    /// Obsługa zebrania pickupa.
    /// </summary>
    /// <param name="collision">Collider, z którym zetknął się pickup.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Deaktywacja pickupa po zetknięciu z graczem.
        if (collision.tag == "Player")
        {
            thispick.SetActive(false);
            if(this.tag == "coin")
            {
                AudioManager.instance.PlayCoin();
                GameLogic.instance.ScoreAdd(1);
            }
            if (this.tag == "hp_up")
            {
                AudioManager.instance.PlayHeart();
                GameLogic.instance.ScoreAdd(2);
            }

        }
    }
}
