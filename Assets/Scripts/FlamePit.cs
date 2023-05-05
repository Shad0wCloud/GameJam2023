using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePit : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private bool _isActive;
    [SerializeField] private bool _isWork = true;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Vector3 _attackBoxScale;
    [SerializeField] private LayerMask _layerMask;

    private void Start()
    {
        _fire.SetActive(_isActive);
    }

    private void FixedUpdate()
    {
        if (_isActive) Damage();
    }

    public void Action()
    {
        if (_isWork)
        {
            _isActive = !_isActive;
            _fire.SetActive(_isActive);
        }
    }
    
    public void ActionWork()
    {
        _isWork = !_isWork;
    }

    private void Damage()
    {
        Collider[] colliders = Physics.OverlapBox(_attackPoint.position, _attackBoxScale, Quaternion.identity, _layerMask);
        foreach (Collider iter in colliders)
        {
            if (iter.CompareTag("Player"))
            {
               // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("Damage");
            }
        }
    }
}
