using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEarth : MonoBehaviour
{
    public float speed = 50f;
   

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0,Time.deltaTime * speed,0));
    }
}
