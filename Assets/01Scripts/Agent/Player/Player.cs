using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    [field:SerializeField] public InputReader PlayerInput {get; private set;}

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        MoveCompo.SetMovement(PlayerInput.Movement);
    }
}
