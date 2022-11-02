using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

public class SongSelector : MonoBehaviour
{
    public string SelectedSongName;
    public VideoClip Clip;
    public SongData Data;

    public void Select(string songName)
    {
        SelectedSongName = songName;
    }

    public void Load()
    {
        if (string.IsNullOrEmpty(SelectedSongName))
            return; 
       
        //���� ��� �õ� ����
        try
        {
            Clip = Resources.Load<VideoClip>($"SongClips/{SelectedSongName}");
            TextAsset dataText = Resources.Load<TextAsset>($"SongData/{SelectedSongName}");
            Data = JsonUtility.FromJson<SongData>(dataText.ToString());
            
        }
        catch (System.Exception e)
        {

            
            Debug.LogError($"[SongSelector] : �ε� ���� ... {e.Message}");
        }
        
        
    }
}
