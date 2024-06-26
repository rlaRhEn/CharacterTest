using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  AttackTest : MonoBehaviour
{
    protected SPUM_Prefabs spum_prefabs;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject testMonster;


    [SerializeField] private float attackTimer;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackSpeed = 0.8f;
    
    


    public enum Unit_State
    {
        idle,
        attack
    }
    public Unit_State unit_State = Unit_State.idle;

    void CheckState() // �� ������ �� �Լ� ����
    {
        switch (unit_State)
        {
            case Unit_State.idle:
                FindMonster();
                break;
            case Unit_State.attack:
                CheckAttack();
                break;
        }
    }
    void SetState(Unit_State state) //�ٸ� �Լ����� setState �ҷ��� �ִϸ��̼� �ൿ
    {
        unit_State = state;
        switch(unit_State)
        {
            case Unit_State.idle:
               
                spum_prefabs.PlayAnimation("0_idle");
                break;
            case Unit_State.attack:
                spum_prefabs.PlayAnimation("0_idle");
               
                break;
        }
    }
    private void Awake()
    {
        spum_prefabs = GetComponent<SPUM_Prefabs>();
    }
    private void Update()
    {
        CheckState();
    }

    public void FindMonster()
    {
        float closestDisSqr = Mathf.Infinity;
        Transform closestTarget = null;
        //��Ÿ� �ȿ� ������ ĳ���� ���� ���
        float distance = Vector2.Distance(testMonster.transform.position, transform.position);
        if(distance <= attackRange && distance <= closestDisSqr)
        {
            closestDisSqr = distance;
            closestTarget = testMonster.transform;
            SetState(Unit_State.attack);
            return;
        }

    }
    public void CheckAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed)
        {
            DoAttack(); //�ִϸ��̼�
            attackTimer = 0;
        }
    }
    public virtual void DoAttack() //���� �ִϸ��̼� attackNormal ���� 3�ʵ� idle 3�� �� attackNormal �ٽ� ���� ����ϴµ� �� 6�� ;
    { 
        //spum_prefabs.PlayAnimation("2_Attack_Normal");
        //spum_prefabs.PlayAnimation("0_idle");
        //SetProjectile();
    }
    public virtual void SetProjectile() //���� ����
    {

    }
   
}
