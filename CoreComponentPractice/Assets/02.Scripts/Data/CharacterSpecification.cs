using ETL10.GameElements.StatSystems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETL10.Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CharacterSpecification")]
    public class CharacterSpecification : ScriptableObject
    {
        [Serializable] // ����������ڷ����� �⺻������ ����ȭ �Ұ����ϱ⶧���� Serializable Ư���� �ο��ؾ���
        public struct StatPair
        {
            public StatType type;
            public int value;
        }
        [field: SerializeField] public List<StatPair> statPairs { get; private set; }
    }
}