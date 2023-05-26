using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    public delegate void TargetEventHandler(CharacterStats target);
    public static event TargetEventHandler onTargetDeath;

    public static void TargetDied(CharacterStats target)
    {
        if (onTargetDeath != null)
        {
            onTargetDeath(target);
        }
    }
}
