using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitTrigger : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnLevelComplete += ChangeColour;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelComplete -= ChangeColour;
    }

    private void ChangeColour()
    {
        var mat = gameObject.GetComponent<Renderer>();
        mat.material.color = Color.green;
    }
}
