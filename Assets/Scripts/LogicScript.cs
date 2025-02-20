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
    public AudioSource diamondCollectSFX;
    public bool isGameOver = false;
    public GameObject tapToStart;
    public BirdScript birdScript;
    public GameObject deathEffect;
    // public GameObject heartMenu;
    public int diamonds;
    public float diamondSpawnRate = 45;
    private float diamondTimer = 0;
    public GameObject diamondEffect;
    public Text diamondText;
    private bool forceGameOver = false;
    public GameObject reviveMenu;
    public Text diamondsToSpendText;
    public Text availableDiamondsText;
    private int diamondsToDeduct = 5;
    private int increaseDiamondsBy = 5;
    public Button reviveButton;
    public Color redColor;
    private int usedRevives = 0;
    public AdmobAdsManager adsManager;
    public GameObject watchAdBtn;
    public GameObject adNotReadyText;

    private void Start()
    {
        Time.timeScale = 0f;
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        diamonds = PlayerPrefs.GetInt("Diamonds", 50);
        Invoke("LoadAds", 4f);
    }

    private void LoadAds()
    {
        adsManager.LoadRewardedAd();
    }

    /*
    [ContextMenu("Delete Diamonds")]
    public void DeleteDiamonds()
    {
        PlayerPrefs.DeleteKey("Diamonds");
        Debug.Log("Diamonds Deleted");
    }
    */
    

    private void Update()
    {
        if (isGameOver == false)
        {
            if (diamondTimer < diamondSpawnRate)
            {
                diamondTimer = diamondTimer + Time.deltaTime;
            }
            else
            {
                AddDiamonds(increaseDiamondsBy);
                increaseDiamondsBy += 5;
                diamondTimer = 0;
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        birdScript.TapJump();
        tapToStart.SetActive(false);
    }

    // [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();

        if(playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            highScore.text = playerScore.ToString();
        }
    }

    public void AddDiamonds(int diamondsToAdd)
    {
        diamonds = diamonds + diamondsToAdd;
        PlayerPrefs.SetInt("Diamonds", diamonds);
        diamondText.text = "+ " + diamondsToAdd.ToString();
        diamondCollectSFX.Play();
        diamondEffect.SetActive(true);
        Invoke("DisableDiamondEffect", 5f);
    }

    private void DisableDiamondEffect()
    {
        diamondEffect.SetActive(false);
    }

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
        if (birdScript.hasHearts)
        {
            birdScript.UseHeart();
            // heartMenu.SetActive(true);
            // Time.timeScale = 0f;
        }
        else if(usedRevives < 4 && forceGameOver == false)
        {
            ShowReviveMenu();
        }
        else
        {
            isGameOver = true;
            gameOverScreen.SetActive(true);
            BGM.Stop();
            gameOverSFX.SetActive(true);
            Time.timeScale = 0.3f;
            Vibrate();
            deathEffect.SetActive(true);
        }
    }

    private void ShowReviveMenu()
    {
        // adsManager.LoadRewardedAd();
        reviveMenu.SetActive(true);
        availableDiamondsText.text = diamonds.ToString();
        diamondsToSpendText.text = "- " + diamondsToDeduct;

        if (diamonds - diamondsToDeduct < 0)
        {
            diamondsToSpendText.color = redColor;
            // reviveButton.interactable = false;
        }

        Time.timeScale = 0f;
    }

    public void CloseReviveMenu()
    {
        reviveMenu.SetActive(false);
        Time.timeScale = 1f;
        forceGameOver = true;
        gameOver();
    }

    public void UseRevive()
    {
        // adsManager.ShowRewardedAd();

        if(diamonds - diamondsToDeduct >= 0)
        {
            birdScript.intangible = true;
            reviveMenu.SetActive(false);
            Time.timeScale = 1f;
            birdScript.transform.position = new Vector3(0, 0, 0);
            birdScript.myRigidBody.linearVelocity = new Vector2(0, 0);
            birdScript.heartLostEffect.SetActive(true);
            diamonds = diamonds - diamondsToDeduct;
            PlayerPrefs.SetInt("Diamonds", diamonds);
            diamondsToDeduct += 5;
            usedRevives += 1;

            if(adsManager.adsLeft > 0 && adsManager.isWatchBtnEnabled == false)
            {
                adsManager.LoadRewardedAd();
            }

            Invoke("DisableIntangiblity", 1.5f);
        }
        
    }

    public void ShowAd()
    {
        adsManager.ShowRewardedAd();
    }

    public void UseAdsRevive()
    {
        birdScript.intangible = true;
        watchAdBtn.SetActive(false);
        reviveMenu.SetActive(false);
        birdScript.transform.position = new Vector3(0, 0, 0);
        birdScript.myRigidBody.linearVelocity = new Vector2(0, 0);
        Time.timeScale = 0;
        tapToStart.SetActive(true);
        adsManager.LoadRewardedAd();
        Invoke("DisableIntangiblity", 1.5f);
    }

    private void DisableIntangiblity()
    {
        if (birdScript.isShieldInUse == false)
        {
            birdScript.intangible = false;
            birdScript.heartLostEffect.SetActive(false);
        }
    }

    // [ContextMenu("Call AdNotAvailable")]
    public void AdNotAvailable()
    {
        adNotReadyText.SetActive(true);
        Invoke("DestroyAdNotAvailable", 1f);
    }

    private void DestroyAdNotAvailable()
    {
        adNotReadyText.SetActive(false);
    }

    private void Vibrate()
    {
        if(PlayerPrefs.GetInt("vibrate", 1) == 1)
        {
            Vibrator.Vibrate(300);
        }
    }

}
