using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerKnight :TowerCon
{
    public override void DoAttacker()
    {
        spum_Prefabs.PlayAnimation("2_Attack_Normal");
        spum_Prefabs.PlayAnimation("0_idle");
    }
    public override Transform SetProjectile()
    {
        Transform projectile = GameManager.instance.pool.GetProJectile(2).transform; //������ ���� ������Ÿ�� �ٲ����
        projectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); //Ÿ�� ��ġ���� ��ȯ
        return projectile;
    }
}
