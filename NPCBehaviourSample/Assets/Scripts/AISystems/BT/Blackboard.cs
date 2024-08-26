using UnityEngine;
using UnityEngine.AI;

namespace Demo.AISystems.BT
{
    /// <summary>
    /// 각 노드들은 데이터를 서로 데이터를 공유하기 힘드므로 (의존성이 증가하므로) 
    /// 칠판에 공유해야하는 데이터를 써놓고 가져다쓰는형태
    /// </summary>
    public class Blackboard
    {
        public Blackboard(GameObject owner)
        {
            transform = owner.transform;
            agent = owner.GetComponent<NavMeshAgent>();
        }


        // owner
        public Transform transform { get; set; }
        public NavMeshAgent agent { get; set; }

        // target
        public Transform target { get; set; }
    }
}
