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
    public GameObject loadingScreen;
    public Text versionText;
    private bool isSettingsMenuActive;
    private bool isQuitMenuActive;
    private int vibrate;
    public Text availableDiamondsText;
    public AudioSource openSFX;
    public AudioSource closeSFX;

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

        versionText.text = "v " + Application.version;

        availableDiamondsText.text = PlayerPrefs.GetInt("Diamonds", 50).ToString();
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
        openSFX.Play();
        isSettingsMenuActive = true;
        isQuitMenuActive = false;
    }

    public void OpenQuit()
    {
        exitScreen.SetActive(true);
        openSFX.Play();
        isSettingsMenuActive = false;
        isQuitMenuActive = true;
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
        closeSFX.Play();
        isSettingsMenuActive = false;
        isQuitMenuActive = false;
    }

    public void CloseQuit()
    {
        exitScreen.SetActive(false);
        closeSFX.Play();
        isSettingsMenuActive = false;
        isQuitMenuActive = false;
    }

    public void PlayGame()
    {
        //ads.HideBannerAd();
        openSFX.Play();
        loadingScreen.SetActive(true);
        Invoke("LoadGameplay", 0.5f);
    }

    private void LoadGameplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("Diamonds");
        availableDiamondsText.text = PlayerPrefs.GetInt("Diamonds", 50).ToString();
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
