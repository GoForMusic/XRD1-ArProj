using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeColorRandom : MonoBehaviour
{
    private Renderer _meshRenderer;
    private Color newColorSet;
    private float randomChannelOne, randomChannelTwo, randomChannelThree;

    private void Start()
    {
        _meshRenderer = gameObject.GetComponent<Renderer>();
        ChangeColor();
    }

    private void ChangeColor()
    {
        randomChannelOne = Random.Range(0f, 1f);
        randomChannelTwo = Random.Range(0f, 1f);
        randomChannelThree = Random.Range(0f, 1f);

        newColorSet = new Color(randomChannelOne, randomChannelTwo, randomChannelThree, 1f);
        
        _meshRenderer.material.SetColor("_Color",newColorSet);
    }
}
