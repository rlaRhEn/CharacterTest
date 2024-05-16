using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerManager : MonoBehaviour
{
    [SerializeField]TowerSpawner towerSpawer;
    [SerializeField]TowerDataViewer towerDataViewer;

    public TowerCon nowObj;
    public Transform towerObjCircle, goalObjCircle;

    private void Update()
    {
        ClickMouse();
    }

    void ClickMouse()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.CompareTag("TileField"))
            {
                Debug.Log("Ÿ�ϼ���");
                towerSpawer.SpawnTower(hit.transform);
            }
            else if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Ÿ�� ����");
                towerDataViewer.OpenPanel(hit.transform);
            }
            else
            {

            }
        }
    }


    //void ClickMove() //ĳ���� ����
    //{
    //    if(Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        nowObj = null;
    //        goalObjCircle.transform.position = Vector2.zero;
    //        towerObjCircle.transform.position = Vector2.zero;
    //    }
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //        for (int i = 0; i < hits.Length; i++)
    //        {
    //            RaycastHit2D hit = hits[i];
    //            if (hit.collider != null)//���콺�� ������Ʈ�� ����� ��
    //            {
    //                Debug.Log(hit.collider.gameObject.name);
    //                if(hit.collider.CompareTag("Player")) // ���� ������Ʈ�� �÷��̾��
    //                {
    //                    nowObj = hit.collider.GetComponent<TowerCon>();
    //                }
    //                else 
    //                {
    //                    //Set move Player object to this point
    //                    if (nowObj != null && hit.collider.CompareTag("TileField")) //���� ������Ʈ�� �÷��̾�� ���� ������ �ٸ� �� Ŭ������ ��� TileField�������� �̵�����
    //                    {
    //                        Vector2 goalPos = hit.point;
    //                        goalObjCircle.transform.position = hit.point;
    //                        nowObj.SetMovePos(goalPos);
    //                    }

    //                }
    //            }
    //        }   
    //    }
    //    if(nowObj != null) //���� ������Ʈ�� �÷��̾�� ���� ���� ��
    //    {
    //        towerObjCircle.transform.position = nowObj.transform.position;
    //    }
    //}
}
