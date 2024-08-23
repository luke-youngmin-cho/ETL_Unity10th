namespace Demo.AISystems.BT
{
    /// <summary>
    /// 행동트리 노드 기반클래스
    /// </summary>
    public abstract class Node
    {
        public Node(BehaviourTree tree)
        {
            this.tree = tree;
            this.blackboard = tree.blackboard;
        }


        protected BehaviourTree tree;
        protected Blackboard blackboard;


        public abstract Result Invoke();
    }
}
