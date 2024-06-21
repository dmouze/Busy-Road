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
        for (int i = 0; i < initialBackgrounds; i++)
        {
            Vector3 spawnPosition = new Vector3(0, i * backgroundHeight, 0);
            GameObject background = Instantiate(backgroundPrefab, spawnPosition, Quaternion.identity);
            background.transform.SetParent(transform);
            backgrounds.Add(background);
        }
    }

    void Update()
    {
        foreach (GameObject background in backgrounds)
        {
            background.transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        }

        if (backgrounds[0].transform.position.y < -backgroundHeight)
        {
            RecycleBackground();
        }
    }

    void RecycleBackground()
    {
        GameObject oldBackground = backgrounds[0];
        backgrounds.RemoveAt(0);

        float newYPosition = backgrounds[backgrounds.Count - 1].transform.position.y + backgroundHeight;
        oldBackground.transform.position = new Vector3(0, newYPosition, 0);
        backgrounds.Add(oldBackground);
    }
}
