using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform[] lanes; // Pozycje X dla pasów ruchu
    private int currentLane = 1; // Startowy pas (œrodkowy)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        // Dodanie obs³ugi klikniêcia myszy dla testowania w edytorze
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Restart 
        }
    }
}
