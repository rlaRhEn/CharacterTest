using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Image image;
    public static Vector2 DefaultPos;
    public Image partyImg;
    Vector2 partyPos;

    Color originalColor;

    void Awake()
    {
        image = GetComponent<Image>();// �̹��� ������Ʈ������
        partyPos = new Vector2(partyImg.transform.position.x, partyImg.transform.position.y);
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        DefaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
        // ȸ������ �����ϰ��� �ϴ� alpha �� (0: ���� ����, 1: ���� ������)

        // ���ο� ������ ���� alpha ���� �����մϴ�.
        //Color greyedOutColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        // ���ο� ������ �̹����� �����մϴ�.
        image.color = Color.gray;

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Color originColor = new Color(1,1,1, 1);
        image.color = originColor;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(this.transform.position, partyPos) < 10)
        {
            this.transform.position = partyPos;
        }

        else { this.transform.position = DefaultPos; }

    }
}
//��ó: https://krapoi.tistory.com/entry/Unity-����-����-�巡��-��-��� [�ڵ� �۾���:Ƽ���丮]
