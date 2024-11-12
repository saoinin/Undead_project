using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive=true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }
        Vector2 dirVec = target.position - rigid.position;//����
        Vector2 nextVec = dirVec.normalized * speed* Time.fixedDeltaTime;//�����Ӱ� �����ϰ�
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
}
