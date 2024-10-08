using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using UGS;

public class MonsterCon : MonoBehaviour
{
    [Header("Stat")]
    public int monsterCode;
    public float currentHp,maxHp, armor, moveSpeed;
    [SerializeField] string name;
    [SerializeField] int gold = 10;


    [Header("wayPoint,Move")]
    [SerializeField]int wayPointCount; //이동 경로 개수
    [SerializeField]Transform[] wayPoints; // 이동 경로 정보
    [SerializeField] int currentIndex = 0; //현재 목표지점 인덱스
    [SerializeField] Vector3 moveDirection = Vector3.zero;

    [Header("etc")]
    [SerializeField] GameObject dmgText;
    Canvas canvas;
    PlayerGold playerGold;
    SPUM_Prefabs spum_prefabs;
    MonsterSpawner monsterSpawner;
    float monsterX = -0.7f;
    bool isDie = false; //적이 사망하면 isDie를 true로 설정
   
    Wave currentWave;

    public IObjectPool<GameObject> Pool{ get; set; }
    //RectTransform rect;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        playerGold = GameObject.Find("GameManager").GetComponent<PlayerGold>();
        monsterSpawner = GameObject.Find("PoolManager").GetComponent<MonsterSpawner>();
        spum_prefabs = GetComponent<SPUM_Prefabs>();
        UnityGoogleSheet.Load<monsterBal.Data>();

        StartCoroutine(Initialize());
    }
    private IEnumerator Initialize() //GameManager에서 로드한 구글시트 데이터를 여기서 사용.
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        var data = GameManager.instance.GetMonsterData(monsterCode);
        if(data != null)
        {
            maxHp = data.hp;
            armor = data.armor;
            moveSpeed = data.moveSpeed;
            name = data.name;
            currentHp = maxHp;
        }
    }

    private void OnEnable()
    {
        // WayPoint 태그로 등록된 모든 오브젝트를 찾아서 wayPoints 배열에 추가
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
        //transform.localScale = new Vector3(monsterX, 0.7f, 0.7f);

    }
    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    public void SetUp(Transform[] wayPoints)
    {
        //적 이동 경로 WayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        //적의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;

        //적 이동/목표지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");

    }
    IEnumerator OnMove()
    {
        //다음 이동방향 설정
        NextMoveTo();
        while(true)
        {
            //적의 현재위치와 목표위치의 거리가 0.02*movement2D.MoveSpeed보다 작을 때 if 조건문 실행
            //Tip. movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문에
            //if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있다.
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * moveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }
    void NextMoveTo()
    {
        //아직 이동할 wayPoints가 남아있다면 
        if(currentIndex < wayPointCount - 1)
        {
            //적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;
            //이동 방향 설정 = > 다음 목표지점(wayPoints)
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            MoveTo(direction);
            //오른쪽 이동
            if (transform.position.x > 0) transform.localScale = new Vector3(-monsterX, 0.7f, 0.7f);
            //왼쪽이동
            else if (transform.position.x < 0) transform.localScale = new Vector3(monsterX, 0.7f, 0.7f);
        }
        //현재 위치가 마지막 wayPoints 이면
        else
        {
            //처음 위치를 목표위치로
            currentIndex = 0;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            MoveTo(direction);
        }

    }
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void TakeDamage(Vector3 hitPos, float damage)
    {
        Debug.Log(currentHp);
        //적의 체력이 damage만큼 감소해서 죽을 상황일 때 여러타워의 공격을 동시에 받으면
        //Ondie()가 여러번 실행 할 수 있다 현재 적의 상태가 사망상태이면 아래코드를 실행하지않는다.
        if (isDie == true) return;
        currentHp -= (damage-armor);
        GameObject damageText = Instantiate(dmgText, hitPos, Quaternion.identity, canvas.transform);
        damageText.GetComponent<Text>().text = (damage-armor).ToString();

        if (currentHp <= 0) //체력 0이하 속도0,사망, 태그
        {
            isDie = true;
            spum_prefabs.PlayAnimation("death");
            moveSpeed = 0;
            Invoke("OnDieMonster", 0.4f);
            monsterSpawner.monsterList.Remove(monsterSpawner.monsterList[0]);  //
        }
    }

    public void OnDieMonster()
    {
        Destroy(gameObject);
        playerGold.CurrentGold += gold;
      //
    }

}
