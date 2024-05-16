using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]Wave[] waves;
    [SerializeField] MonsterSpawner monsterSpawner;
    int currentWaveIndex = -1;
    //���̺� ���� ����� ���� get������Ƽ (������̺� ,�� ���̺�)
    public int CurrentWave => currentWaveIndex + 1; //������ 0�̱� ������ +1
    public int MaxWave => waves.Length;

    public void StartWave() //������ �����ϰ� ������ ������ ���� �����ϰԲ�
    {
        if(currentWaveIndex < waves.Length-1)
        {
            //�ε����� ������ -1�̱� ������ ���̺� �ε��� ������ ���ϸ��� ��
            currentWaveIndex++;
            //���� ���̺� ���� ����
            monsterSpawner.StartWave(waves[currentWaveIndex]);
        }
    }
}
[System.Serializable]
public struct Wave
{
    public int level; //������ ���� �� ��� ���
    public int maxMonsterCount; // �������� �� ���� ����
}
