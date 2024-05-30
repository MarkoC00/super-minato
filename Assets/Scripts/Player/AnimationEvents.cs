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

    public void Rasengan()
    {
        combat.SpawnRasengan();
    }

    public void StopRasengan()
    {
        combat.StopRasengan();
    }

    public void DeathStart()
    {
        combat.animator.SetBool("Death", false);
    }
    public void OnDeath()
    {
        combat.RestartGame();
    }

}
