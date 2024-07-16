namespace ETL10.GameElements.StatSystems
{
    public class StatModifier
    {
        public StatModifier(StatType statType, StatModType modType, int modValue)
        {
            this.statType = statType;
            this.modType = modType;
            this.modValue = modValue;
        }

        public StatType statType { get; private set; }
        public StatModType modType { get; private set; }
        public int modValue { get; private set; }
    }
}
