using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float speed;
    public bool moveDown; // True, jeśli samochód ma zjeżdżać w dół, false jeśli ma jechać do góry

    private GameObject player;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Start()
    {
        Debug.Log("Car initialized at position: " + transform.position + " with moveDown: " + moveDown);
        player = GameObject.FindGameObjectWithTag("Player1");
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
            Debug.Log("Destroying car at position: " + transform.position);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1")
        {
            Destroy(player.gameObject);
        }
    }
}
