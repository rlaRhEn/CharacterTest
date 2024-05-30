using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    //private float moveSpeed;
    private float alphaSpeed;
    //private float destroyTime;
    [SerializeField]Text text;
    [SerializeField]Color alpha;
    //public int damage;

    void Start()
    {
        //moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        //destroyTime = 2.0f;

        //text = GetComponent<Text>();
        alpha = text.color;
        //text.text = damage.ToString();
        Invoke("DestroyObject", 1);
    }

    void Update()
    {
        transform.position += new Vector3 (0,0.5f,0);
        //transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
