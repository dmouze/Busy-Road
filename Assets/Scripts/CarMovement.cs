using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float speed;
    public bool moveDown; // True, jeśli samochód ma zjeżdżać w dół, false jeśli ma jechać do góry
    private CalculatePoints calculatePoints;
    private bool enteredFromStartPosition = false; // Flaga do sprawdzenia, czy samochód wjechał na ekran
    private bool pointsAdded = false; // Flaga do sprawdzenia, czy punkty zostały już dodane

    private GameObject player;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Start()
    {
        Debug.Log("Car initialized at position: " + transform.position + " with moveDown: " + moveDown);
        player = GameObject.FindGameObjectWithTag("Player1");

        // Inicjalizacja calculatePoints
        calculatePoints = GameObject.FindObjectOfType<CalculatePoints>();
        if (calculatePoints == null)
        {
            Debug.LogError("CalculatePoints component not found in the scene.");
        }
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

        // Sprawdź, czy samochód wjechał na ekran
        if (!enteredFromStartPosition)
        {
            if ((moveDown && transform.position.y < 10f) || (!moveDown && transform.position.y > -10f))
            {
                enteredFromStartPosition = true;
            }
        }

        // Dodaj punkty, gdy obiekt w pełni przejedzie z jednej strony ekranu na drugą
        if (enteredFromStartPosition && !pointsAdded && player != null)
        {
            if ((moveDown && transform.position.y < -1f) || (!moveDown && transform.position.y > 1f))
            {
                if (gameObject.CompareTag("Cars"))
                {
                    // Dodaj punkt za omijanie
                    if (calculatePoints != null)
                    {
                        calculatePoints.points++;
                        calculatePoints.AddPoints();
                        pointsAdded = true; // Ustaw flagę na true, aby punkty były dodane tylko raz
                        Debug.Log("Points added for avoiding car.");
                    }
                    else
                    {
                        Debug.LogError("CalculatePoints is null, cannot add points");
                    }
                }
            }
        }

        // Usuń samochód, gdy wyjdzie poza ekran
        if (transform.position.y < -10f || transform.position.y > 10f)
        {
            Debug.Log("Destroying car at position: " + transform.position);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1")
        {
            Destroy(player.gameObject);
            player = null; // Ustawienie player na null, aby zatrzymać dodawanie punktów
        }
    }
}
