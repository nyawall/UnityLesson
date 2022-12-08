using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool IsDetected => Current;
    public Collider2D Current;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _size;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _groundIgnoredLayer;
    private Collider2D _subject;
    private float _subjectHeight;

    private void Awake()
    {
        _subject = GetComponent<CapsuleCollider2D>();
        _subjectHeight = GetComponent<CapsuleCollider2D>().size.y;
    }

    public bool IsUnderGroundExist()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, _size, 0.0f, Vector2.down, 4.0f, _groundLayer);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != Current)
                    return true;
            }
        }
        return false;
    }

    public bool IgnoreCurrentGround()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, _size, 0.0f, Vector2.down, 4.0f, _groundLayer);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider == Current)
                {
                    StartCoroutine(E_IgnoreGround(hits[i].collider, hits[i]));
                    return true;
                }
            }
        }
        return false;
    }

    private IEnumerator E_IgnoreGround(Collider2D ground, RaycastHit2D hit)
    {
        Physics2D.IgnoreCollision(_subject, ground, true);
        ground.gameObject.layer = 9;

        // 한번은 발이 땅 밑으로 내려가야 함
        yield return new WaitUntil(() => _subject.transform.position.y < hit.point.y);

        // 그라운드 무시를 해제하는 조건 (발이 완전히 땅 위로 올라가던지, 머리가 완전히 땅 밑으로 내려가던지)
        yield return new WaitUntil(() => (_subject.transform.position.y > hit.point.y) || (_subject.transform.position.y + _subjectHeight < hit.point.y));

        Physics2D.IgnoreCollision(_subject, ground, false);
        ground.gameObject.layer = 8;
    }

    private void FixedUpdate()
    {
        Current = Physics2D.OverlapBox((Vector2)transform.position + _offset,
                                        _size,
                                        0.0f,
                                        _groundLayer);        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.5f, 1.0f, 0.5f, 1.0f);
        Gizmos.DrawWireCube(transform.position + Vector3.down * 1.0f,
                            new Vector3(_size.x, _size.y + 2.0f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_offset,
                            _size);        
    }
}
