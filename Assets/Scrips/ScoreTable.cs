using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTable : MonoBehaviour
{
    //reference: https://youtu.be/iAbaqGYdnyI
    [SerializeField]
    private Transform entryContainer, entryTemplate;

    /*private List<HighscoreEntry> highscoreEntryList;*/
    private List<Transform> highscoreEntryTransformList;

    private TextMeshProUGUI txtPos, txtScore, txtName;
    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        //data testing
        /*highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{score = 3123243, name = "first name"},
            new HighscoreEntry{score = 4342, name = "2sd name"},
            new HighscoreEntry{score = 423, name = "3rd name"},
            new HighscoreEntry{score = 423, name = "4th name"},
            new HighscoreEntry{score = 234324, name = "5th name"},
            new HighscoreEntry{score = 242, name = "6th name"},
            new HighscoreEntry{score = 34243, name = "7th name"}
        };*/

        AddHighScoreEntry(1000, "Tung");
        AddHighScoreEntry(2131, "qer");
        AddHighScoreEntry(1341413, "yrtwt");
        AddHighScoreEntry(122, "qedf");

        //take json data form playerPrefs
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        //Sort entry list by score
        for (int i = 0; i < highScores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores.highscoreEntryList.Count; j++)
            {
                if (highScores.highscoreEntryList[j].score > highScores.highscoreEntryList[i].score)
                {
                    HighscoreEntry temp = highScores.highscoreEntryList[j];
                    highScores.highscoreEntryList[j] = highScores.highscoreEntryList[i];
                    highScores.highscoreEntryList[i] = temp;
                }
            }
        }

        //pick data form playerpfes to highscore table
        highscoreEntryTransformList = new List<Transform>();

        /*foreach (HighscoreEntry highscoreEntry1 in highScores.highscoreEntryList)
        {
            _CreateHighScoreEntryTranForm(highscoreEntry1, entryContainer, highscoreEntryTransformList);
        }
        */
        HighscoreEntry highscoreEntry = new HighscoreEntry();
        for (int i = 0; i < highScores.highscoreEntryList.Count; i++)
        {
            _CreateHighScoreEntryTranForm(highscoreEntry, entryContainer, highscoreEntryTransformList, i);
            highscoreEntry = new HighscoreEntry();
            if (i > 5) break;
        }

    }


    private void _CreateHighScoreEntryTranForm(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList, int i)
    {
        float templateHeight = 35f;
        Transform entryTranform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = container.GetComponent<RectTransform>();

        entryRectTransform.anchoredPosition3D = new Vector3(0, -templateHeight * i, 0);
        Debug.Log(- templateHeight * i);
        entryTranform.gameObject.SetActive(true);

        int rank = i + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }

        txtPos = entryTranform.Find("Pos").GetComponent<TextMeshProUGUI>();
        txtPos.text = rankString;


        int score = /*Random.Range(0, 1000);*/ highscoreEntry.score;
        txtScore = entryTranform.Find("Score").GetComponent<TextMeshProUGUI>();
        txtScore.text = score + "";

        string name = /*"name " + transformList.Count + 1;*/ highscoreEntry.name;
        txtName = entryTranform.Find("Name").GetComponent<TextMeshProUGUI>();
        txtName.text = name;

        transformList.Add(entryTranform);

        txtName.color = new Color(31, 31, 31);
        txtPos.color = new Color(31, 31, 31);
        txtScore.color = new Color(31, 31, 31);
        entryTranform.Find("backgroundScore").gameObject.SetActive(false);
        if (rank % 2 == 1)   // set color and background one row in table
        {
            entryTranform.Find("backgroundScore").gameObject.SetActive(true);
            txtName.color = Color.white;
            txtPos.color = Color.white;
            txtScore.color = Color.white;
        }
    }

    private void AddHighScoreEntry(int _score, string _name)
    {
        //PlayerPrefs.Get
        //create highscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry(_score, _name );
        //Load saved Highscore
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores;
        //Debug.LogError(jsonString.ToString());
        if (string.IsNullOrEmpty(jsonString))
        {
            highScores = new HighScores();
        }
        else
        {
            highScores = JsonUtility.FromJson<HighScores>(jsonString);
        }
        //Add new entry to highscores
        
        highScores.highscoreEntryList.Add(highscoreEntry);

        //sava data into json by playerprefs
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
        /*Debug.Log(PlayerPrefs.GetString("highScoreTable"));*/
    }

    [System.Serializable]
    private class HighScores
    {
        public List<HighscoreEntry> highscoreEntryList = new List<HighscoreEntry>();

        public HighScores()
        {
            highscoreEntryList = new List<HighscoreEntry>();
        }
        public HighScores(List<HighscoreEntry> highscoreEntryList)
        {
            this.highscoreEntryList = highscoreEntryList;
        }
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;

        public HighscoreEntry(int score, string name)
        {
            this.score = score;
            this.name = name;
        }

        public HighscoreEntry() { }
    }






}
