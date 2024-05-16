using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackRange : MonoBehaviour
{
    private void Awake()
    {
        OffAttackRange();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffAttackRange();
        }
    }
    public void OnAttackRange(Vector3 position, float range)
    {
        gameObject.SetActive(true);

        //���ݹ��� ũ��
        float diameter = range * 2.0f; //�� ������ ��Ÿ�����ؼ� *2
        transform.localScale = Vector3.one * diameter;
        //���� ���� ��ġ
        transform.position = position;
    }
    public void OffAttackRange()
    {
        gameObject.SetActive(false);
    }
}
