using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdvantGhost : MonoBehaviour
{
    [SerializeField] private Ghost _ghostSctipt;
    [SerializeField] private Collider _collider;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _collider.enabled = false;
        _agent.enabled = false;
        _ghostSctipt.enabled = false;
    }

    public void Action()
    {
        _animator.SetBool("isLitle", false);
    }

    public void EnabledComponents()
    {
        Debug.Log("++");
        _collider.enabled = true;
        _agent.enabled = true;
        _ghostSctipt.enabled = true;
    }
}
