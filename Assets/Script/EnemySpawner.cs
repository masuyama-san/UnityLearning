using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("敵オブジェクト")]
    private GameObject enemy;

    private Player player;
    private GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        enemyObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (player == null)
        {
            return;
        }

        Vector3 playerPos = player.transform.position;
        Vector3 cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector3 scale = enemy.transform.lossyScale;

        float distance = Vector2.Distance(transform.position, new Vector2(playerPos.x, transform.position.y));
        float spawnDis = Vector2.Distance(playerPos, new Vector2(cameraMaxPos.x + scale.x / 2.0f, playerPos.y));
        if (distance <= spawnDis && enemyObject == null)
        {
            enemyObject = Instantiate(enemy);
            enemyObject.transform.position = transform.position;
            transform.parent = enemyObject.transform;
        }
    }
}
