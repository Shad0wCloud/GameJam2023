using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightManager : MonoBehaviour
{
    [SerializeField] private int _dayState;
    [SerializeField] private Light _directionLight;
    [SerializeField] private Color[] _StageColor;

    private void Start()
    {
        _directionLight.color = _StageColor[_dayState];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) NewStage();
    }

    public void NewStage()
    {
        if(_dayState < _StageColor.Length)
        {
            StartCoroutine(InterpolationLight(_StageColor[_dayState], _StageColor[_dayState + 1]));
            _dayState++;
        }
    }

    private IEnumerator InterpolationLight(Color startColor, Color endColor)
    {
        float currTime = 0f;
        do
        {
           _directionLight.color = Color.Lerp(startColor, endColor, (currTime / 1f));

            currTime += Time.deltaTime * 0.5f;
            yield return null;
        }
        while (currTime <= 1f);
    }
}
