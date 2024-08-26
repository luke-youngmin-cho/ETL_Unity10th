using System.Collections.Generic;

namespace Demo.AISystems.BT
{
    public abstract class Composite : Node, IParentOfChildren
    {
        public Composite(BehaviourTree tree) : base(tree)
        {
            children = new List<Node>();
        }

        public List<Node> children { get; set; }
        protected int currentChildIndex; // 자식이 Running 반환 이후 빠져나올때, 다음 자식을 Invoke 하기위한 용도

        public void Attach(Node child)
        {
            children.Add(child);
        }
    }
}
