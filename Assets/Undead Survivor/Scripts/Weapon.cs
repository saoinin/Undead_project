using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;


    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;

        }
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
        {
            Arange();
        }
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Arange();
                break;
            default:
                speed = 0.3f;
                break;

        }
    }
    void Arange()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            if (i < transform.childCount) {
                bullet= transform.GetChild(i);// 기존 오브젝트 먼저 활용
            }
            else
            {
               bullet = GameManager.instance.pool.Get(prefabId).transform; //모자란것은 풀링에서 가져오기
                bullet.parent = transform;// 새로만들 불렛의 부모가 내 자신 weapon 0 폴더 이게 됨
            }

            
            
            
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;//위치와 회전 초기화

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);//-1 is infinity per.
        }
    }
    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;// 크기가 포함됨
        dir= dir.normalized;// 크기를 제거함 방향 

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);// 방향
        bullet.GetComponent<Bullet>().Init(damage, count, dir);//count는 관통력
    }
}
