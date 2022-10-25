using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Id;

    public virtual void OnHere()
    {
        Debug.Log($"Player on {name}");
    }
}
