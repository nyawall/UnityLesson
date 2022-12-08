using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextManager : MonoBehaviour
{
    public static PopUpTextManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private PopUpText _bad;
    [SerializeField] private PopUpText _miss;
    [SerializeField] private PopUpText _good;
    [SerializeField] private PopUpText _great;
    [SerializeField] private PopUpText _cool;
    [SerializeField] private PopUpText _comboTitle;
    [SerializeField] private PopUpText _combo;

    public void PopUp(HitTypes hitType)
    {
        switch (hitType)
        {
            case HitTypes.None:
                break;
            case HitTypes.Bad:
                {
                    _bad.PopUp();
                    _bad.transform.Translate(Vector3.back);
                    _miss.transform.Translate(Vector3.forward);
                    _good.transform.Translate(Vector3.forward);
                    _great.transform.Translate(Vector3.forward);
                    _cool.transform.Translate(Vector3.forward);
                }
                break;
            case HitTypes.Miss:
                {
                    _miss.PopUp();
                    _bad.transform.Translate(Vector3.forward);
                    _miss.transform.Translate(Vector3.back);
                    _good.transform.Translate(Vector3.forward);
                    _great.transform.Translate(Vector3.forward);
                    _cool.transform.Translate(Vector3.forward);
                }
                break;
            case HitTypes.Good:
                {
                    _good.PopUp();
                    _bad.transform.Translate(Vector3.forward);
                    _miss.transform.Translate(Vector3.forward);
                    _good.transform.Translate(Vector3.back);
                    _great.transform.Translate(Vector3.forward);
                    _cool.transform.Translate(Vector3.forward);
                }
                break;
            case HitTypes.Great:
                {
                    _great.PopUp();
                    _bad.transform.Translate(Vector3.forward);
                    _miss.transform.Translate(Vector3.forward);
                    _good.transform.Translate(Vector3.forward);
                    _great.transform.Translate(Vector3.back);
                    _cool.transform.Translate(Vector3.forward);
                }
                break;
            case HitTypes.Cool:
                {
                    _cool.PopUp();
                    _bad.transform.Translate(Vector3.forward);
                    _miss.transform.Translate(Vector3.forward);
                    _good.transform.Translate(Vector3.forward);
                    _great.transform.Translate(Vector3.forward);
                    _cool.transform.Translate(Vector3.back);
                }
                break;
            default:
                break;
        }

        if (GameStatus.CurrentCombo > 1)
        {
            _comboTitle.PopUp();
            _combo.PopUp(GameStatus.CurrentCombo.ToString());
        }
    }
}
