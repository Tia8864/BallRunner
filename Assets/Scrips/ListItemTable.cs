using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemScore{
    public int score;
    public string name;

    public ItemScore()
    {
    }

    public ItemScore(int score, string name)
    {

        this.score = score;
        this.name = name;
    }

    public override string ToString()
    {
        return $"{score: 00000}, {name: 10}";
    }
}

