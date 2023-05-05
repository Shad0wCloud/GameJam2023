using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isActive;
    [SerializeField] private bool _isWork = true;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Vector3 _attackBoxScale;
    [SerializeField] private LayerMask _layerMask;

    private void Start()
    {
        _animator.SetBool("isActive", _isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isWork && !_isActive) Action();
    }

    public void Action()
    {
        _isActive = !_isActive;
        _animator.SetBool("isActive", _isActive);
    }

    public void Damage()
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
