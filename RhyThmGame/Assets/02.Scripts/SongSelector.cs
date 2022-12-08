using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class SongSelector : MonoBehaviour
{
    public static SongSelector Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public string SelectedSongName;
    public VideoClip Clip;
    public SongData Data;
    public bool IsLoaded { get; private set; }
    public bool IsSelected { get; private set; }

    public void Select(string songName)
    {
        if (string.IsNullOrEmpty(songName))
        {
            IsSelected = false;
            return;
        }

        SelectedSongName = songName;
        IsSelected = true;
    }

    public void Load()
    {
        if (string.IsNullOrEmpty(SelectedSongName))
            return;

        // ���� ��� �õ� ����
        try
        {
            Clip = Resources.Load<VideoClip>($"SongClips/{SelectedSongName}");
            TextAsset dataText = Resources.Load<TextAsset>($"SongData/{SelectedSongName}");
            Data = JsonUtility.FromJson<SongData>(dataText.ToString());
            IsLoaded = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SongSelector] : �ε� ���� ... {e.Message}");
        }
    }
}
