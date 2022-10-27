using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    #region StarPoint
    private int _starPoint;
    public int StarPoint
    {
        get
        {
            return _starPoint;
        }
        set
        {
            _starPoint = value;
            _starPointText.text = _starPoint.ToString();
        }
    }

    [SerializeField] private TMP_Text _starPointText;
    #endregion

    #region Normal Dice Num
    private int _normalDiceNum;
    public int NormalDiceNum
    {
        get
        {
            return _normalDiceNum;
        }

        set
        {
            _normalDiceNum = value;
            _normalDiceNumText.text = _normalDiceNum.ToString();
        }
    }
    [SerializeField] private TMP_Text _normalDiceNumText;
    #endregion

    #region Golden Dice Num
    private int _goldenDiceNum;
    public int GoldenDiceNum
    {
        get
        {
            return _goldenDiceNum;
        }

        set
        {
            _goldenDiceNum = value;
            _goldenDiceNumText.text = _goldenDiceNum.ToString();
        }
    }
    [SerializeField] private TMP_Text _goldenDiceNumText;
    #endregion

    #region Direction
    // 1: positive , -1 negative
    private int _direction;
    public int Direction
    {
        get
        {
            return _direction;
        }

        set
        {
            if (value < 0)
            {
                _direction = Constants.DIRECTION_NEGATIVE;
                _inverseIcon.SetActive(true);

            }
            else if (value > 0)
            {
                _direction = Constants.DIRECTION_POSITIVE;
                _inverseIcon.SetActive(false);

            }
             
            

        }
    }

    [SerializeField] private GameObject _inverseIcon;
    #endregion

    [SerializeField] private List<Tile> _tiles;
    private List<TileStar> _tileStars;
    private int _tilesCount;
    private int _current;
    

    public void RollANormalDice()
    {
        if (_normalDiceNum > 0)
        {
            _normalDiceNum--;
            int diceValue =  Random.Range(1, 7);
            MovePlayar(diceValue);
        }
    }
    
    
    public void RollAGoldenDice(int diceValue)
    {
        if (_goldenDiceNum > 0)
        {
            _goldenDiceNum--;
            MovePlayar(diceValue);

        }
    }



    public void MovePlayar(int diceValue)
    {
        if (_direction == Constants.DIRECTION_POSITIVE)
        {
            EarnStarPoint(_current, _current + diceValue);
            _current += diceValue;
            if(_current >= _tilesCount)
                _current -= _tilesCount;
        }
        else
        {
            _current -= diceValue;
            if (_current < 0)
                _current += _tilesCount;
            Direction = Constants.DIRECTION_POSITIVE;
        }
        Player.Instance.Move(_tiles[_current].transform.position);
        _tiles[_current].OnHere();
    }



    public void EarnStarPoint(int prev, int next)
    {
        int tmpPoint = 0;
        foreach(TileStar tileStar in _tileStars)
        {
            if(tileStar.Id > prev &&
                tileStar.Id <= next)
            {
                tmpPoint += tileStar.StarValue;
            }
        }
        StarPoint += tmpPoint;
    }



    private void Awake()
    {
        NormalDiceNum = Constants.NORMAL_DICE_NUBER_INIT;
        GoldenDiceNum = Constants.GOLDEN_DICE_NUBER_INIT;

        _tiles = GameObject.Find("Tiles").GetComponentsInChildren<Tile>().ToList();
        _tiles.Sort();

        foreach (var tile in _tiles)
        {
            if (tile is TileStar)
            {
                _tileStars.Add((TileStar)tile);
            }
        }
        //  _tileStars = _tileStars
        //      .Where(tile => tile is TileStar)
        //      .Select(tile => (TileStar)tile)
        //      .ToList();


        _tilesCount = _tiles.Count;
        _current = 0;
    }

}
