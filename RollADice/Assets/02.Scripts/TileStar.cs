using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TileStar : Tile
{
    private int _starValue;
    public int StarValue
    {
        get
        {
            return _starValue;
        }
        set
        {
            _starValue = value;
            _starValueText.text = _starValue.ToString();
        }
    }
    [SerializeField] private int _starValueInit = 3;


    [SerializeField] private TMP_Text _starValueText;
    public override void OnHere()
    {
        base.OnHere();
        StarValue++;
    }
    private void Awake()
    {
        StarValue = _starValueInit;
    }
}
