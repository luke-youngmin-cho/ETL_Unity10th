namespace Demo.AISystems.BT
{
    public class Parallel : Composite
    {
        public Parallel(BehaviourTree tree, int successCountRequired) : base(tree)
        {
            _successCountRequired = successCountRequired;
        }


        private int _successCountRequired; // 성공 정책. 이 개수 이상 성공해야 성공 반환
        private int _successCount;


        public override Result Invoke()
        {
            Result result = Result.Failure;

            for (int i = currentChildIndex; i < children.Count; i++)
            {
                result = children[i].Invoke();

                switch (result)
                {
                    case Result.Success:
                        {
                            _successCount++;
                        }
                        break;
                    case Result.Failure:
                        {
                        }
                        break;
                    case Result.Running:
                        {
                            return result;
                        }
                    default:
                        throw new System.Exception($"Invalid result code : {result}");
                }
            }

            result = _successCount >= _successCountRequired ? Result.Success : Result.Failure;
            _successCount = 0;
            currentChildIndex = 0;
            return result;
        }
    }
}
