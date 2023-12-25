using UnityEngine;

public class CursorLock : MonoBehaviour
{
    void Start()
    {
        // Hide cursor when locking
        Cursor.visible = false;

        // Lock the cursor in the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Press F2 to unlock and show the cursor
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Press F1 to re-lock and hide the cursor
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Press ESC to quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                // If running in the Unity Editor
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                // If running in a build version
                Application.Quit();
            #endif
        }
    }
}
