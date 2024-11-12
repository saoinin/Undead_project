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
        //inputVec.x = Input.GetAxis("Horizontal"); //GetAxisRaw를 하면 딱딱하게 움직일것이다.
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()// 물리엔진 관련은 이 클래스에서
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition (rigid.position + nextVec);

    }

    private void LateUpdate()// 업데이트 이후 다음 프레임 전에 실행
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;//적을 때 true 라는 뜻
        }
    }
}
