using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnd : MonoBehaviour
{
    [SerializeField] private GameObject[] _targetObjects;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _animatorObj;
    [SerializeField] private SpidersDisable _spidersDisable;
    [SerializeField] private Lever _leverObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _animator.SetTrigger("Death");
    }

    public void Action()
    {
        foreach (GameObject iter in _targetObjects)
        {
            if (iter.GetComponent<Door>()) iter.GetComponent<Door>().Action();
            else if (iter.GetComponent<OffObject>()) iter.GetComponent<OffObject>().Action();
            else if (iter.GetComponent<PlayerScale>()) iter.GetComponent<PlayerScale>().NewScale();
        }

        _leverObject.UnLock();
        _animatorObj.SetTrigger("Down");
    }

    public void SpiderDis()
    {
        _spidersDisable.Action();
    }
}
