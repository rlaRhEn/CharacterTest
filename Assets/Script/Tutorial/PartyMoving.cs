using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMoving : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SPUM_Prefabs[] spum_Prefabs;
    RectTransform[] rectTransform;
    [SerializeField] float attackTimer, attackTerm = 3;

    public enum UnitState
    {
        idle,
        run,
        attack,
        skill,
        sturn,
        death

    }
    public UnitState _unitState = UnitState.idle;
    


    private void Awake()
    {
        spum_Prefabs = GetComponentsInChildren<SPUM_Prefabs>(); //�ڽ� ������ ��ũ��Ʈ �������� �ִϸ��̼� ��� �뵵
        rectTransform = GetComponentsInChildren<RectTransform>(); //�ڽ� ��ġ���� �������� 
        SetPartyList();
    }
    void Update()
    {
        //CheckState();
        PartyMove();
        //attackTimer += Time.deltaTime; // ���� �ִϸ��̼�
        //if (attackTimer >= attackTerm) //���� ���� �ִϸ��̼� �Ѱ��� �ϸ� �ٸ� �Ѱ� �۵� x
        //{
        //    PartyAniCheck("2_Attack_Normal");
        //    attackTimer = 0;
        //    Debug.Log("�ִϸ��̼� �۵�");
        //}
    }
    void SetPartyList()
    {
        
    }
    public void CheckState()
    {
        switch(_unitState)
        {
            case UnitState.idle:
                break;
            case UnitState.run:
                //PartyMove();
                break;
            case UnitState.attack:
                break;
            case UnitState.skill:
                break;
            case UnitState.sturn:
                break;
            case UnitState.death:
                break;
        }
    }
        public void PartyMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x > 0)
        {
            FlipX(true);
        }
        else if(x < 0) FlipX(false);
        if (x != 0 || y != 0)
        {
            Vector3 moveVelocity = new Vector3(x, y, 0) * speed * Time.deltaTime;
            this.transform.position += moveVelocity;
            PartyAniCheck("1_Run");
        }
        else if (x == 0 && y == 0) //���� ���� ���
        {
            PartyAniCheck("0_idle");
        }
    }
    public void PartyAniCheck(string aniName)//ĳ���� �ִϸ��̼� ����
    {
        for (int i = 0; i < transform.childCount; i++) // i= ��Ƽ�� ��
        {
            spum_Prefabs[i].PlayAnimation(aniName);
        }
    }
    public void FlipX(bool flipX) // ĳ���� ��,�� ���� �� ����ϸ� ���� �ʿ�
    {
        for(int i = 0; i < transform.childCount; i++)
        if(flipX)// x�� �����ϸ� ���������κ���
        {
            rectTransform[i].localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rectTransform[i].localScale = new Vector3(1, 1, 1);
        }
    }
}
