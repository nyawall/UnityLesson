using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetector : MonoBehaviour
{
    public bool IsGoUpPossible;
    public bool IsGoDownPossible;

    private Collider2D _upLadder;
    private Collider2D _downLadder;

    public float UpTopEscapePosY => UpLadderTopY + _topEscapeOffset;
    public float UpBottomEscapePosY => UpLadderBottomY + _bottomEscapeOffset;
    public float DownTopEscapePosY => DownLadderTopY + _topEscapeOffset;
    public float DownBottomEscapePosY => DownLadderBottomY + _bottomEscapeOffset;

    public float UpTopStartPosY => UpLadderTopY + _topStartOffset;
    public float UpBottomStartPosY => UpLadderBottomY + _bottomStartOffset;
    public float DownTopStartPosY => DownLadderTopY + _topStartOffset;
    public float DownBottomStartPosY => DownLadderBottomY + _bottomStartOffset;
    public float UpPosX;
    public float DownPosX;


    public bool CanEscape
    {
        get
        {
            if (_upLadder)
            {
                return _subject.transform.position.y > UpLadderTopY + _topEscapeOffset ||
                       _subject.transform.position.y < UpLadderBottomY + _bottomEscapeOffset;
            }
            if (_downLadder)
            {
                return _subject.transform.position.y > DownLadderTopY + _topEscapeOffset ||
                       _subject.transform.position.y < DownLadderBottomY + _bottomEscapeOffset;
            }
            return false;
        }
    }

    private float _upDetectOffset => _subjectHeight * 0.8f;
    private float _downDetectOffset => -_subjectHeight * 0.2f;
    private float _topEscapeOffset => -_subjectHeight * 0.1f;
    private float _bottomEscapeOffset => -_subjectHeight * 0.1f;
    private float _topStartOffset => -_subjectHeight * 0.2f;
    private float _bottomStartOffset => +_subjectHeight * 0.2f;

    public float UpLadderTopY;
    public float UpLadderBottomY;
    public float DownLadderTopY;
    public float DownLadderBottomY;

    private Collider2D _subject;
    private float _subjectHeight;
    [SerializeField] private LayerMask _ladderLayer;


    private void Awake()
    {
        _subject = GetComponent<CapsuleCollider2D>();
        _subjectHeight = GetComponent<CapsuleCollider2D>().size.y;
    }

    private void FixedUpdate()
    {
        _upLadder = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + _upDetectOffset),
                                            0.01f,
                                            _ladderLayer);

        if (_upLadder != null)
        {
            BoxCollider2D ladderBoxCol = (BoxCollider2D)_upLadder;
            UpLadderBottomY = ladderBoxCol.transform.position.y + ladderBoxCol.offset.y - ladderBoxCol.size.y * 0.5f;
            UpLadderTopY = UpLadderBottomY + ladderBoxCol.size.y;
            UpPosX = ladderBoxCol.transform.position.x + ladderBoxCol.offset.x;
            IsGoUpPossible = true;
        }
        else
        {
            IsGoUpPossible = false;
        }

        _downLadder = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + _downDetectOffset),
                                              0.01f,
                                              _ladderLayer);

        if (_downLadder != null)
        {
            BoxCollider2D ladderBoxCol = (BoxCollider2D)_downLadder;
            DownLadderBottomY = ladderBoxCol.transform.position.y + ladderBoxCol.offset.y - ladderBoxCol.size.y * 0.5f;
            DownLadderTopY = DownLadderBottomY + ladderBoxCol.size.y;
            DownPosX = ladderBoxCol.transform.position.x + ladderBoxCol.offset.x;
            IsGoDownPossible = true;
        }
        else
        {
            IsGoDownPossible = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + _upDetectOffset, 0.0f), 0.01f);
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + _downDetectOffset, 0.0f), 0.01f);


        if (_upLadder)
        {
            Gizmos.color = Color.white;
            BoxCollider2D boxCol = (BoxCollider2D)_upLadder;
            Gizmos.DrawWireCube(boxCol.transform.position + (Vector3)boxCol.offset, boxCol.size);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(new Vector3(boxCol.transform.position.x - boxCol.size.x * 0.5f, UpLadderTopY + _topEscapeOffset, 0.0f),
                            new Vector3(boxCol.transform.position.x + boxCol.size.x * 0.5f, UpLadderTopY + _topEscapeOffset, 0.0f));
            Gizmos.DrawLine(new Vector3(boxCol.transform.position.x - boxCol.size.x * 0.5f, UpLadderBottomY + _bottomEscapeOffset, 0.0f),
                            new Vector3(boxCol.transform.position.x + boxCol.size.x * 0.5f, UpLadderBottomY + _bottomEscapeOffset, 0.0f));
        }
        if (_downLadder)
        {
            Gizmos.color = Color.white;
            BoxCollider2D boxCol = (BoxCollider2D)_downLadder;
            Gizmos.DrawWireCube(boxCol.transform.position + (Vector3)boxCol.offset, boxCol.size);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(new Vector3(boxCol.transform.position.x - boxCol.size.x * 0.5f, DownLadderTopY + _topEscapeOffset, 0.0f),
                            new Vector3(boxCol.transform.position.x + boxCol.size.x * 0.5f, DownLadderTopY + _topEscapeOffset, 0.0f));
            Gizmos.DrawLine(new Vector3(boxCol.transform.position.x - boxCol.size.x * 0.5f, DownLadderBottomY + _bottomEscapeOffset, 0.0f),
                            new Vector3(boxCol.transform.position.x + boxCol.size.x * 0.5f, DownLadderBottomY + _bottomEscapeOffset, 0.0f));
        }
    }
}
