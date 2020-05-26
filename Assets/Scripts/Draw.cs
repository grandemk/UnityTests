using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class Draw : MonoBehaviour
{
    public Material LineMaterial = null;
    public List<GenTree> curLeaves = new List<GenTree>();

    Color trunkColor = ToColor("794A1C");
    Color leavesColor = ToColor("133609");

    float leavesThreshold = 0.3f;
    int numIteration = 0;
    bool paused = false;
    int maxIteration = 10;

    LineRenderer CreateLine(Vector3 a, Vector3 b, Color color)
    {
        var obj = new GameObject("Line");
        var line = obj.AddComponent<LineRenderer>();

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f)},
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f)}
        );
        line.material = LineMaterial;
        line.colorGradient = gradient;
        line.startWidth = 0.04f;
        line.endWidth = 0.04f;
        List<Vector3> positions = new List<Vector3>();
        positions.Add(a);
        positions.Add(b);
        line.positionCount = positions.Count;
        line.SetPositions(positions.ToArray());

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

    static Vector3 CreatePolarVector3(float r, float theta)
    {
       return new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta), 0.0f);
    }

    static Color RandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
    }

    void Update()
    {
      if(Input.GetKeyDown("space"))
        paused = !paused;
      if(Input.GetKeyDown("m"))
        maxIteration += 1;
    }

    IEnumerator GrowTree()
    {
        while(true)
        {
            while (paused || numIteration > maxIteration)
                yield return null;

            numIteration += 1;
            List<GenTree> toAdd = new List<GenTree>();
            List<GenTree> toRemove = new List<GenTree>();
            for (int i = curLeaves.Count - 1; i >= 0; --i)
            {
                GenTree curTree = curLeaves[i];
                int numChild = 2;
                for (int j = 0; j < numChild; ++j)
                {
                    float r = curTree.r * Random.Range(0.5f, 0.9f);
                    var intersection = Random.Range(0.2f, 0.9f);
                    float theta = curTree.theta + Random.Range(-0.30f, 0.30f) * Mathf.PI;

                    Color color = trunkColor;
                    if (r < leavesThreshold)
                        color = leavesColor;

                    Vector3 a = Vector3.Lerp(curTree.line.GetPosition(0), curTree.line.GetPosition(1), intersection);
                    var b = a + CreatePolarVector3(r, theta);
                    curTree.children.Add(new GenTree(CreateLine(a, b, color), r, theta));
                }
                toAdd.AddRange(curTree.children);
                curLeaves.RemoveAt(i);
            }
            curLeaves.AddRange(toAdd);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Start()
    {
        trunkColor = RandomColor();
        leavesColor = RandomColor();
        leavesThreshold = Random.Range(0.3f, 0.6f);

        var r = Random.Range(1f, 5f);
        var theta = Random.Range(0.30f, 0.70f) * Mathf.PI;
        curLeaves.Add(new GenTree(CreateLine(Vector3.zero, CreatePolarVector3(r, theta), trunkColor), r, theta));

        StartCoroutine("GrowTree");
    }
}
