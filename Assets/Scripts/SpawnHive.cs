using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHive : MonoBehaviour
{
    [SerializeField] private GameObject _hive;
    [SerializeField] private Transform[] _pointsSpawn;

    [SerializeField] private int _activeHive;
    [SerializeField] private int _coolldownLittle;
    [SerializeField] private int _coolldownBig;
    private bool _isCooldawn = false;

    private void Update()
    {
        if (_activeHive < 5 & !_isCooldawn) Spawn();
    }

    private void Spawn()
    {
        _activeHive++;
        int randomPos = Random.Range(0, _pointsSpawn.Length);

        GameObject newHive = Instantiate(_hive);
        newHive.transform.localPosition = _pointsSpawn[randomPos].position;
        int randomRot = Random.Range(0, 360);
        newHive.transform.localRotation = Quaternion.Euler(0, randomRot, 0);

        StartCoroutine(Cooldawn());
    }

    private IEnumerator Cooldawn()
    {
        int coldown = Random.Range(_coolldownLittle, _coolldownBig);
        _isCooldawn = true;
        yield return new WaitForSeconds(coldown);
        _isCooldawn = false;
    }
}
