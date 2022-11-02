using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NoteData
{
    public KeyCode Key;
    public float Time;

    public NoteData(KeyCode key, float time)
    {
        Key = key;
        Time = time;
    }
}
