using ETL10.GameElements.StatSystems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETL10.Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CharacterSpecification")]
    public class CharacterSpecification : ScriptableObject
    {
        [Serializable] // 사용자정의자료형은 기본적으로 직렬화 불가능하기때문에 Serializable 특성을 부여해야함
        public struct StatPair
        {
            public StatType type;
            public int value;
        }
        [field: SerializeField] public List<StatPair> statPairs { get; private set; }
    }
}