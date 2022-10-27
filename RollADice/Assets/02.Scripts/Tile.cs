using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IComparable<Tile>
{
    public int Id;

    public int CompareTo(Tile other)
    {
        int result = 0;
        if (this.Id < other.Id)
            return -1;
        else if (this.Id < other.Id)
            return 1;
        else
            return 0;
        
    }

    public virtual void OnHere()
    {
        Debug.Log($"Player on {name}");
    }
}
