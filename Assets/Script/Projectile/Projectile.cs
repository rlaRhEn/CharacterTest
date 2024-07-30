using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
   
    protected float damage;

    protected Transform target;
    protected MoveMent2D movement2D;

    public void Setup(Transform target,float damage)
    {
        movement2D = GetComponent<MoveMent2D>();
        this.target = target;
        this.damage = damage;
    }
  
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return; //���� �ƴ� ���� �ε�����
        if (collision.transform != target) return;  //���� target�� ���� �ƴ� ��

        //collision.GetComponent<MonsterCon>().OnDieMonster(); // �� ��� �Լ� ȣ��
        Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
        collision.GetComponent<MonsterCon>().TakeDamage(pos, damage);
        
        gameObject.SetActive(false);
    }
}
