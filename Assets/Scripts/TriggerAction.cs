using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    [SerializeField] private GameObject[] _targetObjects;
    [SerializeField] private bool _isOneUse;

    [SerializeField] private bool _isThisObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Action();
        else if (_isThisObject) ActionThisObject(other.gameObject);
    }

    private void Action()
    {
        foreach (GameObject iter in _targetObjects)
        {
            if (iter.GetComponent<Door>()) iter.GetComponent<Door>().Action();
            else if (iter.GetComponent<OffObject>()) iter.GetComponent<OffObject>().Action();
            else if (iter.GetComponent<FlamePit>()) iter.GetComponent<FlamePit>().Action();
            else if (iter.GetComponent<ActionWork>()) iter.GetComponent<ActionWork>().WorkOff();
            else if (iter.GetComponent<RoomManager>()) iter.GetComponent<RoomManager>().NewStage();
            else if (iter.GetComponent<Bee>()) iter.GetComponent<Bee>().Action();
        }

        if (_isOneUse)
        {
            Destroy(this.gameObject);
        }
    }

    private void ActionThisObject(GameObject obj)
    {
        if (obj.GetComponent<Bee>()) obj.GetComponent<Bee>().Action();
    }
}
