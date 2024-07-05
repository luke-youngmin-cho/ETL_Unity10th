using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnumerationSample
{
    void main()
    {
        List<int> list = new List<int>();

        foreach (int i in list)
        {
            Console.WriteLine(i);
        }

        IEnumerator<int> e1 = list.GetEnumerator();
        while (e1.MoveNext())
        {
            Console.WriteLine(e1.Current);
        }
        e1.Dispose();

        using (IEnumerator<int> e2 = list.GetEnumerator())
        {
            while (e2.MoveNext())
            {
                Console.WriteLine(e2.Current);
            }
        }
    }
}

namespace ETL10.ObjectPool
{
    /// <summary>
    /// ���������� GameObject �� Ǯ���ϱ����� Ŭ����
    /// </summary>
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] GameObject _item;
        [SerializeField] int _capacity = 10;
        [SerializeField] int _maxSize = 100;

        /// <summary>
        /// Stack : �����迭 ���, ���Լ��ⱸ��. 
        /// LinkedList : �ܹ��� ���Ḯ��Ʈ ���, ���Լ��ⱸ��. ������ ��ȯ�� First �� ����.
        /// </summary>
        private enum PoolType
        {
            Stack,
            LinkedList
        }
        [SerializeField] PoolType _poolType = PoolType.Stack;
        private IObjectPool<GameObject> _pool;


        private void Awake()
        {
            InitializePool();
        }


        /// <summary>
        /// ������ ��������
        /// </summary>
        /// <returns> Ǯ���� ������ </returns>
        public GameObject Get()
        {
            return _pool.Get();
        }

        /// <summary>
        /// ������ �ݳ��ϱ�
        /// </summary>
        /// <param name="item"> Ǯ���� ������ </param>
        public void Release(GameObject item)
        {
            _pool.Release(item);
        }

        /// <summary>
        /// PoolType �� ���� Pool �ʱ�ȭ
        /// </summary>
        void InitializePool()
        {
            if (_poolType == PoolType.Stack)
            {
                _pool = new ObjectPool<GameObject>(CreatePooledItem,
                                                   OnGet,
                                                   OnRelease,
                                                   OnRemove,
                                                   true,
                                                   _capacity,
                                                   _maxSize);
            }
            else
            {
                _pool = new LinkedPool<GameObject>(CreatePooledItem,
                                                   OnGet,
                                                   OnRelease,
                                                   OnRemove,
                                                   true,
                                                   _maxSize);
            }
        }

        /// <summary>
        /// Ǯ���� ������ ����
        /// </summary>
        /// <returns> ������ Ǯ���� ������ </returns>
        GameObject CreatePooledItem()
        {
            return Instantiate(_item);
        }

        /// <summary>
        /// Ǯ���� �������� ������ ���� ������ ����
        /// </summary>
        /// <param name="pooledItem"> �����پ����� ������ </param>
        void OnGet(GameObject pooledItem)
        {
            pooledItem.SetActive(true);
        }

        /// <summary>
        /// Ǯ���� �������� �ݳ��Ҷ� ������ ����
        /// </summary>
        /// <param name="pooledItem"> �ݳ��Ϸ��� ������ </param>
        void OnRelease(GameObject pooledItem)
        {
            pooledItem.SetActive(false);
        }

        /// <summary>
        /// Ǯ���� �������� Ǯ ��Ͽ��� �����Ҷ� ������ ����
        /// </summary>
        /// <param name="pooledItem"> �����Ϸ��� ������ </param>
        void OnRemove(GameObject pooledItem)
        {
            Destroy(pooledItem);
        }
    }
}