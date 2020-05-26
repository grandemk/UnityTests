using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        float hrz = Input.GetAxis("Horizontal");
        float vrt = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hrz, vrt, 0);
        transform.Translate(Time.deltaTime * speed * direction);
    }
}
