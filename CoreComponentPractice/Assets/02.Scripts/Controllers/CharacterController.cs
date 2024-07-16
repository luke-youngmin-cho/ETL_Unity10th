using UnityEngine;
using ETL10.StateMachineBehaviours;
using System.Collections.Generic;
using System;
using ETL10.GameElements.StatSystems;
using ETL10.Data;

namespace ETL10.Controllers
{
    [RequireComponent(typeof(Animator))]
    public abstract class CharacterController : MonoBehaviour, IStatsContainer
    {
        public Dictionary<State, bool> stateCommands;
        protected Animator animator;

        public int hp;

        [field: SerializeField] public CharacterSpecification specification { get; private set; }

        public Dictionary<StatType, Stat> stats { get; protected set; }

        private void LateUpdate()
        {
            Debug.Log($"AttackForceStat : {stats[StatType.AttackForce].value}, {stats[StatType.AttackForce].valueModified}");
        }
        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            stateCommands = new Dictionary<State, bool>();

            foreach (State item in Enum.GetValues(typeof(State)))
            {
                stateCommands.Add(item, false);
            }

            stats = new Dictionary<StatType, Stat>();

            foreach (var statPair in specification.statPairs)
            {
                stats.Add(statPair.type, new Stat(statPair.type, statPair.value));
            }
            
            hp = stats[StatType.Hp].valueModified;
            stats[StatType.Hp].onValueModifiedChanged += (before, after) =>
            {
                float ratio = hp / (float)before;
                hp = (int)(ratio * after);
            };
        }

        public void ChangeState(State newState)
        {
            animator.SetInteger("state", (int)newState);
            animator.SetBool("isDirty", true);
        }
    }
}