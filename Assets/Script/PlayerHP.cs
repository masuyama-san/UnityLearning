using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]
    private GameObject playerIcon;

    private Player player;

    private int beforeHP;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        beforeHP = player.GetHP();
        CreateHPIcon();
    }

    // Update is called once per frame
    void Update()
    {
        ShowHPIcon();
    }

    private void CreateHPIcon()
    {
        for (int i = 0; i < player.GetHP(); i++)
        {
            GameObject playerHPObject = Instantiate(playerIcon);
            playerHPObject.transform.SetParent(transform, false);
        }
    }

    private void ShowHPIcon()
    {
        if (beforeHP == player.GetHP())
        {
            return;
        }

        UnityEngine.UI.Image[] icons = transform.GetComponentsInChildren<UnityEngine.UI.Image>();
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
        }
        beforeHP = player.GetHP();
    }
}
