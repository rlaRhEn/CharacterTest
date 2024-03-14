using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;


public class MonsterManager : MonoBehaviour
{
    [SerializeField] int monsterKey, hp;
    [SerializeField] float attack, armor, attackSpeed;

    private void Start()
    {
        UnityGoogleSheet.Load<GameBalance.Monster>(); //���� ������ �ε�
        //foreach (var value in GameBalance.Monster.MonsterList) // ���ӹ뷱��Job��Ʈ ������ �ҷ�����
        //{
        //    //Debug.Log($"Loaded {value.key} {value.attack} {value.armor}"); // Ű ���� �Ƹ�
        //}
        hp = GameBalance.Monster.MonsterMap[monsterKey].hp;
        attack = GameBalance.Monster.MonsterMap[monsterKey].attack;
        armor = GameBalance.Monster.MonsterMap[monsterKey].armor;
        attackSpeed = GameBalance.Monster.MonsterMap[monsterKey].attackSpeed;
    }
    public void DownHp()
    {
        hp -= 10;
        Debug.Log(hp);
    }
}
