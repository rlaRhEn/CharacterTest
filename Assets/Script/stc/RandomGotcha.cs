using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGotcha : MonoBehaviour
{
    // 85% Ȯ���� ������ ��ȯ�ϴ� �Լ�
    public bool TrySuccess()
    {
        float randomValue = Random.value; // 0.0���� 1.0 ������ ������ ����
        Debug.Log(randomValue);
        return randomValue < 0.5f; // 85% Ȯ���� true ��ȯ
    }

    // �׽�Ʈ �Լ�
   public void OnClickRandon()
    {
        if (TrySuccess())
        {
            Debug.Log("����");
        }
        else
        {
            Debug.Log("����");
        }
    }
}
