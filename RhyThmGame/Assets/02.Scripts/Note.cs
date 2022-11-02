using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitTypes
{
    None,
    Bad,
    Miss,
    Good,
    Great,
    Cool
}

public class Note : MonoBehaviour
{
    public void Hit(HitTypes hitTypes)
    {
        switch (hitTypes)
        {
            case HitTypes.None:
                break;
            case HitTypes.Bad:
                break;
            case HitTypes.Miss:
                break;
            case HitTypes.Good:
                break;
            case HitTypes.Great:
                break;
            case HitTypes.Cool:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.position += Vector3.down * Time.fixedDeltaTime;
    }
}
