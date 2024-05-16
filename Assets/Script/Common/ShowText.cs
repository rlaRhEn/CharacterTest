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
        timeText.text = "남은 시간: " + Mathf.Round(limitTime);
        if (limitTime <= 0) //필드에 몬스터가 남아있다면 게임패배
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
        monsterCountText.text = "남아있는 적: " +monsterSpawner.currentEnemyCount.ToString();
    }
    void ShowWave()
    {
        waveText.text = "Wave: "+ waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
}
