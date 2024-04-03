using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerCombat combat;
    public void StopAttackAnimation()
    {
        combat.SetAttackToFalse();
    }
    public void PreformAttack()
    {
        combat.DealDamage();
    }
}