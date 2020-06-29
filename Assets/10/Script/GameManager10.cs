using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager10 : MonoBehaviour
{
    public int life;                        // ライフ
    public GameObject ballPrefab;           // ボールプレハブ
    public Text textGameOver;               // ゲームオーバーテキスト
    private int score;                      // スコア
    private float leftTime;                 // 残り時間
    private Text textScore;                 // スコアテキスト
    private Text textLife;                  // ライフテキスト
    private Text textTimer;                 // タイムテキスト
    private bool inGame;                    // ゲーム中かどうかフラグ
    public Text textClear;                  // クリアテキスト
    private static int highScore = 0;       // ハイスコア

    private Text textResult;                // リザルトテキスト
    private Text textResultBall;            // ボールリザルトテキスト
    private Text textResultTime;            // タイムリザルトテキスト
    private Text textResultTotal;           // トータルリザルトテキスト
    private Text textHighScore;             // ハイスコアテキスト

    private AudioSource audioSource;        // AudioSourceコンポーネント

    public AudioClip overSound;             // ゲームオーバーサウンド
    public AudioClip killSound;             // キルサウンド
    public AudioClip clearSound;            // クリアサウンド


    void Start()
    {
        life = 3;                               // ライフを３にセット
        textGameOver.enabled = false;           // ゲームオーバーテキストは非表示
        textClear.enabled = false;              // クリアテキストは非表示
        score = 0;                              // スコアはゼロ
        leftTime = 30f;                         // 残り時間は３０秒
        audioSource = gameObject.AddComponent<AudioSource>();                   // AudioSourceコンポーネントをアタッチ
        textScore = GameObject.Find("Score").GetComponent<Text>();              // ゲームオブジェクト名「Score」にアタッチされているTextコンポーネントを取得
        textLife = GameObject.Find("BallLife").GetComponent<Text>();            // ゲームオブジェクト名「BallLife」にアタッチされているTextコンポーネントを取得
        textTimer = GameObject.Find("TimeText").GetComponent<Text>();           // ゲームオブジェクト名「TimeText」にアタッチされているTextコンポーネントを取得

        textResult = GameObject.Find("Result Score").GetComponent<Text>();      // ゲームオブジェクト名「Result Score」にアタッチされているTextコンポーネントを取得
        textResultBall = GameObject.Find("Result Ball").GetComponent<Text>();   // ゲームオブジェクト名「Result Ball」にアタッチされているTextコンポーネントを取得
        textResultTime = GameObject.Find("Result Time").GetComponent<Text>();   // ゲームオブジェクト名「Result Time」にアタッチされているTextコンポーネントを取得
        textResultTotal = GameObject.Find("Result Total").GetComponent<Text>(); // ゲームオブジェクト名「Result Total」にアタッチされているTextコンポーネントを取得
        textHighScore = GameObject.Find("HighScore").GetComponent<Text>();      // ゲームオブジェクト名「HighScore」にアタッチされているTextコンポーネントを取得

        SetScoreText(score);            // スコアテキストをセットする
        SetLifeText(life);              // ライフテキストをセットする
        SetHighScoreText(highScore);    // ハイスコア表示
        inGame = true;                  // ゲーム中フラグをtrue
    }

    private void SetScoreText(int score)
    {
        textScore.text = "Score:" + score.ToString();   // スコアをセット
    }

    private void SetLifeText(int life)
    {
        textLife.text = "Ball:" + life.ToString();      // ライフをセット
    }

    private void SetHighScoreText(int highScore)
    {
        textHighScore.text = "High Score :" + highScore.ToString(); // ハイスコアをセット
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 現在読み込んでいるSceneをロード（＝シーンの再読み込み）
    }

    public void AddScore(int point)
    {
        if (inGame) // ゲーム中?(Yes)
        {
            score += point;         // ポイント(point)を加算
            SetScoreText(score);    // スコアをセットする
        }
    }

    void Update()
    {
        if (inGame)     // ゲーム中?(Yes)
        {
            // 制限時間に関する処理
            leftTime -= Time.deltaTime; // 前フレームとの時間経過（deltaTime）をleftTimeから引く
            textTimer.text = "Time:" + (leftTime > 0f ? leftTime.ToString("0.00") : "0.00");    // テキスト表示
            if (leftTime < 0f)  // 残り時間が０より小さい?(Yes)
            {
                audioSource.PlayOneShot(overSound); // ゲームオーバーサウンド再生
                textGameOver.enabled = true;        // ゲームオーバーテキスト表示
                inGame = false;                     // ゲーム中フラグをfalse
            }

            // ライフに関する処理
            GameObject ballObj = GameObject.Find("Ball");   // オブジェクト名が「Ball」のオブジェクトを取得
            if (ballObj == null)    // オブジェクトがない?(Yes)
            {
                --life;             // ライフを一つ減らす
                SetLifeText(life);  // ライフをセットする
                if (life > 0)       // ライフが０より大きい?(Yes)
                {
                    audioSource.PlayOneShot(killSound);             // キルサウンドを再生
                    GameObject newBall = Instantiate(ballPrefab);   // ボールを生成
                    newBall.name = ballPrefab.name;                 // 新しく生成したゲームオブジェクトの名前をプレハブの名前と同じにする
                }
                else
                {
                    life = 0;                       // ライフを０にセット
                    textGameOver.enabled = true;    // ゲームオーバーテキストを表示
                    inGame = false;                 // ゲーム中フラグをfalseへ
                }
            }

            // クリアに関する処理
            GameObject targetObj = GameObject.FindWithTag("Target");    // オブジェクト名が「Target」のオブジェクトを取得
            if (targetObj == null)  // オブジェクトがない?(Yes)
            {
                audioSource.PlayOneShot(clearSound);    // クリアサウンドを再生
                textClear.enabled = true;               // クリアテキストを表示

                // 結果表示関連
                int scorePoint = score * 50;    // スコア計算
                int scoreBall = life * 1000;    // スコア計算
                int scoreTime = (int) (leftTime * 100f);    // 少数値を整数型に直します(int) スコア計算
                textResult.text = "Score * 50 = " + scorePoint.ToString();  // 表示
                textResultBall.text = "Ball * 1000 = " + scoreBall.ToString();  // 表示
                textResultTime.text = "Time * 100 = " + scoreTime.ToString();  // 表示

                int totalScore = scorePoint + scoreBall + scoreTime;    // トータル計算
                textResultTotal.text = "Total Score :" + totalScore.ToString(); // 表示

                // ハイスコア更新処理
                if (highScore < totalScore) // ハイスコアを上回っていたら?(Yes)
                {
                    highScore = totalScore; // 更新
                }


                inGame = false; // ゲーム中フラグをオフ
            }
        }
    }

    /// <summary>
    /// ゲーム中かどうかのフラグの状態を返す関数
    /// </summary>
    /// <returns></returns>
    public bool IsInGame()
    {
        return inGame;
    }
}
