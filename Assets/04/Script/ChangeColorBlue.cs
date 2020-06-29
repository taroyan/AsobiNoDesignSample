using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorBlue : MonoBehaviour
{
    
    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball") // 衝突したゲームオブジェクトのタグ名が「Ball」?(Yes)
        {
            GetComponent<Renderer>().material.color = Color.blue;   // このスクリプトがアタッチされているゲームオブジェクトのマテリアルの色を青色に変える
        }
    }
}
