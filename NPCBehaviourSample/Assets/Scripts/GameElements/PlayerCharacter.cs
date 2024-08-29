using Fusion;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    //public PlayerCharacter GetByPlayer(PlayerRef player) => playerCharacters.Get(player);
    //[Networked, Capacity(20)]
    //public NetworkDictionary<PlayerRef, PlayerCharacter> playerCharacters { get; }

    void AttackHit(AnimationEvent e)
    {
        SkillData skillData = e.objectReferenceParameter as SkillData;
        test_skillData = skillData;
        
        if (Runner.GetPhysicsScene().CapsuleCast(transform.position + transform.rotation * skillData.p1Center,
                                                 transform.position + transform.rotation * skillData.p2Center,
                                                 skillData.radius,
                                                 skillData.direction,
                                                 out RaycastHit hit,
                                                 skillData.distance,
                                                 skillData.targetMask))
        {
            
            if (hit.collider.transform.parent.TryGetComponent(out IHp hp))
            {
                hp.Damage(skillData.damageAmount);
            }
        }
    }

    private SkillData test_skillData;

    private void OnDrawGizmos()
    {
        if (test_skillData == null)
            return;

        // Gizmo 색상 설정
        Gizmos.color = Color.red;

        // 월드 좌표에서 p1, p2 계산
        Vector3 p1 = transform.position + transform.rotation * test_skillData.p1Center;
        Vector3 p2 = transform.position + transform.rotation * test_skillData.p2Center;

        // Capsule Gizmo 그리기
        DrawCapsule(p1, p2, test_skillData.radius);

        // 공격 방향 벡터 그리기
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(p1, p1 + transform.rotation * test_skillData.direction * test_skillData.distance);
        Gizmos.DrawLine(p2, p2 + transform.rotation * test_skillData.direction * test_skillData.distance);
    }

    private void DrawCapsule(Vector3 p1, Vector3 p2, float radius)
    {
        // 구의 두 끝점을 그려주는 함수
        Gizmos.DrawWireSphere(p1, radius);
        Gizmos.DrawWireSphere(p2, radius);

        // 원통 부분을 그려주는 함수
        Gizmos.DrawLine(p1 + Vector3.up * radius, p2 + Vector3.up * radius);
        Gizmos.DrawLine(p1 - Vector3.up * radius, p2 - Vector3.up * radius);
        Gizmos.DrawLine(p1 + Vector3.right * radius, p2 + Vector3.right * radius);
        Gizmos.DrawLine(p1 - Vector3.right * radius, p2 - Vector3.right * radius);
    }
}