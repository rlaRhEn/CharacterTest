using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UGS;

public class ShowText : MonoBehaviour
{
    [SerializeField] float limitTime;

    public Text timeText, goldText, waveText,monsterCountText;
    public Text hpText, armorText, nameText;
    [SerializeField] Slider limitMonsterSliderBar;


    [SerializeField] Font customFont;


    [SerializeField] PlayerGold playerGold;
    [SerializeField] WaveSystem waveSystem;
    [SerializeField] MonsterSpawner monsterSpawner;

    int maxCount;

    private void SetAllTextFont() //�ؽ�Ʈ ��Ʈ����
    {
        Text[] allTextComponents = { timeText, goldText, waveText, monsterCountText, hpText, armorText, nameText };

        foreach(Text textComponents in allTextComponents)
        {
            if(textComponents != null)
            {
                textComponents.font = customFont;
            }
        }
    }
    private void Start()
    {
        StartCoroutine(Initialize());
        SetAllTextFont();
        maxCount = 100;
    }
    private IEnumerator Initialize()
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        
    }
    private void Update()
    {
        CountDown();
        ShowGold();
        ShowMonsterCount();
        ShowWave();
        ShowMonsterInfo();
    }
    void ShowMonsterInfo()
    {
        var data = GameManager.instance.GetMonsterData(waveSystem.CurrentWave-1);
        if(data != null)
        {
            hpText.text = "Hp: " + data.hp;
            armorText.text = "Armor: " + data.armor;
            nameText.text =  data.name;
        }
    }
    public void CountDown()
    {
        limitTime -= Time.deltaTime;
        timeText.text = "���� �ð� " + Mathf.Round(limitTime);
        if (limitTime <= 0) //�ð� �ʰ� ��
        {
            GameManager.instance.GameOver();
        }
    }
    public void RoundClear()
    {
        limitTime += 90;
    }
    void ShowGold()
    {
        goldText.text = "Gold: " + playerGold.CurrentGold.ToString();
    }
    void ShowMonsterCount()
    {
        limitMonsterSliderBar.value = (float)monsterSpawner.currentEnemyCount/maxCount;
        monsterCountText.text = monsterSpawner.currentEnemyCount.ToString() + "/" + maxCount.ToString(); //���� ���̺� x ����
    }
    void ShowWave()
    {
        waveText.text = "WAVE "+ waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
   
}
