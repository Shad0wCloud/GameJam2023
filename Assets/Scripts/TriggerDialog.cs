using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField] private Dialog _dialogScript;
    [SerializeField] private int _numberDialog;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _dialogScript.StartDialog(_numberDialog);
        Destroy(this.gameObject);
    }
}
