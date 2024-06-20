using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Tablica prefabów samochodów
    public Transform[] spawnPoints; // Punkty początkowe dla samochodów
    public float minSpawnIntervalLane1 = 2f; // Minimalny interwał generowania samochodów na pasach 1 i 2
    public float maxSpawnIntervalLane1 = 6f; // Maksymalny interwał generowania samochodów na pasach 1 i 2
    public float minSpawnIntervalLane3 = 2f; // Minimalny interwał generowania samochodów na pasach 3 i 4
    public float maxSpawnIntervalLane3 = 6f; // Maksymalny interwał generowania samochodów na pasach 3 i 4
    private float timerLane1;
    private float timerLane3;
    private float speed = 2f; // Początkowa prędkość samochodów

    void Start()
    {
        timerLane1 = GetRandomSpawnInterval(minSpawnIntervalLane1, maxSpawnIntervalLane1);
        timerLane3 = GetRandomSpawnInterval(minSpawnIntervalLane3, maxSpawnIntervalLane3);
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        timerLane1 -= Time.deltaTime;
        timerLane3 -= Time.deltaTime;

        if (timerLane1 <= 0)
        {
            SpawnCarAt(spawnPoints[0], true); // Pas 1 - w dół
            timerLane1 = GetRandomSpawnInterval(minSpawnIntervalLane1, maxSpawnIntervalLane1);
        }

        if (timerLane3 <= 0)
        {
            SpawnCarAt(spawnPoints[2], false); // Pas 3 - w górę
            timerLane3 = GetRandomSpawnInterval(minSpawnIntervalLane3, maxSpawnIntervalLane3);
        }
    }

    void SpawnCarAt(Transform spawnPoint, bool moveDown)
    {
        // Wybierz losowy prefab samochodu
        int carIndex = Random.Range(0, carPrefabs.Length);
        GameObject car = Instantiate(carPrefabs[carIndex], spawnPoint.position, spawnPoint.rotation);

        // Debugowanie
        Debug.Log("Spawning car at: " + spawnPoint.position + " with prefab index: " + carIndex + " and moveDown: " + moveDown);

        // Ustaw prędkość samochodu
        CarMovement carMovement = car.GetComponent<CarMovement>();
        if (carMovement != null)
        {
            carMovement.SetSpeed(speed);
            carMovement.moveDown = moveDown;
        }
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
