using System.Collections.Generic;

namespace Demo.AISystems.BT
{
    public interface IParentOfChildren : IParent
    {
        List<Node> children { get; set; }
    }
}
