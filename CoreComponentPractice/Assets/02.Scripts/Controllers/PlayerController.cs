using ETL10.StateMachineBehaviours;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ETL10.Controllers
{
    public class PlayerController : CharacterController, InputActions.IPlayerActions
    {
        private InputActions _inputActions;


        protected override void Awake()
        {
            base.Awake();

            _inputActions = new InputActions();
            _inputActions.Player.AddCallbacks(this);
            _inputActions.Enable();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                stateCommands[State.Jump] = true;
            }
            else if (context.canceled)
            {
                stateCommands[State.Jump] = false;
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }

        public void OnMove(InputAction.CallbackContext context)
        {
        }
    }
}