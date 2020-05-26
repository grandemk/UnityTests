using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTree
{

    public GenTree(LineRenderer line, float r, float theta)
    {
        this.theta = theta;
        this.r = r;
        this.line = line;
    }
    public List<GenTree> children = new List<GenTree>();
    public LineRenderer line;
    public float theta;
    public float r;
}
