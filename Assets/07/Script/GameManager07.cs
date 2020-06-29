using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager07 : MonoBehaviour
{
    public int life;    // ライフ
    public GameObject ballPrefab;   // ボールプレハブ
    public Text textGameOver;   // ゲームオーバーテキスト
    void Start()
    {
        life = 3;   // ライフを３にセット
        textGameOver.enabled = false;   // ゲームオーバーテキストは非表示
    }

    void Update()
    {
        GameObject ballObj = GameObject.Find("Ball");   // オブジェクト名が「Ball」のオブジェクトを取得
        if (ballObj == null)    // オブジェクトがない?(Yes)
        {
            --life; // ライフを一つ減らす
            if (life > 0)   // ライフが０より大きい?(Yes)
            {
                GameObject newBall = Instantiate(ballPrefab);   // ボールを生成
                newBall.name = ballPrefab.name; // 新しく生成したゲームオブジェクトの名前をプレハブの名前と同じにする
            }
            else
            {
                life = 0;   // ライフを０にセット
                textGameOver.enabled = true;    // ゲームオーバーテキストを表示
            }
        }
    }
}
