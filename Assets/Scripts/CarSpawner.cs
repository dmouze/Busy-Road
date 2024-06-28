using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Tablica prefabów samochodów
    public Transform[] spawnPoints; // Punkty początkowe dla samochodów
    public float minSpawnIntervalLane1 = 3f; // Minimalny interwał generowania samochodów na pasach 1
    public float maxSpawnIntervalLane1 = 7f; // Maksymalny interwał generowania samochodów na pasach 1
    public float minSpawnIntervalLane2 = 3f; // Minimalny interwał generowania samochodów na pasach 2
    public float maxSpawnIntervalLane2 = 7f; // Maksymalny interwał generowania samochodów na pasach 2
    public float minSpawnIntervalLane3 = 3f; // Minimalny interwał generowania samochodów na pasach 3
    public float maxSpawnIntervalLane3 = 7f; // Maksymalny interwał generowania samochodów na pasach 3
    public float minSpawnIntervalLane4 = 3f; // Minimalny interwał generowania samochodów na pasach 4
    public float maxSpawnIntervalLane4 = 7f; // Maksymalny interwał generowania samochodów na pasach 4
    private float timerLane1;
    private float timerLane2;
    private float timerLane3;
    private float timerLane4;
    private float speed = 3f; // Początkowa prędkość samochodów
    private List<GameObject> activeCars = new List<GameObject>(); // Lista aktywnych samochodów
    private int maxCarsOnScreen = 4; // Maksymalna liczba samochodów na ekranie
    private float globalSpawnCooldown = 0.5f; // Minimalny odstęp czasowy między tworzeniem samochodów
    private float globalSpawnTimer;

    void Start()
    {
        timerLane1 = GetRandomSpawnInterval(minSpawnIntervalLane1, maxSpawnIntervalLane1);
        timerLane2 = GetRandomSpawnInterval(minSpawnIntervalLane2, maxSpawnIntervalLane2);
        timerLane3 = GetRandomSpawnInterval(minSpawnIntervalLane3, maxSpawnIntervalLane3);
        timerLane4 = GetRandomSpawnInterval(minSpawnIntervalLane4, maxSpawnIntervalLane4);
        globalSpawnTimer = globalSpawnCooldown;
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        timerLane1 -= Time.deltaTime;
        timerLane2 -= Time.deltaTime;
        timerLane3 -= Time.deltaTime;
        timerLane4 -= Time.deltaTime;
        globalSpawnTimer -= Time.deltaTime;

        // Sprawdzaj, czy można zespawnować nowy samochód tylko, jeśli liczba aktywnych samochodów jest mniejsza niż maksymalna
        if (activeCars.Count < maxCarsOnScreen && globalSpawnTimer <= 0)
        {
            bool carSpawned = false;
            if (timerLane1 <= 0)
            {
                carSpawned = SpawnCarAt(spawnPoints[0], true); // Pas 1 - w dół
                if (carSpawned)
                    timerLane1 = GetRandomSpawnInterval(minSpawnIntervalLane1, maxSpawnIntervalLane1);
            }

            if (timerLane2 <= 0 && !carSpawned)
            {
                carSpawned = SpawnCarAt(spawnPoints[1], true); // Pas 2 - w dół
                if (carSpawned)
                    timerLane2 = GetRandomSpawnInterval(minSpawnIntervalLane2, maxSpawnIntervalLane2);
            }

            if (timerLane3 <= 0 && !carSpawned)
            {
                carSpawned = SpawnCarAt(spawnPoints[2], false); // Pas 3 - w górę
                if (carSpawned)
                    timerLane3 = GetRandomSpawnInterval(minSpawnIntervalLane3, maxSpawnIntervalLane3);
            }

            if (timerLane4 <= 0 && !carSpawned)
            {
                carSpawned = SpawnCarAt(spawnPoints[3], false); // Pas 4 - w górę
                if (carSpawned)
                    timerLane4 = GetRandomSpawnInterval(minSpawnIntervalLane4, maxSpawnIntervalLane4);
            }

            if (carSpawned)
            {
                globalSpawnTimer = globalSpawnCooldown; // Reset globalnego timera
            }
        }

        // Usuń z listy aktywne samochody, które zostały zniszczone
        activeCars.RemoveAll(car => car == null);
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
