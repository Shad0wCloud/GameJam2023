using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    [SerializeField] private float[] _scaleStages;

    private int _currentScaleStage;
    private Transform _player;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) NewScale();
    }

    public void ScaleOnSpawn(int stage)
    {
        transform.localScale = new Vector3(_scaleStages[stage], _scaleStages[stage], _scaleStages[stage]);
        _currentScaleStage = stage;
    }

    public void NewScale()
    {
        _currentScaleStage++;
        StartCoroutine(Scaling(_currentScaleStage));
    }

    public void NewScale(int stage)
    {
        StartCoroutine(Scaling(stage));
        _currentScaleStage = stage;
    }

    private IEnumerator Scaling(int newScaleStage)
    {
        if (newScaleStage < 1)
        {
            transform.localScale = new Vector3(_scaleStages[newScaleStage], _scaleStages[newScaleStage], _scaleStages[newScaleStage]);
        }
        else if (newScaleStage < _scaleStages.Length)
        {
            float currTime = 0f;
            do
            {
                float newScale = Mathf.Lerp(_scaleStages[newScaleStage - 1], _scaleStages[newScaleStage], (currTime / 1f));
                transform.localScale = new Vector3(newScale, newScale, newScale);

                currTime += Time.deltaTime * 0.5f;
                yield return null;
            }
            while (currTime <= 1f);
        }
    }
}
