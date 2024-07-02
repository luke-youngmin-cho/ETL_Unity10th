using UnityEngine;

namespace ETL10.StateMachineBehaviours
{
    public enum State
    {
        Move,
        Jump,
        Fall,
    }

    /* Naming Convention
     * PascalCase : ����������ڷ��� �̸� (enum �� ��� ��ҵ� ����), �Լ� �̸�, Non-Private ���� , ������Ƽ �̸�
     * camelCase : ��������, �Ű�����
     * _camelCase : private ����
     * s_camelCase : private static ����
     * snake_case : ���
     * UPPER_SNAKE_CASE : ���
     * 
     * ����������, Unity API �� ����Ѵٸ� �Ϲ������� Non-private ����, ������Ƽ �̸� �� camelCase �� �����.
     */
    public class BehaviourBase : StateMachineBehaviour
    {
        private readonly int HASH_ID_STATE = Animator.StringToHash("state");
        private readonly int HASH_ID_IS_DIRTY = Animator.StringToHash("isDirty");


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            animator.SetBool(HASH_ID_IS_DIRTY, false);
        }

        protected void ChangeState(Animator animator, State newState)
        {
            animator.SetInteger(HASH_ID_STATE, (int)newState);
            animator.SetBool(HASH_ID_IS_DIRTY, true);
        }
    }
}