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
    /// 범용적으로 GameObject 를 풀링하기위한 클래스
    /// </summary>
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] GameObject _item;
        [SerializeField] int _capacity = 10;
        [SerializeField] int _maxSize = 100;

        /// <summary>
        /// Stack : 동적배열 기반, 후입선출구조. 
        /// LinkedList : 단방향 연결리스트 기반, 후입선출구조. 아이템 반환시 First 에 삽입.
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
        /// 아이템 가져오기
        /// </summary>
        /// <returns> 풀링된 아이템 </returns>
        public GameObject Get()
        {
            return _pool.Get();
        }

        /// <summary>
        /// 아이템 반납하기
        /// </summary>
        /// <param name="item"> 풀링될 아이템 </param>
        public void Release(GameObject item)
        {
            _pool.Release(item);
        }

        /// <summary>
        /// PoolType 에 따른 Pool 초기화
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
        /// 풀링할 아이템 생성
        /// </summary>
        /// <returns> 생성된 풀링할 아이템 </returns>
        GameObject CreatePooledItem()
        {
            return Instantiate(_item);
        }

        /// <summary>
        /// 풀링된 아이템을 가져다 쓸때 수행할 내용
        /// </summary>
        /// <param name="pooledItem"> 가져다쓰려는 아이템 </param>
        void OnGet(GameObject pooledItem)
        {
            pooledItem.SetActive(true);
        }

        /// <summary>
        /// 풀링된 아이템을 반납할때 수행할 내용
        /// </summary>
        /// <param name="pooledItem"> 반납하려는 아이템 </param>
        void OnRelease(GameObject pooledItem)
        {
            pooledItem.SetActive(false);
        }

        /// <summary>
        /// 풀링된 아이템을 풀 목록에서 제거할때 수행할 내용
        /// </summary>
        /// <param name="pooledItem"> 제거하려는 아이템 </param>
        void OnRemove(GameObject pooledItem)
        {
            Destroy(pooledItem);
        }
    }
}