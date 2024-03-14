using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnOffField : MonoBehaviour
{
    GameObject obj_Field;
    Image image;
    [SerializeField] SceneNames sceneName;

    private bool checkbool = false; // ���� ���� ���� ����
    private bool turnOffbool = false;

    private void Awake()
    {
        obj_Field = this.gameObject; //��ũ��Ʈ ������ ������Ʈ
        image = obj_Field.GetComponent<Image>(); 
    }
    private void Update()
    {
        if(turnOffbool)
        {
            StartCoroutine(SplashField());
        }
        if (checkbool)
        {
            BuildScene.LoadScene(sceneName);
            //Destroy(this.gameObject);
        }
    }
    IEnumerator SplashField()
    {
        Color color = image.color;

        for (int i = 100; i >= 0; i--)
        {
            color.a -= Time.deltaTime * 0.01f;
            image.color = color;

            if(image.color.a <=0)
            {
                checkbool = true;
            }
        }
        yield return null;
    }
    public void NextScene()
    {
        turnOffbool = true;
    }
}
