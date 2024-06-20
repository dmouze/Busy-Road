using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float speed;
    public bool moveDown; // True, jeśli samochód ma zjeżdżać w dół, false jeśli ma jechać do góry

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Start()
    {
        Debug.Log("Car initialized at position: " + transform.position + " with moveDown: " + moveDown);
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
        if ((moveDown && transform.position.y < -Screen.height / 2) ||
            (!moveDown && transform.position.y > Screen.height / 2))
        {
            Debug.Log("Destroying car at position: " + transform.position);
            Destroy(gameObject);
        }
    }
}