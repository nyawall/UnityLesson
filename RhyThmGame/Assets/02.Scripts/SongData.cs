using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SongData
{
    public string Name;
    public List<NoteData> Notes;

    public SongData(string name)
    {
        Name = name;
        Notes = new List<NoteData>();
    }
}
