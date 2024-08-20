using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //������ ������ ���� 2��������
    public GameObject[] monsterPrefabs;
    public GameObject[] projectilePrefabs;
   

    //Ǯ ����� �ϴ� ����Ʈ�� 2���ʿ�
    public List<GameObject>[] monsterPools;
    List<GameObject>[] projectilePools;

    public List<GameObject>[] MonsterPools => monsterPools;
    private void Awake()
    {
        monsterPools = new List<GameObject>[monsterPrefabs.Length];
        //���� �ڵ�����
        for (int index = 0; index < monsterPools.Length; index++)
        {
            monsterPools[index] = new List<GameObject>();
        }

        projectilePools = new List<GameObject>[projectilePrefabs.Length];
        //�߻�ü �ڵ�����
        for (int index = 0; index < projectilePools.Length; index++)
        {
            projectilePools[index] = new List<GameObject>();
        }

    }
    public GameObject GetMonster(int index)//���ӿ�����Ʈ ��ȯ �Լ�
    {
        GameObject select = null;

        //������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����
        //�߰��ϸ� select ������ �Ҵ�
        foreach (GameObject item in monsterPools[index]) // �迭�̳� ����Ʈ�� �������� �ݺ���
        {
            if (item != null)
            {
                if (!item.activeSelf) // activeself ������Ʈ�� ��Ȱ��ȭ(������)���� Ȯ��
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
        }
        //�� ã������ ���Ӱ� �����Ͽ� select ������ �Ҵ� 
        if (!select) //���Ӱ� �����ϰ� select ������ �Ҵ�
        {
            select = Instantiate(monsterPrefabs[index], transform);
            monsterPools[index].Add(select);
        }
        return select;
    }

    public GameObject GetProJectile(int index)//���ӿ�����Ʈ ��ȯ �Լ�
    {
        GameObject select = null;

        //������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ����
        //�߰��ϸ� select ������ �Ҵ�
        foreach (GameObject item in projectilePools[index]) // �迭�̳� ����Ʈ�� �������� �ݺ���
        {
            if (item != null)
            {
                if (!item.activeSelf) // activeself ������Ʈ�� ��Ȱ��ȭ(������)���� Ȯ��
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
        }
        //�� ã������ ���Ӱ� �����Ͽ� select ������ �Ҵ� 
        if (!select) //���Ӱ� �����ϰ� select ������ �Ҵ�
        {
            select = Instantiate(projectilePrefabs[index], transform);
            projectilePools[index].Add(select);
        }
        return select;
    }
    public void OnDie(GameObject obj)
    {
        obj.SetActive(false);
    }
}
