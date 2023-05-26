using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        base.Die();
        CombatEvents.TargetDied(this);
        Destroy(gameObject);

    }
}
