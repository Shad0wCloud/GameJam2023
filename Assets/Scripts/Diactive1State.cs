using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diactive1State : MonoBehaviour
{
    [SerializeField] private Door _doorScript;
    [SerializeField] private RoomManager _roomManagerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _doorScript.Action();
            _roomManagerScript.ActiveStuff(0, false);
            _roomManagerScript.WriteStage(1);
            Destroy(gameObject);
        }
    }
}
