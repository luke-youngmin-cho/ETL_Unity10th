using UnityEngine;

namespace ETL10.StateMachineBehaviours
{
    public class Jump : BehaviourBase
    {
        private const float JUMP_FORCE = 5.0f;


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            // ���ӵ� a = v / t -> a x t = v
            // ForceMode 
            // Force : F = m(����) x a(���ӵ�), ������ ����Ͽ� ���ӵ��� �����ϰ� ������ ����ϴ� ���
            // Impulse : I = F(��) x t(�ð�) = m x a x t = m(����) x v(�ӵ�), ������ ����Ͽ� �ӵ��� �����ϰ� ������ ����ϴ� ���
            // Acceleration : a(���ӵ�) , ������ ������� �ʰ� ���ӵ��� �����ϰ� ������ ����ϴ� ���
            // VelocityChange : v(�ӵ�), ������ ������� �ʰ� �ӵ��� �����ϰ� ������ ����ϴ� ���
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