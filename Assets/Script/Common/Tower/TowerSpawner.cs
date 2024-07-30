using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] towerPrefab;

    [SerializeField] int towerBuildGold = 50;
    [SerializeField] PlayerGold playerGold; 

    public void SpawnTower(Transform tileTransform)
    {
        int randomTower;
        randomTower = Random.Range(0, towerPrefab.Length); //����
        Debug.Log(randomTower);
        //Ÿ�� �Ǽ���ŭ�� �� ������ �Ǽ� x
        if(towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();
        //���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� x
        if(tile.IsBuildTower ==true)
        {
            return;
        }
        //Ÿ���� �Ǽ��Ǿ� �������� ����
        tile.IsBuildTower = true;
        //Ÿ�� �Ǽ��� �ʿ��� ��常ŭ ����
        playerGold.CurrentGold -= towerBuildGold;
        //������ Ÿ���� ��ġ�� Ÿ���Ǽ�(Ÿ�� ���� z�� -1�� ��ġ�� ��ġ)
        Vector3 position = tileTransform.position + Vector3.back;
        GameObject tower =  Instantiate(towerPrefab[randomTower], position, Quaternion.identity);
        tower.GetComponent<TowerCon>().SetUp(tile);
    }
}
