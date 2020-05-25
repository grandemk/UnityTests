using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Draw : MonoBehaviour
{
    public Material LineMaterial = null;
    static LineRenderer CreateLine()
    {
        var obj = new GameObject("Line");
        obj.AddComponent<LineRenderer>();
        return obj.GetComponent<LineRenderer>();
    }

    static Color ToColor(string color)
    {
        if (color.Length == 6)
        {
            color += "FF"; // add alpha if missing
        }

        var hex = Convert.ToUInt32(color, 16);
        var r = ((hex & 0xff000000) >> 0x18) / 255f;
        var g = ((hex & 0xff0000) >> 0x10) / 255f;
        var b = ((hex & 0xff00) >> 8) / 255f;
        var a = ((hex & 0xff)) / 255f;

        return new Color(r, g, b, a);
    }
    void Start()
    {
        var line = CreateLine();
        float alpha = 1.0f;
        var green = ToColor("794A1C");
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(green, 0.0f), new GradientColorKey(green, 1.0f)},
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f)}
        );
        line.material = LineMaterial;
        line.colorGradient = gradient;
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        List<Vector3> positions = new List<Vector3>();
        positions.Add(Vector3.zero);
        positions.Add(new Vector3(1f, 0f, 0));
        positions.Add(new Vector3(1f, 1f, 0));
        positions.Add(new Vector3(2f, 1f, 0));
        positions.Add(new Vector3(1f, 2f, 0));
        line.positionCount = positions.Count;
        line.SetPositions(positions.ToArray());

        Debug.Log("Started Draw");
    }

    void Update()
    {
        
    }
}
