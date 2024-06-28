using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Tablica prefabów samochodów
    public Transform[] spawnPoints; // Punkty początkowe dla samochodów
    public float minSpawnInterval = 1f; // Minimalny interwał generowania samochodów
    public float maxSpawnInterval = 3f; // Maksymalny interwał generowania samochodów
    private float globalTimer;
    private float nextSpawnTime;
    private float speed = 3f; // Początkowa prędkość samochodów
    private List<GameObject> activeCars = new List<GameObject>(); // Lista aktywnych samochodów
    private int maxCarsOnScreen = 4; // Maksymalna liczba samochodów na ekranie
    private float laneSpawnDelay = 0.1f; // Opóźnienie między spawnami na różnych pasach

    void Start()
    {
        globalTimer = 0f;
        nextSpawnTime = GetRandomSpawnInterval(minSpawnInterval, maxSpawnInterval);
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        globalTimer += Time.deltaTime;

        // Sprawdzaj, czy można zespawnować nowy samochód tylko, jeśli liczba aktywnych samochodów jest mniejsza niż maksymalna
        if (activeCars.Count < maxCarsOnScreen && globalTimer >= nextSpawnTime)
        {
            StartCoroutine(SpawnCarsOnLanes());
            globalTimer = 0f;
            nextSpawnTime = GetRandomSpawnInterval(minSpawnInterval, maxSpawnInterval); // Reset globalnego timera
        }

        // Usuń z listy aktywne samochody, które zostały zniszczone
        activeCars.RemoveAll(car => car == null);
    }

    IEnumerator SpawnCarsOnLanes()
    {
        int lanesToSpawn = 3; // Minimalna liczba pasów do zaspawnienia samochodów

        // Sprawdzaj, które pasy są już zajęte przez aktywne samochody
        bool[] lanesOccupied = new bool[spawnPoints.Length];
        foreach (var car in activeCars)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (Vector3.Distance(car.transform.position, spawnPoints[i].position) < 1.0f)
                {
                    lanesOccupied[i] = true;
                    break;
                }
            }
        }

        List<int> availableLanes = new List<int>();
        for (int i = 0; i < lanesOccupied.Length; i++)
        {
            if (!lanesOccupied[i])
            {
                availableLanes.Add(i);
            }
        }

        // Upewnij się, że są co najmniej 3 wolne pasy
        if (availableLanes.Count >= lanesToSpawn)
        {
            availableLanes.Shuffle(); // Losowe przetasowanie listy dostępnych pasów

            for (int i = 0; i < lanesToSpawn; i++)
            {
                int laneIndex = availableLanes[i];
                bool moveDown = laneIndex < 2; // Pas 1 i 2 - w dół, Pas 3 i 4 - w górę
                SpawnCarAt(spawnPoints[laneIndex], moveDown);
                yield return new WaitForSeconds(laneSpawnDelay);
            }
        }
    }

    bool SpawnCarAt(Transform spawnPoint, bool moveDown)
    {
        foreach (var car in activeCars)
        {
            if (Vector3.Distance(car.transform.position, spawnPoint.position) < 1.0f)
            {
                // Samochód już znajduje się na tym pasie, nie spawnuj nowego
                return false;
            }
        }

        try
        {
            // Wybierz losowy prefab samochodu
            int carIndex = Random.Range(0, carPrefabs.Length);
            GameObject car = Instantiate(carPrefabs[carIndex], spawnPoint.position, spawnPoint.rotation);
            activeCars.Add(car); // Dodaj nowo zespawnowany samochód do listy aktywnych

            // Ustaw prędkość samochodu
            CarMovement carMovement = car.GetComponent<CarMovement>();
            if (carMovement != null)
            {
                carMovement.SetSpeed(speed);
                carMovement.moveDown = moveDown;
            }
            else
            {
                Debug.LogError("Car prefab does not have CarMovement component");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Exception in SpawnCarAt: " + e.Message);
            return false;
        }
        return true;
    }

    float GetRandomSpawnInterval(float minInterval, float maxInterval)
    {
        return Random.Range(minInterval, maxInterval);
    }

    IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            speed += 0.5f; // Zwiększ prędkość co 10 sekund
        }
    }
}

// Helper method to shuffle a list
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
