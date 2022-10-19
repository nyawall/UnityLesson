using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_bulletPrefab, _firePoint.position , Quaternion.identity);
        }
    }
}
