using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;   // ボールプレハブ

    void Update()
    {
        GameObject ballObj = GameObject.Find("Ball");   // オブジェクト名が「Ball」のオブジェクトを取得
        if (ballObj == null)    // オブジェクトがない?(Yes)
        {
            GameObject newBall = Instantiate(ballPrefab);   // ボールを生成
            newBall.name = ballPrefab.name; // 新しく生成したゲームオブジェクトの名前をプレハブの名前と同じにする

        }
    }
}
