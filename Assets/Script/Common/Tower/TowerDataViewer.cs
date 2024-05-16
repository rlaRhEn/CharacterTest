using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDataViewer : MonoBehaviour
{
    //[SerializeField] Text textInfo;
    [SerializeField] Text textLevel; //����
    [SerializeField] Text textAttack; //���ݷ�
    [SerializeField] Text textUpgrade;// ��ȭ�з�
    [SerializeField] Text textAttackIncrease; // ������ ���� ���ݷ� ��·�
    [SerializeField] Text textCost; //���׷��̵� ���

    [SerializeField] Button buttonUpgrade;

    [SerializeField] TowerAttackRange towerAttackRange;
    TowerCon currentTower;


    private void Start()
    {
        ClosePanel();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    public void OpenPanel(Transform towerCon)
    {
        currentTower = towerCon.GetComponent<TowerCon>();
        //Ÿ������ Panel On
        gameObject.SetActive(true);
        UpdateTowerData();
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    void UpdateTowerData()
    {
        //textInfo.text =
        textLevel.text = currentTower.Level.ToString();
        textAttack.text = currentTower.Attack.ToString();
        textUpgrade.text = currentTower.Level + " -> " + (currentTower.Level+1) + "\n ����Ȯ��: " + currentTower.Probablilty + "%" + "\n����(����): " + currentTower.Fail + "%"; 
        textAttackIncrease.text = currentTower.AttackIncrease.ToString();
        textCost.text = currentTower.Cost.ToString();

        //���׷��̵尡 �Ұ��������� ��ư ��Ȱ��ȭ
        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
    }
    public void OnClickEventTowerUpgrade()
    {
        //Ÿ�� ���׷��̵� �õ� (����: true , ����: false)
        bool isSuccess = currentTower.Upgrade();

        if(isSuccess == true)
        {
            //Ÿ���� ���׷��̵� ���� �� Ÿ�� ���� ����
            UpdateTowerData();
        }
    }
    public void OnClickEventTowerSell()
    {
        //Ÿ�� �Ǹ�
        currentTower.Sell();
        //������ Ÿ���� �����
        ClosePanel();
    }
}
