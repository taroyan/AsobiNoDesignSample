using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager08 : MonoBehaviour
{
    public int life;                // ライフ
    public GameObject ballPrefab;   // ボールプレハブ
    public Text textGameOver;       // ゲームオーバーテキスト
    private int score;              // スコア
    private Text textScore;         // スコアテキスト
    private Text textLife;          // ライフテキスト
    private bool inGame;            // ゲーム中かどうかフラグ

    void Start()
    {
        life = 3;   // ライフを３にセット
        textGameOver.enabled = false;   // ゲームオーバーテキストは非表示
        score = 0;  // スコアはゼロ
        textScore = GameObject.Find("Score").GetComponent<Text>();  // ゲームオブジェクト名「Score」にアタッチされているTextコンポーネントを取得
        textLife = GameObject.Find("BallLife").GetComponent<Text>();    // ゲームオブジェクト名「BallLife」にアタッチされているTextコンポーネントを取得
        SetScoreText(score);    // スコアテキストをセットする
        SetLifeText(life);      // ライフテキストをセットする
        inGame = true;
    }

    private void SetScoreText(int score)
    {
        textScore.text = "Score:" + score.ToString();   // スコアをセット
    }

    private void SetLifeText(int life)
    {
        textLife.text = "Ball:" + life.ToString();      // ライフをセット
    }

    public void AddScore(int point)
    {
        if (inGame) // ゲーム中?(Yes)
        {
            score += point; // ポイント(point)を加算
            SetScoreText(score);    // スコアをセットする
        }
    }

    void Update()
    {
        if (inGame) // ゲーム中?(Yes)
        {
            GameObject ballObj = GameObject.Find("Ball");   // オブジェクト名が「Ball」のオブジェクトを取得
            if (ballObj == null)    // オブジェクトがない?(Yes)
            {
                --life; // ライフを一つ減らす
                SetLifeText(life);  // ライフをセットする
                if (life > 0)   // ライフが０より大きい?(Yes)
                {
                    GameObject newBall = Instantiate(ballPrefab);   // ボールを生成
                    newBall.name = ballPrefab.name; // 新しく生成したゲームオブジェクトの名前をプレハブの名前と同じにする
                }
                else
                {
                    life = 0;   // ライフを０にセット
                    textGameOver.enabled = true;    // ゲームオーバーテキストを表示
                    inGame = false; // ゲーム中フラグをfalseへ
                }
            }
        }
    }
}
