using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerLife : MonoBehaviour
{

    public GameObject textDisplay;
    public int secondsLeft = 30;
    public bool takingAway = false;

    public float transitionTime = 1f;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00" + secondsLeft;
    }

    void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;

        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }   

        takingAway = false;

        if (secondsLeft == 0)
        {
            //ir pra proxima tela
            LoadNextLevel();
        }
    }


    // TRANSIÇÃO TELA
    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
