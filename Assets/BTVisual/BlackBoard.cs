using System;
using UnityEngine;

[Serializable]
public class BlackBoard
{
    public Transform targetTrm;
    public Vector3 moveToPosition;
    public Vector3 enemySpotPosition;
    public float spotTime;
    public float startChaseTime = 0;
    public LayerMask whatIsEnemy, whatIsObstacle;
    public bool animationEnd;
    public bool isAttacking;
}
