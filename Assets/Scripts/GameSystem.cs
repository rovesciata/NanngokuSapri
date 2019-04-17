using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // スタートボタンを押したらゲーム画面に移動
    public void GameStart()
    {
        SceneManager.LoadScene("Main");
    }
}
