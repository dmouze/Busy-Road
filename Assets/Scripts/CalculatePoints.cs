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
        // Aktualizacja wyświetlania punktów na ekranie
        pointsOnScreen.text = points.ToString();
    }

    public void AddPoints()
    {
        // Dodawanie punktów
        pointsOnScreen.text = points.ToString();
    }
}