using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class UIController : MonoBehaviour
{
    public static UIController _Instance;
    [SerializeField] GameObject startUI, inGameUI, pauseUI, endUI, highScoreUI;
    public bool isStart, isInGame, isPause, isEnd, isHighScore;
    public Animator animator;
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

    private void Update()
    {
        if (PlayerController._Instance.flagDeath)
        {
            isInGame = false;
            isEnd = true;
            isHighScore = true;
            _Show();
            PlayerController._Instance.flagDeath = false;
        }

        if (PlayerController._Instance.flagWin)
        {
            isInGame = false;
            isEnd = true;
            isHighScore = true;
            Time.timeScale = 0;
            _Show();
            _ShowPopUp();
            PlayerController._Instance.flagWin = false;
            //so sanh diem o day
        }

        //resolver show popup
        //1st get 
    }
    private void _ShowPopUp()
    {
        var ketqua = from ItemScore in LoadAndSaveHS._Instance._listItem
                     orderby ItemScore.score descending //sort item by score
                     select ItemScore;

        
        if (GameController._Instance.score > ketqua.Last().score)
        {
            animator.SetTrigger("visible");
            animator.SetBool("isOpen", true);
        }
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
        Time.timeScale = 1;
        GameController._Instance._ReSetTime();
        PlayerController._Instance._ResetGame();
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
        Application.Quit();
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
        GameController._Instance.startingTime = 120;

        _Show();
    }

   //de show popup newName can so sanh diem giua
   //LisItem.itemScoreList[0..5] ? nesu dungs thif show ko thif hide

}
