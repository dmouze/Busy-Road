using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculatePoints : MonoBehaviour
{
    public int points;
    public TextMeshProUGUI pointsOnScreen;

    private void Update()
    {
        // Aktualizacja wy�wietlania punkt�w na ekranie
        pointsOnScreen.text = points.ToString();
    }

    public void AddPoints()
    {
        // Dodawanie punkt�w
        pointsOnScreen.text = points.ToString();
    }
}
