using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float[] lanePositions = { -2.09f, -1.28f, -0.39f, 0.46f }; // Wartości x dla każdego pasa
    public int initialLane = 0; // Początkowy pas, 0 to najbardziej lewy pas
    private int currentLane; // Bieżący pas, na którym znajduje się samochód
    private Vector3 targetPosition; // Docelowa pozycja samochodu

    void Start()
    {
        // Ustawienie bieżącego pasa na podstawie początkowej pozycji samochodu
        currentLane = initialLane;

        // Ustawienie początkowej docelowej pozycji na podstawie bieżącego pasa
        UpdateTargetPosition(true);

        // Ustawienie pozycji samochodu na docelową pozycję
        transform.position = targetPosition;

        // Debugowanie pozycji początkowej
        Debug.Log("Initial Lane: " + initialLane);
        Debug.Log("Initial Position: " + transform.position);
        Debug.Log("Target Position: " + targetPosition);
    }

    void Update()
    {
        // Sprawdzenie, czy nastąpiło dotknięcie ekranu
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Sprawdzenie, w której części ekranu nastąpiło dotknięcie
                if (touch.position.x < Screen.width / 2)
                {
                    MoveLeft();
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    MoveRight();
                }
            }
        }

        // Dodanie obsługi kliknięcia myszy dla testowania w edytorze
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            if (mousePosition.x < Screen.width / 2)
            {
                MoveLeft();
            }
            else if (mousePosition.x > Screen.width / 2)
            {
                MoveRight();
            }
        }

        // Przemieszczanie samochodu do docelowej pozycji
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5);
    }

    void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            UpdateTargetPosition(false);
        }
    }

    void MoveRight()
    {
        if (currentLane < lanePositions.Length - 1)
        {
            currentLane++;
            UpdateTargetPosition(false);
        }
    }

    void UpdateTargetPosition(bool isInitial)
    {
        // Aktualizacja docelowej pozycji na podstawie aktualnego pasa
        targetPosition = new Vector3(lanePositions[currentLane], transform.position.y, transform.position.z);

        if (isInitial)
        {
            // Ustawienie początkowej pozycji
            transform.position = targetPosition;
        }

        Debug.Log("Updated Target Position: " + targetPosition);
    }
}
