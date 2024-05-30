using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UGS;

public enum WeaponState { SearchTarget, AttackToTarget}
public class TowerCon : MonoBehaviour
{
    public SPUM_Prefabs spum_Prefabs;
    [SerializeField] TowerTemplate towerTemplate;
    [SerializeField] PlayerGold playerGold;
    [Header("Stat")]
    [SerializeField] int characterCode, level;
    [SerializeField] float attackSpeed, speed, attackTimer;
    [SerializeField] string type;
    [SerializeField] float attackRange;// ���� Ÿ��

    [SerializeField] Transform target;

    [SerializeField]Tile ownerTile;

    public float Attack => towerTemplate.weapon[level].attack;
    public float AttackIncrease => towerTemplate.weapon[level].attackIncrease;
    public int Cost => towerTemplate.weapon[level].cost;
    public float Probablilty => towerTemplate.weapon[level].probability;
    public int Fail => towerTemplate.weapon[level].fail;

    public float Range => attackRange;
    public int Level => level+1;
    public int MaxLevel => towerTemplate.weapon.Length;

    public Vector3 goalPos;


    public enum TowerState
    {
        idle,
        run,
        attack,
        skill,
        stun,
        death
    }
    public TowerState tower_State;
    public WeaponState weaponState = WeaponState.SearchTarget;

    private void Awake()
    {
        spum_Prefabs = GetComponent<SPUM_Prefabs>();
        UnityGoogleSheet.Load<characterBal.Balance>();
    }
    void Start()
    {
        playerGold = GameObject.Find("GameManager").GetComponent<PlayerGold>();
        attackSpeed = characterBal.Balance.BalanceMap[characterCode].attackSpeed;
        attackRange = characterBal.Balance.BalanceMap[characterCode].attackRange;
        type = characterBal.Balance.BalanceMap[characterCode].type;
        speed = characterBal.Balance.BalanceMap[characterCode].speed;
        level = 0;

    }
    private void OnEnable()
    {
        ChangeState(WeaponState.SearchTarget);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.localPosition.y * 0.01f);
        //CheckState();
    }
    public void SetUp(Tile ownerTile)
    {
        this.ownerTile = ownerTile;
    }
    public void ChangeState(WeaponState nowstate)//���� state
    {
        //���� ������̴� ���� ����
        StopCoroutine(weaponState.ToString());
        //���� ����
        weaponState = nowstate;
        //���ο� �ڷ�ƾ ����
        StartCoroutine(weaponState.ToString());
    }
    void CheckState() //�ִϸ��̼� state
    {
        switch (tower_State)
        {
            case TowerState.idle:
                break;
            case TowerState.attack:
                break;
            case TowerState.run:
                DoMove();
                break;
            case TowerState.skill:
                break;
        }
    }

    public void SetState(TowerState state)
    {
        tower_State = state;
        switch (tower_State)
        {
            case TowerState.idle:
                spum_Prefabs.PlayAnimation("0_idle");
                break;
            case TowerState.attack:
                spum_Prefabs.PlayAnimation("2_Attack_Normal");
                break;
            case TowerState.run:
                spum_Prefabs.PlayAnimation("run");

                break;
            case TowerState.skill:
                spum_Prefabs.PlayAnimation("2_Attack_Magic");
                break;
        }
    }
   
    public void DoMove()
    {
        
        Vector3 _dirVec = goalPos - transform.position;
        Vector3 _disVec = (Vector2)goalPos - (Vector2)transform.position;
        if (_disVec.sqrMagnitude < 0.1f)
        {
            SetState(TowerState.idle);
            return;
        }
        Vector3 _dirMVec = _dirVec.normalized;
        transform.position += (_dirMVec * speed * Time.deltaTime);


        if (_dirMVec.x > 0) spum_Prefabs.transform.localScale = new Vector3(-1, 1, 1);
        else if (_dirMVec.x < 0) spum_Prefabs.transform.localScale = new Vector3(1, 1, 1);
    }
    public void SetMovePos(Vector2 pos)//��ǥ�����̵�
    {
        goalPos = pos;
        SetState(TowerState.run);
    }
    IEnumerator SearchTarget()
    {
        while (true)
        {
 
            // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
            float closestDisSqr = Mathf.Infinity;
            Transform closestTarget = null;

            for (int i = 0; i < GameManager.instance.pool.monsterPools.Length; i++)
            {
                // ���� Ǯ�� �� ���͸� �����ͼ� transform �Ӽ��� ���
                foreach (GameObject monster in GameManager.instance.pool.monsterPools[i])
                {
                    if (monster != null && monster.CompareTag("Monster"))
                    {
                        float distance = Vector3.Distance(monster.transform.position, transform.position);
                        if (distance <= attackRange && distance <= closestDisSqr)
                        {
                            closestDisSqr = distance;
                            closestTarget = monster.transform;
                        }
                    }
                }
            }
            // ���� ����� ���� ã������ ���� ����
            if (closestTarget != null)
            {
                target = closestTarget;
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }
    //IEnumerator SearchTarget()
    //{
    //    while(true)
    //    {
    //        ���� ������ �ִ� ���� ã�� ���� ���ʰŸ��� �ִ��� ũ�Լ���
    //        float closesetDisSqr = Mathf.Infinity;

    //        for (int i = 0; i < GameManager.instance.pool.monsterPools.Length; i++)
    //        {
    //            GameObject monster = GameManager.instance.pool.monsterPools[0];
    //            Transform monsterTransform = monster.transform;
    //            float distance = Vector3.Distance(GameManager.instance.pool.monsterPools[i].transform.position, transform.position);
    //            if (distance <= attackRange && distance <= closesetDisSqr)
    //            {
    //                closesetDisSqr = distance;
    //                target = GameManager.instance.pool.monsterPools[i].transform;
    //            }
    //        }
    //        if(target != null)
    //        {
    //            ChangeState(WeaponState.AttackToTarget);
    //        }
    //        yield return null;
    //    }
    //}
    IEnumerator AttackToTarget()
    {
        while(true)
        {
            //spum_Prefabs.PlayAnimation("0_idle");
            spum_Prefabs.PlayAnimation("2_Attack_Normal");
            //Ÿ���� �ִ��� �˻�
            if (target ==null || !target.gameObject.activeSelf)
            {
                Debug.Log("Ÿ�� ��ġ");
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            //Ÿ���� ���� ���� �ȿ��ִ��� �˻� (���� ���� ����� ���ο� �� Ž��)
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if(distance > attackRange)
            {
                target = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            
            yield return new WaitForSeconds(attackSpeed);
            spum_Prefabs.PlayAnimation("2_Attack_Normal");
            Transform projectile =  GameManager.instance.pool.GetProJectile(0).transform;
            projectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            projectile.GetComponent<Projectile>().Setup(target, towerTemplate.weapon[level].attack);
            
        }
    }
    public bool Upgrade()
    {
        
        //Ÿ�� ���׷��̵忡 �ʿ��� ��尡 ������� �˻�
        if (playerGold.CurrentGold < towerTemplate.weapon[level+1].cost)
        {
            //�� ����
            return false;
        }
        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;
        //Ȯ�� �ֱ�   //Ÿ������ ����
        if (TrySuccess())
        {
            level++;
            Debug.Log("��ȭ����");
            return true;
        }
        else // 10���� �̻��� �� ���� �϶�
        {
            Debug.Log("��ȭ ����");
            return false;
        }
    }
    public bool TrySuccess()
    {
        float randomValue = Random.value; // 0.0���� 1.0������ ������ ����
        Debug.Log(randomValue);
        return randomValue < towerTemplate.weapon[level + 1].probability / 100; //������ Ȯ���� true ��ȭ
    }
    public void Sell()
    {
        //��� ����
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}
