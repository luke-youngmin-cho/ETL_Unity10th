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
     * PascalCase : 사용자정의자료형 이름 (enum 의 모든 요소도 포함), 함수 이름, Non-Private 변수 , 프로퍼티 이름
     * camelCase : 지역변수, 매개변수
     * _camelCase : private 변수
     * s_camelCase : private static 변수
     * snake_case : 상수
     * UPPER_SNAKE_CASE : 상수
     * 
     * 예외적으로, Unity API 를 사용한다면 일반적으로 Non-private 변수, 프로퍼티 이름 에 camelCase 를 사용함.
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