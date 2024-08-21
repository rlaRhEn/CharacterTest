using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile02 : Projectile
{
    private void OnEnable()
    {
        
    }
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
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return; //���� �ƴ� ���� �ε�����
        if (collision.transform != target) return;  //���� target�� ���� �ƴ� ��

        //collision.GetComponent<MonsterCon>().OnDieMonster(); // �� ��� �Լ� ȣ��
        Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
        collision.GetComponent<MonsterCon>().TakeDamage(pos, damage);
        gameObject.SetActive(false);
    }
    public void OnDestroyProjectile02()
    {
        gameObject.SetActive(false);
    }
}
