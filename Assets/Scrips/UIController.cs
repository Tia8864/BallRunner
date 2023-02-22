using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController _Instance;
    [SerializeField] GameObject startUI, inGameUI, pauseUI, endUI, highScoreUI;
    public bool isStart, isInGame, isPause, isEnd, isHighScore;

    private void Awake()
    {
        if (_Instance == null) _Instance = this;
        else Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        isStart = true;
        isInGame = false;
        isEnd = false;
        isPause = false;
        isHighScore = false;
        _Show();
    }
    private void _Show()
    {
        startUI.SetActive(isStart);
        inGameUI.SetActive(isInGame);
        pauseUI.SetActive(isPause);
        endUI.SetActive(isEnd);
        highScoreUI.SetActive(isHighScore);
    }

    //startUI
    public void _btnStart()
    {
        isStart = false;
        isInGame = true;
        isEnd = false;
        _Show();
    }
    public void _btnHghScore()
    {
        isStart = false;
        isHighScore = true;
        isEnd = false;
        _Show();
    }
    public void _btnExit()
    {
        //exit apllication
    }
    //inGameUI
    public void _btnPause()
    {
        GameController._Instance.pause = true;
        isInGame = false;
        isPause = true;
        Time.timeScale = 0;
        _Show();
    }
    //endUI & highScoreUI
    public void _btnOk()
    {
        isHighScore = false;
        isStart = true;
        _Show();
    }

    //pauseUI
    public void _btnResume()
    {
        GameController._Instance.pause = false;
        isPause = false;
        isInGame = true;
        Time.timeScale = 1;
        _Show();
    }
    public void _btnQuit()
    {
        isPause = false;
        isStart = true;
        Time.timeScale = 1;
        _Show();
    }

    
}
