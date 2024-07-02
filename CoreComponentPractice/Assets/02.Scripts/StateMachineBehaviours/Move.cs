using ETL10.Controllers;
using UnityEngine;
using CharacterController = ETL10.Controllers.CharacterController;

namespace ETL10.StateMachineBehaviours
{
    public class Move : BehaviourBase
    {
        [SerializeField] LayerMask _groundMask;
        private const float CAST_START_OFFSET = 0.001f;
        private const float CAST_DISTANCE = 0.01f;


        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (Physics.Raycast(animator.transform.position + Vector3.up * CAST_START_OFFSET,
                                Vector3.down,
                                out RaycastHit hit,
                                CAST_DISTANCE,
                                _groundMask) == false)
            {
                ChangeState(animator, State.Fall);
            }

            if (animator.GetComponent<CharacterController>().stateCommands[State.Jump])
            {
                ChangeState(animator, State.Jump);
            }
        }
    }
}