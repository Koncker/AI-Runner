using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMScript : MonoBehaviour {

    public KeyCode reset;   // Game KeyCode to RESET -- Reset   -- SET TO ESCAPE
    public KeyCode quit;    // Game KeyCode to QUIT  -- Quit    -- SET TO Q
    public static Scene scene;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(reset))    // Input from Player to reset
        {
            resetGame();
        }

        if (Input.GetKeyDown(quit))
        {
            Debug.Log("Quitting Game"); // Test purposes to quit
            Application.Quit();         // Quits the application
        }

    }

    public static void resetGame()
    {
        Debug.Log("resetting scene: " + GMScript.scene);    // Test purposes to see loading scene
        playerController.forwVel = 7.0f;                    // Resets the forward velocity back to 7.0f
        SceneManager.LoadScene("startingLevel");            // Restarts the game to startingLevel (default scene)
    }
}
