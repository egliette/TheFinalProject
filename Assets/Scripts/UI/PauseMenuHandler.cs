using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    GameObject pauseScreenCanvas, settingsScreenCanvas;
    GameObject pauseButtonCanvas;
    private void Start()
    {
        pauseScreenCanvas = gameObject.transform.Find("PauseScreenCanvas").gameObject;
        pauseButtonCanvas = gameObject.transform.Find("PauseButtonCanvas").gameObject;
        settingsScreenCanvas = gameObject.transform.Find("SettingsScreenCanvas").gameObject;
        pauseScreenCanvas.SetActive(false);
    }
    public void OnPauseButtonClick()
    {
        Time.timeScale = 0f;
        pauseScreenCanvas.SetActive(true);
        pauseButtonCanvas.SetActive(false);
    }
    public void OnResumeButtonClick()
    {
        Time.timeScale = 1f;
        pauseScreenCanvas.SetActive(false);
        pauseButtonCanvas.SetActive(true);
    }
    public void OnBackToMainButtonClick()
    {
        Time.timeScale = 1f;
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }    
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
    public void OnSettingsButtonClick()
    {

        pauseScreenCanvas.SetActive(false);
        settingsScreenCanvas.SetActive(true);

    }    
}
