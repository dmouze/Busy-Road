using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculatePoints : MonoBehaviour
{
    public int points;
    public TextMeshProUGUI pointsOnScreen;

    private void Start()
    {
        StartCoroutine(AddPointsOverTime());
    }

    private void Update()
    {
        // Aktualizacja wyświetlania punktów na ekranie
        pointsOnScreen.text = points.ToString();
    }

    public void AddPoints()
    {
        // Dodawanie punktów
        points++;
        pointsOnScreen.text = points.ToString();
    }

    private IEnumerator AddPointsOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Dodaj punkt co sekundę
            AddPoints();
        }
    }
}
