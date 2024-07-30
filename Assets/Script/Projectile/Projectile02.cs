using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile02 : Projectile
{
    private void Update()
    {
        if (target != null)//Ÿ���� ������
        {
            //Ÿ���� ���� ����
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
