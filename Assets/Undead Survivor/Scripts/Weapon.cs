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
                break;

        }
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
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

            bullet.GetComponent<Bullet>().Init(damage, -1);//-1 is infinity per.
        }
    }
}
