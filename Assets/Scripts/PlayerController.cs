using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform[] lanes; // Pozycje X dla pasów ruchu
    private int currentLane = 1; // Startowy pas (środkowy)
    private CalculatePoints calculatePoints;
    public bool isPaused = false; // Instance field for pause state

    void Start()
    {
        calculatePoints = GameObject.FindObjectOfType<CalculatePoints>();
    }

    void Update()
    {
        if (isPaused)
            return; // Exit update loop if paused

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        // Dodanie obsługi kliknięcia myszy dla testowania w edytorze
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
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

        // Przenoszenie samochodu na odpowiedni pas
        transform.position = new Vector3(lanes[currentLane].position.x, transform.position.y, transform.position.z);
    }

    void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
        }
    }

    void MoveRight()
    {
        if (currentLane < lanes.Length - 1)
        {
            currentLane++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cars"))
        {
            Debug.Log("Collision detected with another car!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart 
        }
    }

    private bool IsPointerOverUIElement()
    {
        // Return true if pointing over a UI element
        return EventSystem.current.IsPointerOverGameObject();
    }
}
