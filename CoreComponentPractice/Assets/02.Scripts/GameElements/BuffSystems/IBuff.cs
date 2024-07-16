using UnityEngine;

namespace ETL10.GameElements.BuffSystems
{
    public interface IBuff
    {
        GameObject target { get; set; }
        float duration { get; }
        float startTimeMark { get; set; }
        bool IsTargetAvailable(GameObject target);
        void OnBegin();
        void OnDuration();
        void OnEnd();
    }
}
