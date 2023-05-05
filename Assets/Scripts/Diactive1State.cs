using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diactive1State : MonoBehaviour
{
    [SerializeField] private Door _doorScript;
    [SerializeField] private RoomManager _roomManagerScript;
    [SerializeField] private Dialog _dialogScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _doorScript.Action();
            _roomManagerScript.ActiveStuff(0, false);
            _roomManagerScript.WriteStage(1);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScale>().NewScale();
            _dialogScript.StartDialog(2);
            Destroy(gameObject);
        }
    }
}
