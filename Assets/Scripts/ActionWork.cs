using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWork : MonoBehaviour
{
    [SerializeField] private FlamePit _flamePit;

    public void WorkOff()
    {
        _flamePit.ActionWork();
    }
}
