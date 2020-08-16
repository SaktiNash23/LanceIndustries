using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class InGamePauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public Button inGameOptionsButton;

    [BoxGroup("PAUSE: Reflector Type & Color Buttons")]
    public List<Button> reflectorButtons = new List<Button>();

    private void Awake()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void PauseGame()
    {
        //In this script, disable reflector selection menus. Disabling other gameplay inputs such as reflectors, starting points, etc..
        //must be done in their respective scripts.

        //Disable interaction with the reflector buttons (both type and color buttons)
        foreach (Button reflectorButton in reflectorButtons)
        {
            reflectorButton.GetComponent<Image>().raycastTarget = false;
        }

        pauseMenuPanel.SetActive(true);
        inGameOptionsButton.GetComponent<Image>().raycastTarget = false;
        GameManager.gameManagerInstance.gameIsPaused = true;
        Time.timeScale = 0.0f; //Stops in-game time
    
    }

    public void ResumeGame()
    {
        //Enable interaction with the reflector buttons (both type and color buttons)
        foreach (Button reflectorButton in reflectorButtons)
        {
            reflectorButton.GetComponent<Image>().raycastTarget = true;
        }

        pauseMenuPanel.SetActive(false);
        inGameOptionsButton.GetComponent<Image>().raycastTarget = true;
        GameManager.gameManagerInstance.gameIsPaused = false;
        Time.timeScale = 1.0f; //Resumes in-game time
    }
}
