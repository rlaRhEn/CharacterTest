using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

using UnityEngine.Networking;

public class Sheet : MonoBehaviour
{
    public readonly string ADRESS = "https://docs.google.com/spreadsheets/d/1-zS5N7VAfl30BOx_vuHpB-Muo6KbDsRVOYAway74BeM";
    public readonly string RANGE = "A1:D";
    public readonly long SHEET_ID = 0;
   public static string GetSVAdress(string adress, string range, long sheetID)
    {//�ּҹ�ȯ
        return $"{adress}/export?format=tsv&range={range}&gid={sheetID}";
    }
    private void Start()
    {
        StartCoroutine(LoadData());
       
    }
    private IEnumerator LoadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(GetSVAdress(ADRESS, RANGE, SHEET_ID));
        yield return www.SendWebRequest();

        Debug.Log(GetData<Animal>(www.downloadHandler.text.Split('\n')[0].Split('\t')).name);//ù��° ���� ������
    }
    T GetData<T>(string[] datas)
    {
        object data = Activator.CreateInstance(typeof(T));

        // Ŭ������ �ִ� �������� ������� ������ �迭
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < datas.Length; i++)
        {
            try
            {
                // string > parse
                Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(datas[i])) continue;

                // ������ �´� �ڷ������� �Ľ��ؼ� �ִ´�
                if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(datas[i]));

                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(datas[i]));

                else if (type == typeof(bool))
                    fields[i].SetValue(data, bool.Parse(datas[i]));

                else if (type == typeof(string))
                    fields[i].SetValue(data, datas[i]);

                // enum
                else
                    fields[i].SetValue(data, Enum.Parse(type, datas[i]));
            }

            catch (Exception e)
            {
                Debug.LogError($"SpreadSheet Error : {e.Message}");
            }
        }
        return (T)data;
    }
}
[System.Serializable]
public class Animal //������ ���� Ŭ���� �ڵ�
{ //���ǻ��� 1. ��Ʈ���ִ� �����Ϳ� ��ġ
  //2.enum ���� ��Ʈ�� �����ִ� �����Ϳ� �̸��� �����Ȱ��ƾ��Ѵ�.
  // >> pink�� PINK�� �ȵ�
    public string name;
    public int attack;
    public float shield;
    public ColorType colorType;
}

public enum ColorType
{
    pink, Orange, Blue
}