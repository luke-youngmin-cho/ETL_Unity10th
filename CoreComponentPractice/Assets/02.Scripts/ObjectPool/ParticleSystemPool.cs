using UnityEngine;
using UnityEngine.Pool;

namespace ETL10.ObjectPool
{
    /// <summary>
    /// 범용적으로 ParticleSystem 을 풀링하기위한 클래스
    /// </summary>
    public class ParticleSystemPool : MonoBehaviour
    {
        /// <summary>
        /// 파티클시스템이 종료될때 콜백을 받아서 풀로 되돌아 가게 하는 컴포넌트
        /// </summary>
        public class ReturnToPool : MonoBehaviour
        {
            public IObjectPool<ParticleSystem> pool { get; set; }

            private ParticleSystem _particleSystem;

            private void Awake()
            {
                _particleSystem = GetComponent<ParticleSystem>();
                var mainModule = _particleSystem.main;
                mainModule.stopAction = ParticleSystemStopAction.Callback; // stopAction 이 콜백일때만 OnParticleSystemStopped() 콜백이 호출됨.
            }

            private void OnParticleSystemStopped()
            {
                pool.Release(_particleSystem);
            }
        }

        [SerializeField] ParticleSystem _item;
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
        private IObjectPool<ParticleSystem> _pool;


        private void Awake()
        {
            InitializePool();
        }

        /// <summary>
        /// 아이템 가져오기
        /// </summary>
        /// <returns> 풀링된 아이템 </returns>
        public ParticleSystem Get()
        {
            return _pool.Get();
        }

        /// <summary>
        /// 아이템 반납하기
        /// </summary>
        /// <param name="item"> 풀링될 아이템 </param>
        public void Release(ParticleSystem item)
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
                _pool = new ObjectPool<ParticleSystem>(CreatePooledItem,
                                                       OnGet,
                                                       OnRelease,
                                                       OnRemove,
                                                       true,
                                                       _capacity,
                                                       _maxSize);
            }
            else
            {
                _pool = new LinkedPool<ParticleSystem>(CreatePooledItem,
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
        ParticleSystem CreatePooledItem()
        {
            ParticleSystem particleSystem = Instantiate(_item);
            particleSystem.gameObject.AddComponent<ReturnToPool>();
            return particleSystem;
        }

        /// <summary>
        /// 풀링된 아이템을 가져다 쓸때 수행할 내용
        /// </summary>
        /// <param name="pooledItem"> 가져다쓰려는 아이템 </param>
        void OnGet(ParticleSystem pooledItem)
        {
            pooledItem.gameObject.SetActive(true);
            pooledItem.Play();
        }

        /// <summary>
        /// 풀링된 아이템을 반납할때 수행할 내용
        /// </summary>
        /// <param name="pooledItem"> 반납하려는 아이템 </param>
        void OnRelease(ParticleSystem pooledItem)
        {
            pooledItem.gameObject.SetActive(false);
        }

        /// <summary>
        /// 풀링된 아이템을 풀 목록에서 제거할때 수행할 내용
        /// </summary>
        /// <param name="pooledItem"> 제거하려는 아이템 </param>
        void OnRemove(ParticleSystem pooledItem)
        {
            Destroy(pooledItem.gameObject);
        }
    }
}