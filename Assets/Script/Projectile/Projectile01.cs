using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile01 : Projectile
{
    private void Update()
    {
        if(target == null)
        {
            gameObject.SetActive(false);
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return; //���� �ƴ� ���� �ε�����
        if (collision.transform != target) return;  //���� target�� ���� �ƴ� ��

        //collision.GetComponent<MonsterCon>().OnDieMonster(); // �� ��� �Լ� ȣ��
        Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
        collision.GetComponent<MonsterCon>().TakeDamage(pos, damage);
        Invoke("OnDestroyProjectile01", 0.5f);
        
    }
    public void OnDestroyProjectile01()
    {
        gameObject.SetActive(false);
    }
}
