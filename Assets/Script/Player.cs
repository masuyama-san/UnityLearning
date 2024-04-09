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
    [SerializeField, Header("無敵時間")]
    private float damageTime;
    [SerializeField, Header("点滅時間")]
    private float flashTime;

    private bool bJump;
    private Animator anime;
    private SpriteRenderer spriteRenderer;
    private Vector2 inputDirection;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookMoveDirec();
        HitFloor();
    }

    private void Move()
    {
        if(bJump) return;

        rigid.velocity = new Vector2(inputDirection.x * moveSpeed, rigid.velocity.y);
        anime.SetBool("Walk", inputDirection.x != 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Floor")
        // {
        //     bJump = false;

        //     anime.SetBool("Jump", bJump);
        // }

        if (collision.gameObject.tag == "Enemy")
        {
            HitEnemy(collision.gameObject);
        }

    }

    private void HitFloor()
    {
        //引数で指定したレイヤー名のレイヤーを取得する
        int layerMask = LayerMask.GetMask("Floor");
        //プレイヤーの位置から半分の位置（足元）
        Vector3 rayPos = transform.position - new Vector3(0.0f, transform.lossyScale.y / 2.0f);
        //プレイヤーオブジェクトの幅の位置
        Vector3 raySize = new Vector3(transform.lossyScale.x - 0.1f, 0.1f);
        //ray＝スクリプトで作成する当たり判定
        //プレイヤーが衝突するときの当たり判定を設定する
        RaycastHit2D rayHit = Physics2D.BoxCast(rayPos, raySize, 0.0f, Vector2.zero, 0.0f, layerMask);

        if (rayHit.transform == null)
        {
            bJump = true;
            anime.SetBool("Jump", bJump);
            return;
        }

        if (rayHit.transform.tag == "Floor" && bJump)
        {
            bJump = false;
            anime.SetBool("Jump", bJump);
        }

    }
    private void LookMoveDirec()
    {
        if (inputDirection.x > 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (inputDirection.x < 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }

    private void HitEnemy(GameObject enemy)
    {
        float halfScaleY = transform.lossyScale.y / 2.0f;
        float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;
        if (transform.position.y >= enemy.transform.position.y)
        {
            Destroy(enemy);
            rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
        else
        {
            enemy.GetComponent<Enemy>().PlayerDamage(this);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        Color color = spriteRenderer.color;
        for (int i = 0; i < damageTime; i++)
        {
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0.0f);

            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
        }
        spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void Dead()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
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

        // bJump = true;

        // anime.SetBool("Jump", bJump);
    }

    public void Damage(int damage)
    {
        hp = Math.Max(hp - damage, 0);
        Dead();
    }

    public int GetHP()
    {
        return hp;
    }
}
