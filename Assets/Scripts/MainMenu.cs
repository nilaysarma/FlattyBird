using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //public AdsInitializer ads;
    public GameObject resetedScreen;
    public GameObject exitScreen;
    public GameObject settingsScreen;
    public GameObject vibrateToggler;
    private bool isSettingsMenuActive;
    private bool isQuitMenuActive;
    private int vibrate;

    private void Start()
    {
        Time.timeScale = 1f;
        //ads.LoadBannerAd();

        isSettingsMenuActive = false;
        isQuitMenuActive = false;

        vibrate = PlayerPrefs.GetInt("vibrate", 1);

        if (vibrate == 1)
        {
            vibrateToggler.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            vibrateToggler.GetComponent<Toggle>().isOn = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isQuitMenuActive)
            {
                CloseQuit();
            }

            else if (isSettingsMenuActive)
            {
                CloseSettings();
            }

            else
            {
                OpenQuit();
            }
        }
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
        isSettingsMenuActive = true;
        isQuitMenuActive = false;
    }

    public void OpenQuit()
    {
        exitScreen.SetActive(true);
        isSettingsMenuActive = false;
        isQuitMenuActive = true;
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
        isSettingsMenuActive = false;
        isQuitMenuActive = false;
    }

    public void CloseQuit()
    {
        exitScreen.SetActive(false);
        isSettingsMenuActive = false;
        isQuitMenuActive = false;
    }

    public void PlayGame()
    {
        //ads.HideBannerAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        resetedScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited");
    }

    public void PrivacyPolicyURL()
    {
        Application.OpenURL("https://nrgames.github.io/privacy-policy.html");
    }

    public void MuteToggle(bool muted)
    {
        if(muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void VibrateToggle(bool vibrated)
    {
        if(vibrated)
        {
            PlayerPrefs.SetInt("vibrate", 1);
        }
        else
        {
            PlayerPrefs.SetInt("vibrate", 0);
        }
    }
}
