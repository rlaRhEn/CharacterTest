using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArcher : TowerCon
{
    public override void DoAttacker()
    {
        spum_Prefabs.PlayAnimation(3);
    }
    public override Transform SetProjectile()
    {
        Transform projectile = GameManager.instance.pool.GetProJectile(0).transform; //������ ���� ������Ÿ�� �ٲ����
        projectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); //Ÿ�� ��ġ���� ��ȯ
        return projectile;
    }
}
