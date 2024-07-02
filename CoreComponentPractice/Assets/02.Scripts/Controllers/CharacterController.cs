using UnityEngine;
using ETL10.StateMachineBehaviours;
using System.Collections.Generic;
using System;

namespace ETL10.Controllers
{
    [RequireComponent(typeof(Animator))]
    public abstract class CharacterController : MonoBehaviour
    {
        public Dictionary<State, bool> stateCommands;
        protected Animator animator;


        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            stateCommands = new Dictionary<State, bool>();

            foreach (State item in Enum.GetValues(typeof(State)))
            {
                stateCommands.Add(item, false);
            } 
        }

        public void ChangeState(State newState)
        {
            animator.SetInteger("state", (int)newState);
            animator.SetBool("isDirty", true);
        }
    }
}