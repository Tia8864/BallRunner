using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController _Instance;
    [Header("Ball Setting")]
    public float speed, forceJump;
    public int startingTime;
    private int currentTime = 0;

    [Header("SpeedUp")]
    public float multipleSpeed;
    public float speedUpTime;


    [Header("UI Setting")]
    public bool pause = false;
    [SerializeField] private TextMeshProUGUI txtTimer, txtEndGame, txtScore, txtScore2;
    public int score = 0;


    
    private void Awake()
    {
        if(_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void _Being(int timedown)
    {
        currentTime = timedown;
        StartCoroutine(_UpdateTimer());
    }

    private IEnumerator _UpdateTimer()
    {
        string textTime;
        score = startingTime * 10;
        while(currentTime >= 0)
        {
            if (!pause)
            {
                textTime = $"{currentTime / 60:00}:{currentTime % 60:00}";
                txtTimer.text = "Time: " + textTime;
                currentTime--;
                yield return new WaitForSeconds(1f);
            }
            else
                yield return null;
        }
    }
    //reference : https://www.youtube.com/watch?v=2gPHkaPGbpI

    public void _ReSetTime()
    {
         _Being(startingTime);
    }
    private void Update()
    {
        if (PlayerController._Instance.flagWin)
        {
            UIController._Instance.isEnd = true;
            txtEndGame.text = "You win";
            txtScore.text = "Your score: " + score;
            txtScore2.text = "Your score: " + score;
            /*Time.timeScale = 0;*/
            /*PlayerController._Instance.flagWin = false;*/
        }

        if (PlayerController._Instance.flagDeath || currentTime < 0)
        {
            UIController._Instance.isEnd = true;
            txtEndGame.text = "Game Over";
            txtScore.text = "Your score: " + score;
            /*Time.timeScale = 0;*/
/*            PlayerController._Instance.flagDeath = false;*/
        }

        if (PlayerController._Instance.flagSpeedUP)
        {
            float temp = multipleSpeed * speed;
            PlayerController._Instance.speed = temp;
            StartCoroutine(_SpeedDown(multipleSpeed));
            /*PlayerController._Instance.flagSpeedUP = false;*/
        }

        if(!PlayerController._Instance.flagDeath && !PlayerController._Instance.flagWin && currentTime >= 0)
        {
            score = currentTime * 10; 
        }
    }
    private IEnumerator _SpeedDown(float multipleSpeed)
    {
        PlayerController._Instance.speed = speed;
        yield return new WaitForSeconds(speedUpTime);
    }

}