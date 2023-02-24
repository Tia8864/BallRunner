using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTable : MonoBehaviour
{
    //reference: https://youtu.be/iAbaqGYdnyI
    [SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private ScoreItemTable _itemSpawn;

    /*private List<HighscoreEntry> highscoreEntryList;*/
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
        PlayerPrefs.SetString("highScoreTable", new HighscoreEntry(100, "drres").ToString());

        Debug.LogError(new HighscoreEntry(100, "drres").ToString());
        //take json data form playerPrefs
        string jsonString = PlayerPrefs.GetString("highScoreTable");
        Debug.LogError(jsonString);
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        //Sort entry list by score
        for (int i = 0; i < highScores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores.highscoreEntryList.Count; j++)
            {
                if (highScores.highscoreEntryList[j].score < highScores.highscoreEntryList[i].score)
                {
                    HighscoreEntry temp = highScores.highscoreEntryList[j];
                    highScores.highscoreEntryList[j] = highScores.highscoreEntryList[i];
                    highScores.highscoreEntryList[i] = temp;
                }
            }
        }

        //linq

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

    private void _CreateHighScoreEntryTranForm(
        HighscoreEntry highscoreEntry, 
        Transform container, 
        List<Transform> transformList, 
        int i)
    {
        float templateHeight = 35f;
        ScoreItemTable itemFill = Instantiate(_itemSpawn, container);
        RectTransform entryRectTransform = container.GetComponent<RectTransform>();

        entryRectTransform.anchoredPosition3D = new Vector3(0, -templateHeight * i - 70f, 0);
        Debug.Log( -templateHeight * i -70f);
        itemFill.gameObject.SetActive(true);

        int rank = i + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }

        
       itemFill._SetText("1", "2000", "sdfadf");
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
