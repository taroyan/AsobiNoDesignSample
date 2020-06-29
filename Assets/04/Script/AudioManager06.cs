using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager06 : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip sound01;
    public AudioClip sound02;
    public AudioClip sound03;

    void Start()
    {
        audio = gameObject.AddComponent<AudioSource>(); // AudioSourceコンポーネントをこのゲームオブジェクトにアタッチする
    }

    /// <summary>
    /// 何かが衝突したときに呼ばれる関数
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")   // 衝突したゲームオブジェクトのタグ名が「Player」?(Yes)
        {
            audio.PlayOneShot(sound01); // sound01を鳴らす
        }
        else if (other.gameObject.tag == "Target")  // 衝突したゲームオブジェクトのタグ名が「Target」?(Yes)
        {
            audio.PlayOneShot(sound02); // sound02を鳴らす
        }
        else
        {
            audio.PlayOneShot(sound03); // sound03を鳴らす
        }
    }
}
