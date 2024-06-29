using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void Recolor()
    {
        GetComponent<Renderer>().material.color = PickRandomColor();
    }

    private Color PickRandomColor()
    {
        Color color;
        float redComponent;
        float greenComponent;
        float blueComponent;

        redComponent = Random.Range(0.000f, 1.000f);
        greenComponent = Random.Range(0.000f, 1.000f);
        blueComponent = Random.Range(0.000f, 1.000f);

        color = new Color(redComponent, greenComponent, blueComponent);

        return color;
    }
}
