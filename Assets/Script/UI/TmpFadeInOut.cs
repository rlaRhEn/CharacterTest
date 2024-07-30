using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TmpFadeInOut : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI successMeshPro;
    [SerializeField]TextMeshProUGUI failedMeshPro;
    public float fadeInDuration = 2.0f; // ���� ���� 0���� 1�� ���ϴ� �� �ɸ��� �ð�
    public float fadeOutDuration = 2.0f; // ���� ���� 1���� 0���� ���ϴ� �� �ɸ��� �ð�

    void Start()
    {
        
        // �ڷ�ƾ ����
        //StartCoroutine(FadeInAndOutText());
    }

    public IEnumerator SuccessFadeInAndOutText()
    {
        Color originalColor = successMeshPro.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0); //���� 0

        // �ؽ�Ʈ�� ���� ���� ����
        successMeshPro.color = transparentColor;

        // ���� �� ���� (���̵� ��)
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            successMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // ���� �����ӱ��� ���
        }


        // ���� �� ���� (���̵� �ƿ�)
        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - elapsedTime / fadeOutDuration);
            successMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // ���� �����ӱ��� ���
        }

        // ���������� ���� ���� Ȯ���ϰ� 0���� ����
        successMeshPro.color = transparentColor;
    }


    public IEnumerator FailedFadeInAndOutText()
    {
        Color originalColor = failedMeshPro.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0); //���� 0

        // �ؽ�Ʈ�� ���� ���� ����
        failedMeshPro.color = transparentColor;

        // ���� �� ���� (���̵� ��)
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            failedMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // ���� �����ӱ��� ���
        }


        // ���� �� ���� (���̵� �ƿ�)
        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - elapsedTime / fadeOutDuration);
            failedMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // ���� �����ӱ��� ���
        }

        // ���������� ���� ���� Ȯ���ϰ� 0���� ����
        failedMeshPro.color = transparentColor;
    }
}

