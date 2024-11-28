using NUnit.Framework;
using System.Collections.Generic; // �߰� �ʿ�
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �ʿ��Ѱ� �������� ������ ����
    public GameObject[] prefabs;

    // Ǯ ����� �ϴ� ����Ʈ�� ���� 1��1 ����
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index]= new List<GameObject>();//Ǯ����Ʈ�� �ʱ�ȭ

            Debug.Log(pools.Length);
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //������ Ǯ�� ���(��Ȱ��ȭ��) �ִ� ���� ������Ʈ ����
        foreach(GameObject item in pools[index]){
            if (!item.activeSelf)// ��Ȱ��ȭ ���
            {
                //�߰��ϸ�  select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;// �ϳ� ã������ �ƾ�
            }
        }
        //���࿡ ��� ���� �ִ� �ϸ�(�� ã������?)
        if (!select)//select==null
        {
            //���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);// Ǯ���� ���
         }
        return select;
    }
}
