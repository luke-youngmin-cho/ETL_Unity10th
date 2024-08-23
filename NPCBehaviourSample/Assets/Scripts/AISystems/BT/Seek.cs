using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AISystems.BT
{
    public class Seek : Node
    {
        public Seek(BehaviourTree tree, float radius, float height, float angle, LayerMask targetMask, float maxDistance) : base(tree)
        {
            _radius = radius;
            _height = height;
            _angle = angle;
            _targetMask = targetMask;
            _maxDistance = maxDistance;
        }

        private float _radius;
        private float _height;
        private float _angle;
        private LayerMask _targetMask;
        private float _maxDistance;

        public override Result Invoke()
        {
            // 감지된 목표가 있으면
            if (blackboard.target)
            {
                // 나와 목표와의 거리
                float distance = Vector3.Distance(blackboard.transform.position,
                                                  blackboard.target.position);

                // 목표에 도달 했을때
                if (distance <= blackboard.agent.stoppingDistance)
                {
                    return Result.Success;
                }
                // 목표 아직 추적중일때
                else if (distance < _maxDistance)
                {
                    blackboard.agent.SetDestination(blackboard.target.position);
                    return Result.Running;
                }
                // 목표가 추적 최대범위를 벗어났을때
                else
                {
                    blackboard.target = null;
                    blackboard.agent.ResetPath();
                    return Result.Failure;
                }
            }
            // 감지된 목표가 없으면
            else
            {
                Collider[] cols =
                    Physics.OverlapCapsule(blackboard.transform.position,
                                           blackboard.transform.position + Vector3.up * _height,
                                           _radius,
                                           _targetMask);

                if (cols.Length > 0)
                {
                    if (IsInSight(cols[0].transform))
                    {
                        blackboard.target = cols[0].transform;
                        blackboard.agent.SetDestination(blackboard.target.position);
                        return Result.Running;
                    }
                }
            }

            return Result.Failure;
        }

        private bool IsInSight(Transform target)
        {
            Vector3 origin = blackboard.transform.position; // 내 위치
            Vector3 forward = blackboard.transform.forward; // 내 앞쪽 방향 벡터
            Vector3 lookDir = (target.position - origin).normalized; // 타겟을 바라보는 방향벡터
            float theta = Mathf.Acos(Vector3.Dot(forward, lookDir)) * Mathf.Rad2Deg; // 사이각

            if (theta < _angle / 2.0f)
            {
                // 중간에 장애물 있는지
                if (Physics.Raycast(origin + Vector3.up * _height / 2.0f,
                                    lookDir,
                                    out RaycastHit hit,
                                    Vector3.Distance(origin, target.position),
                                    _targetMask))
                {
                    return hit.collider.transform == target;
                }
            }

            return false;
        }
    }
}