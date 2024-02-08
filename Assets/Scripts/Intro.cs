using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float startScreenCountDown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", startScreenCountDown);
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
