using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpAssets : MonoBehaviour
{
    private static DamagePopUpAssets _instance;
    public static DamagePopUpAssets Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<DamagePopUpAssets>("DamagePopUpAssets"));
            return _instance;
        }
    }

    [SerializeField] private List<DamagePopUp> _items;

    public DamagePopUp GetDamagePopUp(LayerMask layer)
    {
        return _items.Find(x => x.Layer == layer);
    }
}
