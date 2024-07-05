using ETL10.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace ETL10.Test
{
    [RequireComponent(typeof(GameObjectPool))]
    public class GameObjectSpawner : MonoBehaviour
    {
        private GameObjectPool _pool;
        private Queue<GameObject> _queue = new Queue<GameObject>(100);

        private void Awake()
        {
            _pool = GetComponent<GameObjectPool>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                _queue.Enqueue(_pool.Get()); // 풀에서 가져와서 큐에 집어넣음
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_queue.Count > 0)
                {
                    _pool.Release(_queue.Dequeue()); // 큐에서 빼서 풀에 집어넣음
                }
            }
        }
    }
}