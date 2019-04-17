using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    // Rigidbodyを入れる変数
    private Rigidbody rb;
    // 風船の位置を入れる変数
    private Vector3 balloonPos;
    // プレイヤーを入れる変数
    public GameObject player;
    // プレイヤーのRigidbodyを入れる変数
    private Rigidbody playerRb;
    // プレイヤーの位置を入れる変数
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        // 風船の位置を取得
        balloonPos = transform.position;
        // プレイヤーを取得
        player = GameObject.Find("Player");
        // プレイヤーのRigidbodyを取得
        playerRb = player.GetComponent<Rigidbody>();
        // プレイヤーの位置を取得
        playerPos = player.transform.position;
    }

    // プレイヤーが当ると一緒に飛んで行く
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // 風船が上に飛んで行く
            balloonPos.y = balloonPos.y + 0.01f;
            // 風船が左右に揺れる
            rb.MovePosition(new Vector3(balloonPos.x + Mathf.PingPong(Time.time, 1), balloonPos.y, balloonPos.z));
            // 風船の位置をプレイヤーの位置と一緒にする
            playerPos = balloonPos;
        }
    }
}
