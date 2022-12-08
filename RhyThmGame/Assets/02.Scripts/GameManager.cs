using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public enum States
    {
        Idle,
        LoadSongData,
        WaitForSongDataLoaded,
        StartGame,
        WaitForGameFinished,
        DisplayScore,
        WaitForUser
    }
    public States Current;

    public void MoveNext() => Current++;
    public void OnPlayButtonClick()
    {
        if (SongSelector.Instance.IsSelected)
            Current = States.LoadSongData;
        else
            Debug.LogWarning("[GameManager] : 곡을 먼저 선택하세요 !");
    }


    private void Update()
    {
        switch (Current)
        {
            case States.Idle:
                break;
            case States.LoadSongData:
                {
                    SongSelector.Instance.Load();
                    MoveNext();
                }
                break;
            case States.WaitForSongDataLoaded:
                {
                    if (SongSelector.Instance.IsLoaded)
                        MoveNext();
                }
                break;
            case States.StartGame:
                {
                    SceneManager.LoadScene("Play");
                    MoveNext();
                }
                break;
            case States.WaitForGameFinished:
                {
                    //
                }
                break;
            case States.DisplayScore:
                break;
            case States.WaitForUser:
                break;
            default:
                break;
        }
    }
}
