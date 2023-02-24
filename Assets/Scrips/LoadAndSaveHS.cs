using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LoadAndSaveHS : MonoBehaviour
{
    public static LoadAndSaveHS _Instance;

    [Header("ContainerBox and highscore")]
    [SerializeField] private Transform _container;
    [SerializeField] private ScoreItemTable _item;

    [Header("Input new highscore")]
    [SerializeField] private TMP_InputField _txtInputName;
    [SerializeField] private TextMeshProUGUI _txtDetail;

    private const string NAMEDATA = "highScore";
    private ItemScore _itemScore = new ItemScore();
    public List<ItemScore> _listItem = new List<ItemScore>();

    private List<ScoreItemTable> _listScoreItemTables = new List<ScoreItemTable>();

    private void Awake()
    {
        if (_Instance == null) _Instance = this;
        else Destroy(this.gameObject);
        _Init();
    }

    public void _DataTest()
    {
        _listItem.Add(new ItemScore(100, "Tung"));
        _listItem.Add(new ItemScore(2230, "tung2"));
        _listItem.Add(new ItemScore(120, "someone"));
        _listItem.Add(new ItemScore(20, "HDT"));
        _listItem.Add(new ItemScore(5, "123456789"));
        _listItem.Add(new ItemScore(30, "hrreee"));
        _listItem.Add(new ItemScore(35, "Midles"));
        _listItem.Add(new ItemScore(80, "fghgggg"));
        _listItem.Add(new ItemScore(1500, "rrrrrr"));
    }

    private void _Init()
    {
        _DataTest();
        //foreach (var product in ketqua) Console.WriteLine($"{product.Name} - {product.Price}");
        //-----------------------

        if (PlayerPrefs.GetString(NAMEDATA).Equals(null))
        {
            _listItem.Add(_itemScore);
            PlayerPrefs.SetString(NAMEDATA, JsonUtility.ToJson(_listItem));
        }
        //linq
        else
        {
            //json => object
            string json = PlayerPrefs.GetString(NAMEDATA);
            //ListItemTable = JsonUtility.FromJson<ListItemTable>(json);
        }
        Debug.Log(PlayerPrefs.GetString(NAMEDATA));
        _AddDataToTable(_container, _item, _listItem, 5);
    }

    public void _AddDataToListNToPlayerPfebs()
    {
        if (_CheckInput())
        {
            _itemScore = new ItemScore(GameController._Instance.score, _txtInputName.text);
            _listItem.Add(_itemScore);
            string json = JsonUtility.ToJson(_listItem);
            PlayerPrefs.SetString(NAMEDATA, json);
            Debug.Log(json.ToString());
            /*Destroy(gameObject.tag("clone");*/
            for (int i = 0; i < _listScoreItemTables.Count; i++)
            {
                Destroy(_listScoreItemTables[i].gameObject);
            }
            _listScoreItemTables.Clear();
            _AddDataToTable(_container, _item, _listItem, 5);
            UIController._Instance.animator.SetBool("isOpen", false);
        }
        else
        {
            _txtDetail.text = "Limit under 10 charater!";
            ///stop in here
            UIController._Instance.animator.SetTrigger("visible");
            UIController._Instance.animator.SetBool("isOpen", true);
        }
    }

    private void _AddDataToTable(Transform container, ScoreItemTable item, List<ItemScore> listItem, int n)
    {
        int rank = 1;
        string rankString; 
        //-------test with linq--------
        var ketqua = from ItemScore in listItem
                     orderby ItemScore.score descending //sort item by score
                     select ItemScore;
        foreach (var item1 in ketqua.Take(n))
        {
            switch (rank)
            {
                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
                default: rankString = rank + "TH"; break;
            }
            item._SetText(rankString, item1.score+"", item1.name);
            ScoreItemTable scoreItemTable = Instantiate(item, container);
            _listScoreItemTables.Add(scoreItemTable);
            rank++;
        }
    }

    private bool _CheckInput()
    {
        return (_txtInputName.text.Length <= 10) && (_txtInputName.text.Length > 0) ? true : false;
    }
}
