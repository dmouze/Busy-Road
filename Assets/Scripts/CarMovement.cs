using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour
{
    private float speed;
    public bool moveDown; // True, jeśli samochód ma zjeżdżać w dół, false jeśli ma jechać do góry
    private CalculatePoints calculatePoints;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Start()
    {
        Debug.Log("Car initialized at position: " + transform.position + " with moveDown: " + moveDown);
        calculatePoints = GameObject.FindObjectOfType<CalculatePoints>();
    }

    void Update()
    {
        if (moveDown)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        // Usuń samochód, gdy wyjdzie poza ekran
        if (transform.position.y < -10f || transform.position.y > 10f)
        {
            if (gameObject.CompareTag("Cars"))
            {
                // Dodaj punkt za omijanie
                if (calculatePoints != null)
                {
                    calculatePoints.points++;
                    calculatePoints.AddPoints();
                }
            }
            Debug.Log("Destroying car at position: " + transform.position);
            Destroy(gameObject);

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            Debug.Log("Collision detected with player!");
            //ToDo: Dodaj tutaj logikę obsługi kolizji, np. kończenie gry
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart sceny
        }
    }
}