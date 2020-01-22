using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public KeyCode pauseKey = KeyCode.Escape;
    [SerializeField]
    private GameObject pauseMenuUI;

    private Player player;
    // Update is called once per frame

    private void Start()
    {
        player = SetPlayer.player;
    }
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false; 
    }

    public void LoadMenu()
    {
        //SceneManager.LoadScene("Menu" (or some variable));
        //Create SceneManager at some point
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

   
}
