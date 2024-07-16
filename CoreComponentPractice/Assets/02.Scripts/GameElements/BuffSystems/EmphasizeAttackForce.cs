using ETL10.GameElements.StatSystems;
using UnityEngine;

namespace ETL10.GameElements.BuffSystems
{
    public class EmphasizeAttackForce : IBuff
    {
        public EmphasizeAttackForce(float duration, int emphasizeAmount)
        {
            this.duration = duration;
            _modifier = new StatModifier(StatType.AttackForce, StatModType.AddFlat, emphasizeAmount);
        }

        public GameObject target { get; set; }

        public float duration { get; private set; }
        public float startTimeMark { get; set; }

        private StatModifier _modifier;

        public bool IsTargetAvailable(GameObject target)
        {
            if (target.TryGetComponent(out IStatsContainer statsContainer))
            {
                if (statsContainer.stats.ContainsKey(StatType.AttackForce))
                    return true;
            }

            return false;
        }

        public void OnBegin()
        {
            target.GetComponent<IStatsContainer>()
                  .stats[StatType.AttackForce]
                  .AddModifier(_modifier);
        }

        public void OnDuration()
        {
        }

        public void OnEnd()
        {
            target.GetComponent<IStatsContainer>()
                  .stats[StatType.AttackForce]
                  .RemoveModifier(_modifier);
        }
    }
}
