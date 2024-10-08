using Demo.AISystems.BT;
using Fusion;
using UnityEngine;

namespace Demo
{
    [RequireComponent(typeof(BehaviourTree))]
    public class NPCController : NetworkBehaviour
    {
        [Header("AI Behaviours")]
        [Header("Seek")]
        [SerializeField] float _seekRadius = 5.0f;
        [SerializeField] float _seekHeight = 1.0f;
        [SerializeField] float _seekAngle = 90f;
        [SerializeField] LayerMask _seekTargetMask;
        [SerializeField] float _seekMaxDistance = 10f;

        private void Awake()
        {
            // 체이닝함수로 빌드함수들 제공할때
            BehaviourTree tree = GetComponent<BehaviourTree>();
            tree.Build()
                    .Selector()
                        .Sequence()
                            .Seek(_seekRadius, _seekHeight, _seekAngle, _seekTargetMask, _seekMaxDistance)
                            .Decorator(() => true)
                                .Execution(() => Result.Success)
                            .FinishCurrentComposite()
                        .FinishCurrentComposite();
            
            // 그냥 하드코딩
            /*tree.Build();
            Selector selector1 = new Selector(tree);
            tree.root.child = selector1;
            Sequence sequence1 = new Sequence(tree);
            Selector selector2 = new Selector(tree);
            selector1.children.Add(sequence1);
            selector1.children.Add(selector2);
            Seek seek = new Seek(tree, _seekRadius, _seekHeight, _seekAngle, _seekTargetMask, _seekMaxDistance);
            sequence1.children.Add(seek);
            Decorator isInAttackRange = new Decorator(tree, () =>
            {
                Debug.Log("Check target is attack range");
                return true;
            });
            Execution attack = new Execution(tree, () =>
            {
                Debug.Log("Start Attack");
                return Result.Success;
            });
            isInAttackRange.child = attack;
            sequence1.children.Add(isInAttackRange);*/
        }
    }
}