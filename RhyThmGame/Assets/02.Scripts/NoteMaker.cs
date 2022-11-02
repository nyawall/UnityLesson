using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;
public class NoteMaker : MonoBehaviour
{
    private KeyCode[] _keys = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L };
    private SongData _songData;
    private VideoPlayer _vp;
    private bool _onRecording;
    public void StartRecord()
    {
        if (_onRecording)

            return;

        if (_vp.clip == null)
        {
            Debug.LogWarning("[NotesMaker] : VideoPlayer 에 클립이 없습니다. 클립을 참조해 주세요");
        }

        _songData = new SongData(_vp.clip.name);
        _vp.Play();
        _onRecording = true;
    }
    public void StopRecord()
    {
        if (_onRecording == false)
            return;

        SaveSongData();
        _songData = null;
        _vp.Stop();
    }

    /// <summary>
    ///  키 입력이 들어오면 해당 키에 해당하는 노트데이터 생성
    /// </summary>
    private void Record()
    {
        foreach(KeyCode key in _keys)
        {
            if (Input.GetKeyDown(key))
                CreateNoteData(key);
        }
    }

    private void CreateNoteData(KeyCode key)
    {
        float time = (float) System.Math.Round(_vp.time, 2);
        _songData.Notes.Add(new NoteData(key, time));
        Debug.Log($"[NotesMaker] : {key} 노트가 생성됨. 시간 {time} 초");
    }

    private void Awake()
    {
        _vp = GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        if (_onRecording)
            Record();
    }

    private void SaveSongData()
    {
        string dir = UnityEditor.EditorUtility.SaveFilePanelInProject("노래 데이터 저장", _songData.Name, "json", "");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(_songData));
    }
}


