using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // スタートボタンを押したら実行する
    public void GameStart()
    {
        SceneManager.LoadScene("Main");

    }
}
