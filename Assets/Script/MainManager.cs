using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject gameOverUI;

    [SerializeField, Header("ゲームクリアUI")]
    private GameObject gameClearUI;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        if (player != null)
        {
            return;
        }
        gameOverUI.SetActive(true);
    }

    //プレイヤースクリプトで呼び出し
    public void ShowGameClearUI()
    {
        gameClearUI.SetActive(true);
    }
}
