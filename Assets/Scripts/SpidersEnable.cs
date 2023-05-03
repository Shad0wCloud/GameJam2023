using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidersEnable : MonoBehaviour
{
    [SerializeField] private GameObject _spidersObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isActive;
    [SerializeField] private GameObject _ghost;
    [SerializeField] private GameObject _ghostEnd;


    private void Start()
    {
        _spidersObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Action();
        }
    }

    private void Action()
    {
        if (_isActive)
        {
            _spidersObject.SetActive(true);
            _animator.SetTrigger("Show");
            _ghost.SetActive(false);
            _ghostEnd.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScale>().NewScale();
            gameObject.SetActive(false);
        }
        else
        {
            _animator.SetTrigger("Hide");
        }
    }

    public void DeactiveSpiders()
    {
        _spidersObject.SetActive(false);
    }
}
