using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerScale : MonoBehaviour
{
    [SerializeField] private bool _isOneUse = true;
    private PlayerScale _playerScaleScript;

    private void Start()
    {
        _playerScaleScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScale>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Action();
    }

    private void Action()
    {
        _playerScaleScript.NewScale();

        if (_isOneUse) Destroy(this.gameObject);
    }
}
