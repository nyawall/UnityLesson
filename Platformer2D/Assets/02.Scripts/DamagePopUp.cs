using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    public LayerMask Layer;
    private TMP_Text _text;
    private float _fadeSpeed = 2.0f;
    private float _moveSpeedY = 0.5f;
    private Color _color;

    public static DamagePopUp Create(LayerMask layer, Vector3 pos, int damage)
    {
        DamagePopUp damagePopUp = Instantiate(DamagePopUpAssets.Instance.GetDamagePopUp(layer),
                                              pos,
                                              Quaternion.identity);
        damagePopUp._text.SetText(damage.ToString());
        return damagePopUp;
    }


    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _color = _text.color;
    }

    private void Update()
    {
        transform.position += Vector3.up * _moveSpeedY * Time.deltaTime;

        _color.a -= _fadeSpeed * Time.deltaTime;
        _text.color = _color;

        if (_color.a <= 0.0f)
            Destroy(gameObject);
    }

}
