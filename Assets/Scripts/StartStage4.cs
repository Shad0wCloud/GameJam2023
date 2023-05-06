using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStage4 : MonoBehaviour
{
    [SerializeField] private Cauldron cauldron;

    public void Start4Stage()
    {
        cauldron.ChengeMode();
    }
}
