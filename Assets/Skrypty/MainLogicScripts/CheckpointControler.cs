using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa <c>CheckpointControler</c> kontroluje sprite checkpointa.
/// </summary>
public class CheckpointControler : MonoBehaviour {

    // Parametry
    public bool passed = false; // Czy gracz przeszedł przez dany checkpoint
    public Sprite sprite;       // Domyślny sprite checkpointa
    public Sprite sprite2;      // Sprite checkpointa po aktywacji
	

    /// <summary>
    /// Inicjalizacja checkpointa.
    /// </summary>
	void Start () {
        
	}

    /// <summary>
    /// Funkcja wywoływana co klatkę.
    /// </summary>
	void Update () {
		
	}

    /// <summary>
    /// Reakcja na przejście gracza przez checkpoint.
    /// </summary>
    /// <param name="collision">Collider, który zetknął się z checkpointem</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Zmiana sprite'a po zetknięciu z graczem
        if (collision.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().sprite = sprite;
            //passed = true;
        }
    }

    /// <summary>
    /// Reset checkpointa.
    /// </summary>
    public void Reset()
    {
        this.passed = false;
        this.GetComponent<SpriteRenderer>().sprite = sprite2;
    }
}
