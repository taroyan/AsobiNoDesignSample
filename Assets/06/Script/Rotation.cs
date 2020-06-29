using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotAngle = 4.0f;   // 回転スピード

    void FixedUpdate()
    {
        transform.Rotate(0f,-rotAngle,0f);  // Y軸を軸にして回転
    }
}
