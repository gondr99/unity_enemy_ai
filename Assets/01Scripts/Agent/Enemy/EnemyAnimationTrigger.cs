using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    public event Action AnimationEndEvent;
    public event Action AttackCasteEvent;

    private void OnAnimationEndTrigger()
    {
        AnimationEndEvent?.Invoke();
    }

    private void OnAttackCast()
    {
        AttackCasteEvent?.Invoke();
    }
}
