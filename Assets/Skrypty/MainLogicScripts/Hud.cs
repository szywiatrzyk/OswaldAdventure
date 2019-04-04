using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Klasa <c>Hud</c> kontroluje wyświetlanie serc symbolizujących liczbę żyć gracza.
/// </summary>
public class Hud : MonoBehaviour {

    public Sprite[] Hearts;        // Tablica sprite'ów serduszek
    public Image HeartUI;          // 
    public Sprite[] BossHP;        // Tablica sprite'ów serduszek
    public Image BossHPUI;

    /// <summary>
    /// Funkcja wywoływana co ustalony framerate.
    /// </summary>
    private void FixedUpdate()
    {
        // Ustawienie wyświetlanego sprite'a zależnie od ilości żyć pozostałych graczowi
        HeartUI.sprite = Hearts[Player.instance.life];
        BossHPUI.sprite = BossHP[Boss.instance.hp];
    }

}
