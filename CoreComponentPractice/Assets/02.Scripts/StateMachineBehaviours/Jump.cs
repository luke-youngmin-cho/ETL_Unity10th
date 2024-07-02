using UnityEngine;

namespace ETL10.StateMachineBehaviours
{
    public class Jump : BehaviourBase
    {
        private const float JUMP_FORCE = 5.0f;


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            // 가속도 a = v / t -> a x t = v
            // ForceMode 
            // Force : F = m(질량) x a(가속도), 질량을 고려하여 가속도를 설정하고 싶을때 사용하는 모드
            // Impulse : I = F(힘) x t(시간) = m x a x t = m(질량) x v(속도), 질량을 고려하여 속도를 설정하고 싶을때 사용하는 모드
            // Acceleration : a(가속도) , 질량을 고려하지 않고 가속도를 설정하고 싶을때 사용하는 모드
            // VelocityChange : v(속도), 질량을 고려하지 않고 속도를 설정하고 싶을때 사용하는 모드
            animator.GetComponent<Rigidbody>().AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (animator.GetComponent<Rigidbody>().velocity.y <= 0f)
            {
                ChangeState(animator, State.Fall);
            }
        }
    }
}