using System;
using DG.Tweening;
using UnityEngine;

public class RecoilFeedback : Feedback
{
    [SerializeField] private float recoilDelta = 0.2f;
    [SerializeField] private Transform targetTrm;
    public override void CreateFeedback()
    {
        float targetX = targetTrm.localPosition.x - recoilDelta;
        targetTrm.DOLocalMoveX(targetX, 0.05f).SetLoops(2, LoopType.Yoyo);
    }

    public override void FinishFeedback()
    {
        targetTrm.DOComplete();
    }

    private void OnDisable()
    {
        FinishFeedback();
    }
}
