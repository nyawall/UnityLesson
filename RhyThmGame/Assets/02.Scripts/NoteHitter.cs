using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteHitter : MonoBehaviour
{
    private const float HIT_JUDGE_RANGE_COOL = 0.3f;
    private const float HIT_JUDGE_RANGE_GREAT = 0.5f;
    private const float HIT_JUDGE_RANGE_GOOD = 0.7f;
    private const float HIT_JUDGE_RANGE_MISS = 0.9f;
    private const float HIT_JUDGE_RANGE_SPEED_GAIN = 0.05f;
    public KeyCode Key;
    [SerializeField] private LayerMask _noteLayer;
    private SpriteRenderer _spriteRenderer;
    private Color _colorOrigin;
    [SerializeField] private Color _colorPressed;
    [SerializeField] private GameObject _spotlightEffect;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colorOrigin = _spriteRenderer.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            Hit();
            SetColorPressed();
            _spotlightEffect.SetActive(true);
        }
        if (Input.GetKeyUp(Key))
        {
            SetColorOrigin();
            _spotlightEffect.SetActive(false);
        }
    }

    private void Hit()
    {
        HitTypes hitType = HitTypes.Bad;

        // 일단 범위내 모든 노트 감지
        Collider2D[] cols = Physics2D.OverlapBoxAll(point: transform.position,
                                                    size: new Vector2(transform.lossyScale.x / 2.0f,
                                                                      HIT_JUDGE_RANGE_MISS + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN),
                                                    angle: 0.0f,
                                                    layerMask: _noteLayer
                                                    );

        if (cols.Length > 0)
        {
            // 가장 가까운 노드 선출
            Collider2D first = cols.ToList()
                                   .OrderBy(x => Mathf.Abs(x.transform.position.y - transform.position.y))
                                   .First();

            // 가장 가까운 노드와 히터간의 거리
            float distance = Mathf.Abs(first.transform.position.y - transform.position.y);

            // 거리에 따라서 히트 판정
            if      (distance < HIT_JUDGE_RANGE_COOL)  hitType = HitTypes.Cool;
            else if (distance < HIT_JUDGE_RANGE_GREAT) hitType = HitTypes.Great;
            else if (distance < HIT_JUDGE_RANGE_GOOD)  hitType = HitTypes.Good;
            else                                       hitType = HitTypes.Miss;

            // 판정된 노트 히트하기
            first.GetComponent<Note>().Hit(hitType);
        }
    }


    private void SetColorPressed()
    {
        _spriteRenderer.color = _colorPressed;
    }

    private void SetColorOrigin()
    {
        _spriteRenderer.color = _colorOrigin;
    }

    private void OnDrawGizmos()
    {
        if (NoteSpawnManager.Instance == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2.0f,
                                        HIT_JUDGE_RANGE_COOL + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN,
                                        0.0f));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(transform.lossyScale.x / 2.0f,
                                        HIT_JUDGE_RANGE_GREAT + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN,
                                        0.0f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, 
                            new Vector3(transform.lossyScale.x / 2.0f,
                                        HIT_JUDGE_RANGE_GOOD + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN,
                                        0.0f));
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, 
                            new Vector3(transform.lossyScale.x / 2.0f,
                                        HIT_JUDGE_RANGE_MISS + NoteSpawnManager.Instance.NoteSpeedScale * HIT_JUDGE_RANGE_SPEED_GAIN,
                                        0.0f));
    }
}
