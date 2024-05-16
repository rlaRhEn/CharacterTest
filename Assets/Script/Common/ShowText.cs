using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    [SerializeField] float limitTime;

    public Text timeText, goldText, waveText,monsterCountText;

    [SerializeField] PlayerGold playerGold;
    [SerializeField] WaveSystem waveSystem;
    [SerializeField] MonsterSpawner monsterSpawner;
  
    private void Update()
    {
        CountDown();
        ShowGold();
        ShowMonsterCount();
        ShowWave();
    }
    public void CountDown()
    {
        limitTime -= Time.deltaTime;
        timeText.text = "���� �ð�: " + Mathf.Round(limitTime);
        if (limitTime <= 0) //�ʵ忡 ���Ͱ� �����ִٸ� �����й�
        {
            //GameOver();
        }
    }
    public void RoundClear()
    {
        limitTime = 90;
    }
    void ShowGold()
    {
        goldText.text = "Gold: " + playerGold.CurrentGold.ToString();
    }
    void ShowMonsterCount()
    {
        monsterCountText.text = "�����ִ� ��: " +monsterSpawner.currentEnemyCount.ToString();
    }
    void ShowWave()
    {
        waveText.text = "Wave: "+ waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
}
