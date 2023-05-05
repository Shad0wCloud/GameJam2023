using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidersDisable : MonoBehaviour
{
    [SerializeField] private GameObject _spidersObject;
    [SerializeField] private Animator _animator;

    public void Action()
    {
         _animator.SetTrigger("Hide");        
    }

    public void DeactiveSpiders()
    {
        _spidersObject.SetActive(false);
    }
}
