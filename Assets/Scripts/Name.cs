using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name : MonoBehaviour
{
    [ContextMenuItem("Randomize Name", "Randomize")]
    public string myName = "";

    [ContextMenu("Reset Name")]
    private void ResetName()
    {
        myName = string.Empty;
    }

    private void Randomize()
    {
        myName = "Whouhou !";
    }
}
