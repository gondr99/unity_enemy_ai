using System;
using DG.Tweening;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _01Scripts.BTEnemy.BTCode.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "DashAttack", story: "[Self] dash attack to [target]", category: "Certificate/Action", id: "945ace3de4d69cc301716ad5cb325406")]
    public partial class DashAttackAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyGolem> Self;
        [SerializeReference] public BlackboardVariable<Transform> Target;

        private AgentRenderer _rendererCompo;
        private bool _isEnd;
        
        protected override Status OnStart()
        {
            InitializeComponent();
            _isEnd = false;
            
            _rendererCompo.SetSortingToTop(true);
            Sequence seq = DOTween.Sequence();
            Transform transform = Self.Value.transform;
            seq.Append(transform.DOShakePosition(0.3f, 0.3f, 20));
            seq.Append(transform.DOMove(Target.Value.position, 0.8f).SetEase(Ease.InCubic));
            seq.OnComplete(() =>
            {
                _isEnd = true;
                _rendererCompo.SetSortingToTop(false);
            });
            return Status.Running;
        }

        private void InitializeComponent()
        {
            if (_rendererCompo == null)
                _rendererCompo = Self.Value.GetCompo<AgentRenderer>();
        }

        protected override Status OnUpdate()
        {
            if(_isEnd)
                return Status.Success;
            return Status.Running;
        }
    }
}

