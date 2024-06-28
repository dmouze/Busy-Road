// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class Pause : MonoBehaviour
// {
//     public GameObject pausePanel; // Ensure this is assigned in the Inspector
//     public Button pauseButton;
//     public Button returnButton; // Button to return to the game

//     // Start is called before the first frame update
//     void Start()
//     {
//         if (pauseButton != null)
//         {
//             pauseButton.onClick.AddListener(PauseGame);
//         }
//         else
//         {
//             Debug.LogError("Pause button is not assigned in the Inspector");
//         }

//         if (returnButton != null)
//         {
//             returnButton.onClick.AddListener(ReturnToGame);
//         }
//         else
//         {
//             Debug.LogError("Return button is not assigned in the Inspector");
//         }

//         // Ensure the pause panel is hidden at the start
//         if (pausePanel != null)
//         {
//             pausePanel.SetActive(false);
//         }
//         else
//         {
//             Debug.LogError("Pause panel is not assigned in the Inspector");
//         }
//     }

//     // Method to be called when the pause button is pressed
//     public void PauseGame()
//     {
//         if (pausePanel != null)
//         {
//             pausePanel.SetActive(true);
//         }
//         else
//         {
//             Debug.LogError("Pause panel is not assigned in the Inspector");
//         }

//         // Pausing the game
//         Time.timeScale = 0f;
//         PlayerController.isPaused = true;
//         Debug.Log("Game Paused");
//     }

//     // Method to be called when the return button is pressed
//     public void ReturnToGame()
//     {
//         if (pausePanel != null)
//         {
//             pausePanel.SetActive(false);
//         }
//         else
//         {
//             Debug.LogError("Pause panel is not assigned in the Inspector");
//         }

//         // Resuming the game
//         Time.timeScale = 1f;
//         PlayerController.isPaused = false;
//         Debug.Log("Game Resumed");
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel; // Ensure this is assigned in the Inspector
    public Button pauseButton;
    public Button returnButton; // Button to return to the game

    private PlayerController playerController; // Reference to PlayerController instance

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find PlayerController instance

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseGame);
        }
        else
        {
            Debug.LogError("Pause button is not assigned in the Inspector");
        }

        if (returnButton != null)
        {
            returnButton.onClick.AddListener(ReturnToGame);
        }
        else
        {
            Debug.LogError("Return button is not assigned in the Inspector");
        }

        // Ensure the pause panel is hidden at the start
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause panel is not assigned in the Inspector");
        }
    }

    // Method to be called when the pause button is pressed
    public void PauseGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Pause panel is not assigned in the Inspector");
        }

        // Pausing the game
        Time.timeScale = 0f;
        if (playerController != null)
        {
            playerController.isPaused = true; // Set isPaused on the instance
        }
        Debug.Log("Game Paused");
    }

    // Method to be called when the return button is pressed
    public void ReturnToGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause panel is not assigned in the Inspector");
        }

        // Resuming the game
        Time.timeScale = 1f;
        if (playerController != null)
        {
            playerController.isPaused = false; // Set isPaused on the instance
        }
        Debug.Log("Game Resumed");
    }
}
