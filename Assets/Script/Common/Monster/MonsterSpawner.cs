using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public int monsterCount, wave;
    [SerializeField]float spawnTime = 3;
    [SerializeField] Transform startPos;
    [SerializeField]ShowText showText;

    public List<int> monsterList;
    public int currentEnemyCount; //���� ���̺꿡 �����ִ� �� ���� x -> ���� �ʵ��� ���� ���� �� 
    Wave currentWave;

    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxMonsterCount;
    public void StartWave(Wave wave)
    {
        currentWave = wave;
        //���� ���̺��� �ִ� �� ���ڸ� ����

        //currentEnemyCount += currentWave.maxMonsterCount;
        StartCoroutine("SpawnMonster");
    }
    IEnumerator SpawnMonster()
    {
        showText.RoundClear();
        monsterCount = 0;
        while(monsterCount < currentWave.maxMonsterCount)
        {
            GameObject monster = GameManager.instance.pool.GetMonster(currentWave.level);
            monsterCount++;
            monsterList.Add(monsterCount);
            yield return new WaitForSeconds(spawnTime);
        }
    }
    private void Update()
    {
        currentEnemyCount = monsterList.Count;
        if (currentEnemyCount > 100) //�� �ʰ� ��
        {
            GameManager.instance.GameOver();
        }
    }
}
