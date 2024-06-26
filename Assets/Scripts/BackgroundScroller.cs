using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public GameObject backgroundPrefab; // Prefab tła
    public float scrollSpeed = 2f; // Prędkość przewijania tła
    public float backgroundHeight = 10f; // Wysokość tła
    public int initialBackgrounds = 3; // Początkowa liczba elementów tła

    private List<GameObject> backgrounds = new List<GameObject>();

    void Start()
    {
        // Tworzenie początkowych elementów tła
        for (int i = 0; i < initialBackgrounds; i++)
        {
            Vector3 spawnPosition = new Vector3(0, i * backgroundHeight, 0); // Pozycja początkowa
            GameObject background = Instantiate(backgroundPrefab, spawnPosition, Quaternion.identity);
            background.transform.SetParent(transform);
            backgrounds.Add(background);
        }
    }

    void Update()
    {
        // Przesuwanie wszystkich elementów tła w dół
        foreach (GameObject background in backgrounds)
        {
            background.transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        }

        // Sprawdzanie, czy pierwszy element tła wyszedł poza ekran i recykling tła
        if (backgrounds[0].transform.position.y < -10f) // Zmieniono granicę na -10
        {
            RecycleBackground();
        }
    }

    void RecycleBackground()
    {
        // Usunięcie pierwszego elementu tła
        GameObject oldBackground = backgrounds[0];
        backgrounds.RemoveAt(0);

        // Przemieszczenie usuniętego elementu na nową pozycję na górze
        float newYPosition = 15f;
        oldBackground.transform.position = new Vector3(0, newYPosition, 0);
        backgrounds.Add(oldBackground);
    }
}
