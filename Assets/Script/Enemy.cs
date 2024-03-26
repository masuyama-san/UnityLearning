using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    [SerializeField, Header("攻撃力")]
    private int attackPower;

    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        rigid.velocity = new Vector2(Vector2.left.x * moveSpeed, rigid.velocity.y);
    }

    public void PlayerDamage(Player player)
    {
        player.Damage(attackPower);
    }
}
