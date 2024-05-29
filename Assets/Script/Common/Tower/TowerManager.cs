using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerManager : MonoBehaviour
{
    [SerializeField]TowerSpawner towerSpawer;
    [SerializeField]TowerDataViewer towerDataViewer;
    [SerializeField] TowerAttackRange towerattackRange;

    //public TowerCon nowObj;
    //public Transform towerObjCircle, goalObjCircle;

    private Transform selectedTile;
    private Transform previousTile; // ������ ������ Ÿ��
    private Color originalColor; // ���� Ÿ���� ���� ����

    private void Update()
    {
        ClickMouse();
    }

    void ClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("TileField")) // ���� �� ���� -> ���� �� ��ư�� ������ ����
                {
                    Debug.Log("Ÿ�ϼ���");
                    towerDataViewer.ClosePanel();
                    towerattackRange.OffAttackRange();

                    // ���� Ÿ���� ���İ� ����
                    if (previousTile != null)
                    {
                        SpriteRenderer previousRenderer = previousTile.GetComponent<SpriteRenderer>();
                        if (previousRenderer != null)
                        {
                            previousRenderer.color = originalColor;
                        }
                    }

                    // ���� ������ Ÿ���� ���İ� ����
                    SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        originalColor = spriteRenderer.color; // ���� ���� ����
                        Color newColor = spriteRenderer.color;
                        newColor.a = 0.5f; // ���ϴ� ���İ� (0.0f ~ 1.0f)
                        spriteRenderer.color = newColor;
                    }

                    previousTile = hit.transform; // ���� Ÿ���� ���� Ÿ�Ϸ� ����
                    selectedTile = hit.transform; // ���õ� Ÿ�� ����

                }
                else if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Ÿ�� ����");
                    towerDataViewer.OpenPanel(hit.transform);

                }
            }
        }
    }
   public Transform GetSelectedTile()
    {
        return selectedTile;
    }
    public void BuildTower()
    {
        Transform selecteTile = GetSelectedTile();
        if(selecteTile != null)
        {
            towerSpawer.SpawnTower(selecteTile);
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
