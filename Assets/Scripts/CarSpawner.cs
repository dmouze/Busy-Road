using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Tablica prefabów samochodów
    public Transform[] spawnPoints; // Punkty początkowe dla samochodów
    public float minSpawnIntervalLane1 = 1f; // Minimalny interwał generowania samochodów na pasach 1
    public float maxSpawnIntervalLane1 = 4f; // Maksymalny interwał generowania samochodów na pasach 1
    public float minSpawnIntervalLane2 = 1f; // Minimalny interwał generowania samochodów na pasach 2
    public float maxSpawnIntervalLane2 = 4f; // Maksymalny interwał generowania samochodów na pasach 2
    public float minSpawnIntervalLane3 = 1f; // Minimalny interwał generowania samochodów na pasach 3
    public float maxSpawnIntervalLane3 = 4f; // Maksymalny interwał generowania samochodów na pasach 3
    public float minSpawnIntervalLane4 = 1f; // Minimalny interwał generowania samochodów na pasach 4
    public float maxSpawnIntervalLane4 = 4f; // Maksymalny interwał generowania samochodów na pasach 4
    private float timerLane1;
    private float timerLane2;
    private float timerLane3;
    private float timerLane4;
    private float speed = 3f; // Początkowa prędkość samochodów

    void Start()
    {
        timerLane1 = GetRandomSpawnInterval(minSpawnIntervalLane1, maxSpawnIntervalLane1);
        timerLane2 = GetRandomSpawnInterval(minSpawnIntervalLane2, maxSpawnIntervalLane2);
        timerLane3 = GetRandomSpawnInterval(minSpawnIntervalLane3, maxSpawnIntervalLane3);
        timerLane4 = GetRandomSpawnInterval(minSpawnIntervalLane4, maxSpawnIntervalLane4);
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        timerLane1 -= Time.deltaTime;
        timerLane2 -= Time.deltaTime;
        timerLane3 -= Time.deltaTime;
        timerLane4 -= Time.deltaTime;

        if (timerLane1 <= 0)
        {
            SpawnCarAt(spawnPoints[0], true); // Pas 1 - w dół
            timerLane1 = GetRandomSpawnInterval(minSpawnIntervalLane1, maxSpawnIntervalLane1);
        }

        if (timerLane2 <= 0)
        {
            SpawnCarAt(spawnPoints[1], true); // Pas 2 - w dół
            timerLane2 = GetRandomSpawnInterval(minSpawnIntervalLane2, maxSpawnIntervalLane2);
        }

        if (timerLane3 <= 0)
        {
            SpawnCarAt(spawnPoints[2], false); // Pas 3 - w górę
            timerLane3 = GetRandomSpawnInterval(minSpawnIntervalLane3, maxSpawnIntervalLane3);
        }

        if (timerLane4 <= 0)
        {
            SpawnCarAt(spawnPoints[3], false); // Pas 4 - w górę
            timerLane4 = GetRandomSpawnInterval(minSpawnIntervalLane4, maxSpawnIntervalLane4);
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
