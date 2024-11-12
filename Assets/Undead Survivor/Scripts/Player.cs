using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        //inputVec.x = Input.GetAxis("Horizontal"); //GetAxisRaw�� �ϸ� �����ϰ� �����ϰ��̴�.
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()// �������� ������ �� Ŭ��������
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition (rigid.position + nextVec);

    }

    private void LateUpdate()// ������Ʈ ���� ���� ������ ���� ����
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;//���� �� true ��� ��
        }
    }
}
