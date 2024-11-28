using NUnit.Framework;
using System.Collections.Generic; // 추가 필요
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 필요한거 프리팹을 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당을 하는 리스트들 위랑 1대1 관계
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index]= new List<GameObject>();//풀리스트를 초기화

            Debug.Log(pools.Length);
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //선택한 풀의 놀고(비활성화된) 있는 게임 오브젝트 접근
        foreach(GameObject item in pools[index]){
            if (!item.activeSelf)// 비활성화 라면
            {
                //발견하면  select 변수에 할당
                select = item;
                select.SetActive(true);
                break;// 하나 찾았으면 됐어
            }
        }
        //만약에 모두 쓰고 있다 하면(못 찾았으면?)
        if (!select)//select==null
        {
            //새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);// 풀에다 등록
         }
        return select;
    }
}
