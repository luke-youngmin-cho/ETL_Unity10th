namespace Demo.AISystems.BT
{
    /// <summary>
    /// 최상위 노드. 자식을 이어서 순회하도록함.
    /// </summary>
    public class Root : Node, IParentOfChild
    {
        public Root(BehaviourTree tree) : base(tree)
        {
        }

        public Node child { get; set; }


        public override Result Invoke()
        {
            tree.stack.Push(child);
            return Result.Success;
        }
    }
}
