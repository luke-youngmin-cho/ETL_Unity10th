using System.Collections.Generic;

namespace Demo.AISystems.BT
{
    public interface IParentOfChildren
    {
        List<Node> children { get; set; }
    }
}
