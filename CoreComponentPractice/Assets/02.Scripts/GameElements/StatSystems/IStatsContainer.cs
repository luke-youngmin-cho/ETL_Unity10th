using System.Collections.Generic;

namespace ETL10.GameElements.StatSystems
{
    public interface IStatsContainer
    {
        Dictionary<StatType, Stat> stats { get; }
    }
}
