using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isPuhed = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") isPuhed = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") isPuhed = true;
    }
}
