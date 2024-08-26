using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AISystems.BT
{
    public class BehaviourTree : MonoBehaviour
    {
        public Blackboard blackboard { get; private set; }
        public Stack<Node> stack = new Stack<Node>(); // DFS (깊이우선탐색) 추적할 노드들을 쌓아놓기위함.
        public Root root;
        public bool isRunning;

        private void Update()
        {
            if (isRunning)
                return;

            isRunning = true;
            StartCoroutine(C_Tick());
        }

        /// <summary>
        /// 트리 탐색하는 단위
        /// </summary>
        IEnumerator C_Tick()
        {
            stack.Push(root);

            // 탐색할 노드가 남았으면 계속 탐색
            while (stack.Count > 0)
            {
                // 가장 최근에 등록한 노드 탐색
                Node current = stack.Pop();
                Result result = current.Invoke();

                // 현재 탐색중인 노드의 탐색이 끝나지 않았으므로 다음 프레임에 재탐색을 하기위해 스택에 되돌림
                if (result == Result.Running)
                {
                    stack.Push(current);
                    yield return null;
                }
            }

            isRunning = false;
        }

        #region Builder
        private Node _current;
        private Stack<Composite> _builingCompositeStack = new Stack<Composite>(); // 자식 빌드를 모두 완료하지 못한 스택을 차례로 쌓아둠

        public BehaviourTree Build()
        {
            blackboard = new Blackboard(gameObject);
            root = new Root(this);
            _current = root;
            return this;
        }

        public BehaviourTree Selector()
        {
            Composite composite = new Selector(this);
            Attach(_current, composite);
            return this;
        }

        public BehaviourTree Sequence()
        {
            Composite composite = new Sequence(this);
            Attach(_current, composite);
            return this;
        }

        public BehaviourTree Parallel(int successCount)
        {
            Composite composite = new Parallel(this, successCount);
            Attach(_current, composite);
            return this;
        }

        public BehaviourTree Decorator(Func<bool> condition)
        {
            Node node = new Decorator(this, condition);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree Execution(Func<Result> execute)
        {
            Node node = new Execution(this, execute);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree Seek(float radius, float height, float angle, LayerMask layerMask, float maxDistance)
        {
            Node node = new Seek(this, radius, height, angle, layerMask, maxDistance);
            Attach(_current, node);
            return this;
        }

        public BehaviourTree FinishCurrentComposite()
        {
            if (_builingCompositeStack.Count > 0)
                _builingCompositeStack.Pop();
            else
                throw new Exception("빌드할 컴포지트가 없어요..");

            if (_builingCompositeStack.Count > 0)
                _current = _builingCompositeStack.Peek();

            return this;
        }

        private void Attach(Node parent, Node child)
        {
            if (parent is IParent)
                ((IParent)parent).Attach(child);
            else
                throw new System.Exception($"{_current} 노드에는 자식을 붙여넣을 수 없습니다.");

            if (child is IParent)
            {
                if (child is Composite)
                    _builingCompositeStack.Push((Composite)child);

                _current = child;
            }
            else
            {
                _current = _builingCompositeStack.Count > 0 ? _builingCompositeStack.Peek() : null;
            }
        }
        #endregion
    }
}
