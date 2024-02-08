using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public Text highScore;
    public GameObject gameOverScreen;
    //public AudioSource pillarHitSFX;
    public GameObject gameOverSFX;
    public AudioSource BGM;
    public bool isGameOver = false;
    public GameObject tapToStart;
    public BirdScript birdScript;
    public GameObject deathEffect;
    //public AdsInitializer ads;

    private void Start()
    {
        Time.timeScale = 0f;
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        birdScript.TapJump();
        tapToStart.SetActive(false);
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();

        if(playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            highScore.text = playerScore.ToString();
        }
        
        //pillarHitSFX.Play();
    }

    //public void ResetHighScore()
    //{
    //    PlayerPrefs.DeleteKey("HighScore");
    //}

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void gameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        BGM.Stop();
        gameOverSFX.SetActive(true);
        Time.timeScale = 0.3f;
        Vibrate();
        deathEffect.SetActive(true);
        //ads.LoadInterstitialAd();
    }

    private void Vibrate()
    {
        if(PlayerPrefs.GetInt("vibrate", 1) == 1)
        {
            Vibrator.Vibrate(300);
        }
    }

}
