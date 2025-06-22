using System;
using DG.Tweening;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _01Scripts.BTEnemy.BTCode.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "JumpAttack", story: "[Self] jump attack to [Target]", category: "Certificate/Action", id: "80592a8c08a5f2880375b90d9232febf")]
    public partial class JumpAttackAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyGolem> Self;
        [SerializeReference] public BlackboardVariable<Transform> Target;

        private AgentRenderer _rendererCompo;
        private AgentAnimator _agentAnimator;
    
        private readonly int _jumpAtkHash = Animator.StringToHash("JumpATK");
        private readonly int _dropAtkHash = Animator.StringToHash("DropATK");

        private Sequence _jumpseq;
        private bool _isAttackEnd;
        protected override Status OnStart()
        {
            InitComponents();
            _isAttackEnd = false;
            _rendererCompo.SetSortingToTop(true);
            _jumpseq = DOTween.Sequence();
            _jumpseq.Append(Self.Value.transform.DOJump(Target.Value.transform.position, 4f, 1, 1.2f).SetEase(Ease.Linear));
            _jumpseq.JoinCallback(() => _agentAnimator.SetTrigger(_jumpAtkHash));
            _jumpseq.InsertCallback(1f, () => _agentAnimator.SetTrigger(_dropAtkHash));
            _jumpseq.OnComplete(() =>
            {
                _rendererCompo.SetSortingToTop(false);
                _isAttackEnd = true;
            });
            return Status.Running;
        }

        private void InitComponents()
        {
            if(_rendererCompo == null)
                _rendererCompo = Self.Value.GetCompo<AgentRenderer>();
        
            if(_agentAnimator == null)
                _agentAnimator = Self.Value.GetCompo<AgentAnimator>(true);
        }

        protected override Status OnUpdate()
        {
            if(_isAttackEnd)
                return Status.Success;
            return Status.Running;
        }

        protected override void OnEnd()
        {
            if(_jumpseq != null && _jumpseq.IsActive())
                _jumpseq.Kill();
        }
    }
}

