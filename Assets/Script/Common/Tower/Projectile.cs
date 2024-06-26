using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    MoveMent2D movement2D;
    Transform target;
    float damage;
    float speed = 5;
    [SerializeField] Transform head;


    public void Setup(Transform target,float damage)
    {
        movement2D = GetComponent<MoveMent2D>();
        this.target = target;
        this.damage = damage;
    }
    private void Update()
    {
        if(target != null)//Ÿ���� ������
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
        Vector3 direction = (target.position -  transform.position).normalized;
        movement2D.MoveTo(direction);
        // ȭ���� ȸ��
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)) ;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return; //���� �ƴ� ���� �ε�����
        if (collision.transform != target) return;  //���� target�� ���� �ƴ� ��

        //collision.GetComponent<MonsterCon>().OnDieMonster(); // �� ��� �Լ� ȣ��
        Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
        collision.GetComponent<MonsterCon>().TakeDamage(pos, damage);
        
        gameObject.SetActive(false);
    }
}
