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
                _queue.Enqueue(_pool.Get()); // Ǯ���� �����ͼ� ť�� �������
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_queue.Count > 0)
                {
                    _pool.Release(_queue.Dequeue()); // ť���� ���� Ǯ�� �������
                }
            }
        }
    }
}