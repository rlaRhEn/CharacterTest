using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile00 : Projectile
{
    [SerializeField] Transform head;
    private void Update()
    {
        if (target != null)//Ÿ���� ������
        {
            //Ÿ���� ���� ����
            //Vector3 direction = (target.position - transform.position).normalized;

            RotateToTarget();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    private void RotateToTarget() //����
    {
        // Ÿ�� ���� ���
        Vector3 direction = (target.position - transform.position).normalized;
        movement2D.MoveTo(direction);
        // ȭ���� ȸ��
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
