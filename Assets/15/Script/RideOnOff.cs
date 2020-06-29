using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideOnOff : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameObject emptyObject = new GameObject();  // ゲームオブジェクトemptyObjectを作成
        emptyObject.transform.parent = this.transform;  // emptyObjectの親オブジェクトをこのスクリプトがアタッチされているゲームオブジェクトとする。(=emptyObjectが子になる)
        other.transform.parent = emptyObject.transform; // 接触したゲームオブジェクト(プレイヤー)の親オブジェクトをemptyObjectとする（プレイヤーを子にする）
        emptyObject.name = "empty"; // emptyObjectのゲームオブジェクトの名前を「empty」にする
    }

    void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;  // 接触から抜けたゲームオブジェクト（プレイヤー）の親オブジェクトをなくす（=ヒエラルキーにおいて、シーン直下にプレイヤーのゲームオブジェクトが配置される）
        GameObject emptyObject = GameObject.Find("empty");  // 「empty」オブジェクトがあるか検索 
        Destroy(emptyObject);   // 「empty」オブジェクトを破棄
    }
}
