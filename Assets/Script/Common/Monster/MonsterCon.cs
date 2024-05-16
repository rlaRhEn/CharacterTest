using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UGS;

public class MonsterCon : MonoBehaviour
{
    [Header("Stat")]
    public int monsterCode;
    [SerializeField] float currentHp,maxHp, armor, moveSpeed;
    [SerializeField] string type;
    [SerializeField] int gold = 10;


    [Header("wayPoint,Move")]
    [SerializeField]int wayPointCount; //�̵� ��� ����
    [SerializeField]Transform[] wayPoints; // �̵� ��� ����
    [SerializeField] int currentIndex = 0; //���� ��ǥ���� �ε���
    [SerializeField] Vector3 moveDirection = Vector3.zero;

    [Header("etc")]
    PlayerGold playerGold;
    SPUM_Prefabs spum_prefabs;
    MonsterSpawner monsterSpawner;
    float monsterX = -0.7f;
    bool isDie = false; //���� ����ϸ� isDie�� true�� ����
    public GameObject damageText;
    public Transform textPos;

    public IObjectPool<GameObject> Pool{ get; set; }
    //RectTransform rect;
    private void Awake()
    {
        playerGold = GameObject.Find("GameManager").GetComponent<PlayerGold>();
        monsterSpawner = GameObject.Find("PoolManager").GetComponent<MonsterSpawner>();
        spum_prefabs = GetComponent<SPUM_Prefabs>();
        UnityGoogleSheet.Load<monsterBal.Data>();

       
    }

    private void Start()
    {
        maxHp = monsterBal.Data.DataMap[monsterCode].hp;
        armor = monsterBal.Data.DataMap[monsterCode].armor;
        moveSpeed = monsterBal.Data.DataMap[monsterCode].moveSpeed;
        type = monsterBal.Data.DataMap[monsterCode].type;
        currentHp = maxHp;

    }
    private void OnEnable()
    {
        // WayPoint �±׷� ��ϵ� ��� ������Ʈ�� ã�Ƽ� wayPoints �迭�� �߰�
        GameObject[] wayPointObjects = GameObject.FindGameObjectsWithTag("WayPoint");
        wayPoints = new Transform[wayPointObjects.Length];
        for (int i = 0; i < wayPointObjects.Length; i++)
        {
            wayPoints[i] = wayPointObjects[i].transform;
        }
        transform.position = wayPoints[0].position;


        gameObject.tag = "Monster";
        SetUp(wayPoints);
        spum_prefabs.PlayAnimation("run");
        transform.localScale = new Vector3(monsterX, 0.7f, 0.7f);

    }
    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    public void SetUp(Transform[] wayPoints)
    {
        //�� �̵� ��� WayPoints ���� ����
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        //���� ��ġ�� ù��° wayPoint ��ġ�� ����
        transform.position = wayPoints[currentIndex].position;

        //�� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ� ����
        StartCoroutine("OnMove");

    }
    IEnumerator OnMove()
    {
        //���� �̵����� ����
        NextMoveTo();
        while(true)
        {
            //���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02*movement2D.MoveSpeed���� ���� �� if ���ǹ� ����
            //Tip. movement2D.MoveSpeed�� �����ִ� ������ �ӵ��� ������ �� �����ӿ� 0.02���� ũ�� �����̱� ������
            //if ���ǹ��� �ɸ��� �ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� �ִ�.
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * moveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }
    void NextMoveTo()
    {
        //���� �̵��� wayPoints�� �����ִٸ� 
        if(currentIndex < wayPointCount - 1)
        {
            //���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoints[currentIndex].position;
            //�̵� ���� ���� = > ���� ��ǥ����(wayPoints)
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            MoveTo(direction);
            //������ �̵�
            if (transform.position.x > 0) transform.localScale = new Vector3(-monsterX, 0.7f, 0.7f);
            //�����̵�
            else if (transform.position.x < 0) transform.localScale = new Vector3(monsterX, 0.7f, 0.7f);
        }
        //���� ��ġ�� ������ wayPoints �̸�
        else
        {
            //ó�� ��ġ�� ��ǥ��ġ��
            currentIndex = 0;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            MoveTo(direction);
        }

    }
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log(currentHp);
        //���� ü���� damage��ŭ �����ؼ� ���� ��Ȳ�� �� ����Ÿ���� ������ ���ÿ� ������
        //Ondie()�� ������ ���� �� �� �ִ� ���� ���� �󤼰��� ��������̸� �Ʒ��ڵ带 ���������ʴ´�.
        if (isDie == true) return;

        currentHp -= damage;
        GameObject hudText = Instantiate(damageText); //������ �ؽ�Ʈ ������Ʈ
        hudText.transform.position = textPos.position;//ǥ�õ� ��ġ
        hudText.GetComponent<DamageText>(); //������ ����
        if (currentHp <= 0)
        {
            isDie = true;
            OnDieMonster();
        }
    }

    public void OnDieMonster()
    {
        Destroy(gameObject);
        playerGold.CurrentGold += gold;
        monsterSpawner.currentEnemyCount--;
    }

}
