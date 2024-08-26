namespace Demo.AISystems.BT
{
    public interface IParentOfChild : IParent
    {
        Node child { get; set; }
    }
}
