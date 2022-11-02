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
            Debug.LogWarning("[NotesMaker] : VideoPlayer �� Ŭ���� �����ϴ�. Ŭ���� ������ �ּ���");
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
    ///  Ű �Է��� ������ �ش� Ű�� �ش��ϴ� ��Ʈ������ ����
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
        Debug.Log($"[NotesMaker] : {key} ��Ʈ�� ������. �ð� {time} ��");
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
        string dir = UnityEditor.EditorUtility.SaveFilePanelInProject("�뷡 ������ ����", _songData.Name, "json", "");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(_songData));
    }
}


