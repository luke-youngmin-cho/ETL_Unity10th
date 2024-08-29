using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SkillData")]
public class SkillData : ScriptableObject
{
    [field: SerializeField] public int damageAmount { get; private set; }
    [field: SerializeField] public Vector3 p1Center { get; private set; }
    [field: SerializeField] public Vector3 p2Center { get; private set; }
    [field: SerializeField] public float radius { get; private set; }
    [field: SerializeField] public Vector3 direction { get; private set; }
    [field: SerializeField] public float distance { get; private set; }
    [field: SerializeField] public LayerMask targetMask { get; private set; }
}