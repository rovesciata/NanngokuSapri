using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // タッチ開始位置、離した位置、方向を定義
    private Vector2 startPos, endPos, direction;
    // Rigidbodyを変数に入れる
    Rigidbody rb;
    // プレイヤーの位置
    Vector3 playerPos;
    // プレイヤーのスピード
    public float speed = 1f;
    // 地面に接触しているか否か
    bool ground;
    // Animatorを変数に入れる
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        // Playerの現在より少し前の位置を保存
        playerPos = transform.position;
        // プレイヤーのAnimatorにアクセスする
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 地面に接触していると作動する
        if (ground)
        {
            // タッチした場合
            if (Input.touchCount > 0)
            {
                // タッチ開始
                Touch touch = Input.GetTouch(0);

                // タッチの段階により動作を変更
                switch (touch.phase)
                {
                    // タッチ開始時
                    case TouchPhase.Began:
                        // タッチ開始位置を取得
                        startPos = touch.position;

                        break;
                        // 指を動かしている時

                    case TouchPhase.Moved:
                        // タッチ開示時の位置と現在の指の位置の差分から方向を取得
                        direction = startPos - touch.position;
                        // プレイヤー移動
                        PlayerMove();

                        break;

                        // 指を押したままの時
                    case TouchPhase.Stationary:
                        // タッチ開示時の位置と現在の指の位置の差分から方向を取得
                        direction = startPos - touch.position;
                        // プレイヤー移動
                        PlayerMove();

                        break;

                        // 指を離した時
                    case TouchPhase.Ended:
                        // ベクトルの長さがない=移動していない時は走るアニメーションはオフ
                        animator.SetBool("Walking", false);
                        
                        break;
                }
            }
        }
    }


    // Groundの触れている間作動
    void OnCollisionStay(Collision col)
    {
        ground = true;
    }

    // Groundから離れると作動
    void OnCollisionExit(Collision col)
    {
        ground = false;
    }

    // プレイヤーの移動
    void PlayerMove()
    {
        speed = 3f;
        // ８方向の角度を代入
        float radian = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        if (radian < 0)
        {
            radian += 360; // マイナスのものに360を加算
        }
        // 方向判定
        if (radian <= 22.5f || radian > 337.5f)
        {
            // 右向きに移動
            float x = 1 * Time.deltaTime * speed;
            rb.MovePosition(transform.position + new Vector3(x, 0, 0));
        }
        else if (radian < 67.5f && radian > 22.5f)
        {
            // 右上に移動
            float x = 1 * Time.deltaTime * speed;
            float z = 1 * Time.deltaTime * speed;
            rb.MovePosition(transform.position + new Vector3(x, 0, z));
        }
        else if (radian <= 112.5f && radian > 67.5f)
        {
            // 上向きに移動
            float z = 1 * Time.deltaTime * speed;
            rb.MovePosition(transform.position + new Vector3(0, 0, z));
        }
        else if (radian <= 157.5f && radian > 112.5f)
        {
            // 左上に移動
            float x = -1 * Time.deltaTime * speed;
            float z = 1 * Time.deltaTime * speed;
            rb.MovePosition(transform.position + new Vector3(x, 0, z));
        }
        else if (radian <= 202.5f && radian > 157.5f)
        {
            // 左向きに移動
            float x = -1 * Time.deltaTime * speed;
            rb.MovePosition(transform.position + new Vector3(x, 0, 0));
        }
        else if (radian <= 247.5f && radian > 202.5f)
        {
            // 左下に移動
            float x = -1 * Time.deltaTime * speed;
            float z = -1 * Time.deltaTime * speed;
            rb.MovePosition(transform.position + new Vector3(x, 0, z));
        }
        else if (radian <= 292.5f && radian > 247.5f)
        {
            // 下向きに移動
            float z = -1 * Time.deltaTime * speed;
            rb.MovePosition(transform.position + new Vector3(0, 0, z));
        }
        else if (radian <= 337.5f && radian > 292.5f)
        {
            // 右下に移動
            float x = 1 * Time.deltaTime * speed;
            float z = -1 * Time.deltaTime * speed;
            rb.MovePosition(transform.position + new Vector3(x, 0, z));
        }

        // 移動距離が少しでもあった場合に方向転換
        if (direction.magnitude > 0.01f)
        {
            // directionのx軸とz軸の方向を向かせる
            transform.rotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.y));
            animator.SetBool("Walking", true);
        }

        // Playerの位置を更新する
        playerPos = transform.position;
    }

    // 風船に当ると上に飛んで行く
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Balloon")
        {
            // プレイヤーが上に移動
            playerPos.y = playerPos.y + 0.01f;
            // プレイヤーが左右に揺れる
            rb.MovePosition(new Vector3(playerPos.x + Mathf.PingPong(Time.time, 1), playerPos.y, playerPos.z));
        }
    }
}
