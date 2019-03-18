using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 startPos, endPos, direction;
    Rigidbody rb;
    Vector3 playerPos;
    public float speed = 1f;
    // 地面に接触しているか否か
    bool ground;
    // ジャンプの強さ
    //public float thrust = 500.0f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Playerの現在より少し前の位置を保存
        playerPos = transform.position;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ground)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        break;
                    case TouchPhase.Moved:
                        direction = startPos - touch.position;

                        PlayerMove();

                        break;

                    case TouchPhase.Stationary:
                        direction = startPos - touch.position;

                        PlayerMove();

                        break;

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
        // ジャンプのアニメーションをオフにする
        //animator.SetBool("Jumping", false);
    }

    // Groundから離れると作動
    void OnCollisionExit(Collision col)
    {
        ground = false;
        // ジャンプのアニメーションをオンにする
        //animator.SetBool("Jumping", true);
    }

    // プレイヤーの移動
    void PlayerMove()
    {
        speed = 3f;
        // 斜め移動用の角度を代入
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

        // スペースキーでジャンプ
        //if (Input.GetButton("Jump"))
        //{
        //    // thrustの分だけ上方に力がかかる
        //    rb.AddForce(transform.up * thrust);
        //    // 速度が出ていたら前方と上方に力がかかる
        //    if (rb.velocity.magnitude > 0)
        //        rb.AddForce(transform.forward * thrust + transform.up * thrust);
        //}
    }
}
