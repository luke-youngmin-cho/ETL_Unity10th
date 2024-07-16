using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETL10.GameElements.BuffSystems
{
    public class BuffController : MonoBehaviour
    {
        private List<IBuff> _buffs = new List<IBuff>();

        public void Register(IBuff buff)
        {
            if (buff.IsTargetAvailable(gameObject))
            {
                buff.target = gameObject;
                _buffs.Add(buff);
                StartCoroutine(C_BuffRoutine(buff));
            }
        }

        IEnumerator C_BuffRoutine(IBuff buff)
        {
            buff.startTimeMark = Time.time;
            buff.OnBegin();

            while (Time.time - buff.startTimeMark < buff.duration)
            {
                buff.OnDuration();
                yield return null;
            }

            buff.OnEnd();
            _buffs.Remove(buff);
        }
    }
}
