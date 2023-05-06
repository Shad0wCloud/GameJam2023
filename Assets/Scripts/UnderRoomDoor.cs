using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderRoomDoor : MonoBehaviour
{
    [SerializeField] private int number;

    public void Action()
    {
        number += 1;
        if (number >= 3) GetComponent<Door>().Action();
    }
}
