using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed;

    [SerializeField, Header("体力")]
    private int hp;

    private bool bJump;

    private Vector2 inputDirection;

    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        bJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Debug.Log(hp);
    }

    private void Move()
    {
        rigid.velocity = new Vector2(inputDirection.x * moveSpeed, rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            bJump = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            HitEnemy(collision.gameObject);
        }

    }

    private void HitEnemy(GameObject enemy)
    {
        float halfScaleY = transform.lossyScale.y / 2.0f;
        float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;
        if (transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f))
        {
            Destroy(enemy);
        }
        else
        {
            enemy.GetComponent<Enemy>().PlayerDamage(this);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || bJump)
        {
            return;
        }

        rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

        bJump = true;
    }

    public void Damage(int damage)
    {
        hp = Math.Max(hp - damage, 0);
    }
}
