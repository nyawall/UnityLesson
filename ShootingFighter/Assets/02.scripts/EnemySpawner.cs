using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnTime;
    [SerializeField] private BoxCollider _bound;
    private float _timer;

    private float _maxX;
    private float _minX;
    private float _maxZ;
    private float _minZ;

    private void Awake()
    {
        _bound = gameObject.GetComponent<BoxCollider>();
        _minX = _bound.transform.position.x - _bound.size.x / 2.0f;
        _minX = _bound.transform.position.x + _bound.size.x / 2.0f;
        _minZ = _bound.transform.position.z - _bound.size.z / 2.0f;
        _minZ = _bound.transform.position.z + _bound.size.z / 2.0f;
    } 

    private void Update()
    {
        if(_timer < 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(_minX, _maxX), 0.0f, Random.Range(_minZ, _maxZ));
            Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            _timer = _spawnTime;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

}
