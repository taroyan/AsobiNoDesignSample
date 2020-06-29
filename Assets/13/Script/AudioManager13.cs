using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager13 : MonoBehaviour
{
    private GameObject player;  // プレイヤーのゲームオブジェクト
    private AudioSource audioSE;    // オーディオソース
    public AudioClip sound01;   // オーディオクリップ
    private bool killSEflag = true; // プレイヤーが消えたときのSEフラグ

    void Start()
    {
        audioSE = gameObject.AddComponent<AudioSource>();   // 自身にAudioSourceをアタッチ
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");  // 「Player」のタグ名がついたオブジェクトを取得
        if (player == null && killSEflag == true)   // playerがなく、killSEflagがtrue?(Yes)
        {
            audioSE.PlayOneShot(sound01);   // sound01を再生
            killSEflag = false;             // killSEflagをfalseへ
        }
    }
}
