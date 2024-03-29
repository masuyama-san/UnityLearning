using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField, Header("振動する時間")]
    private float shakeTime;
    [SerializeField, Header("振動の大きさ")]
    private float shakeMagnitude;

    private Player player;
    private float shakeCount;
    private int currentPlayerHP;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        currentPlayerHP = player.GetHP();
    }

    // Update is called once per frame
    void Update()
    {
        ShakeCheck();
    }

    private void ShakeCheck()
    {
        if (currentPlayerHP != player.GetHP())
        {
            currentPlayerHP = player.GetHP();
            shakeCount = 0.0f;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Vector3 initPos = transform.position;

        while (shakeCount < shakeTime)
        {
            float x = initPos.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = initPos.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = new Vector3(x, y, initPos.z);

            shakeCount += Time.deltaTime;

            yield return null;
        }
        transform.position = initPos;
    }
}
