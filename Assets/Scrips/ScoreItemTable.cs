using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreItemTable : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _txtPos;
    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private TextMeshProUGUI _txtName;

    public void _SetText(string txtPos, string txtScore, string txtName)
    {
        _txtPos.text = txtPos;
        _txtScore.text = txtScore;
        _txtName.text = txtName;
    }

    public void _GetText(string txtPos, string txtScore, string txtName)
    {
        txtPos = _txtPos.text;
        txtScore = _txtScore.text ;
        txtName = _txtName.text;
    }
}
