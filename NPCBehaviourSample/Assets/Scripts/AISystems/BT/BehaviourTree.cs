using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AISystems.BT
{
    public class BehaviourTree : MonoBehaviour
    {
        public Blackboard blackboard { get; private set; }
        public Stack<Node> stack = new Stack<Node>();
        public Root root;
        public bool isRunning;

        private void Update()
        {
            if (isRunning)
                return;

            isRunning = true;
            StartCoroutine(C_Tick());
        }

        IEnumerator C_Tick()
        {
            stack.Push(root);

            while (stack.Count > 0)
            {
                Node current = stack.Pop();
                Result result = current.Invoke();

                if (result == Result.Running)
                {
                    stack.Push(current);
                    yield return null;
                }
            }

            isRunning = false;
        }


        public void Build()
        {
            blackboard = new Blackboard(gameObject);
            root = new Root(this);
        }
    }
}
