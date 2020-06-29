using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{

    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball") // タグが「Ball」?(Yes)
        {
            Destroy(other.gameObject,0.1f); // 0.1秒後に衝突物を破棄
        }
    }
}
