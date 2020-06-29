using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeBlock : MonoBehaviour
{
    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball") // 衝突したゲームオブジェクトのタグ名が「Ball」?(Yes)
        {
            GetComponent<Transform>().localScale = new Vector3(1/2f,1.0f,1/2f); // このスクリプトがアタッチされているゲームオブジェクトのスケールを変更
        }
    }
}
