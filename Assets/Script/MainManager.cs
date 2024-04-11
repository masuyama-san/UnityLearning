using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject gameOverUI;

    [SerializeField, Header("ゲームクリアUI")]
    private GameObject gameClearUI;

    private GameObject player;
    private bool bShowUI;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>().gameObject;
        bShowUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        if (player != null) return;

        gameOverUI.SetActive(true);
        bShowUI = true;
    }

    //プレイヤースクリプトで呼び出し
    public void ShowGameClearUI()
    {
        gameClearUI.SetActive(true);
        bShowUI = true;
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        if (!bShowUI || !context.performed) return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
