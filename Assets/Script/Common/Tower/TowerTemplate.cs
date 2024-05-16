using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject //Ÿ�� ���� ������
{
    public GameObject towerPrefab;
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        public float attack;
        public float attackIncrease; // ���ݷ�������
        public int cost; //���׷��̵� ���
        public int probability; //���׷��̵� ����Ȯ��
        public int fail;//���׷��̵� ����Ȯ��
        public int sell; //Ÿ�� �Ǹ� �� ȹ�� ���
    }
}
