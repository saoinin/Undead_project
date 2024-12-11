using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;

    WaitForFixedUpdate wait;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();

    }

    private void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        Vector2 dirVec = target.position - rigid.position;//방향
        Vector2 nextVec = dirVec.normalized * speed* Time.fixedDeltaTime;//프레임과 무관하게
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }
        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()//활성화될때 
    {
        target=GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

    }
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
        {
            return;
        }
        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        if (health > 0)
        {
            anim.SetTrigger("Hit");
            

        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;// 콜라이더랑 리지드 바디 없
            Dead();
        }
    }
    // 코루틴
    IEnumerator KnockBack()
    {
        yield return wait;// 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;//플레이어 반대 방향
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
