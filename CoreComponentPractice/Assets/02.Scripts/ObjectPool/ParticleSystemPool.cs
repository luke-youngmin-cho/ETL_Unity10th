using UnityEngine;
using UnityEngine.Pool;

namespace ETL10.ObjectPool
{
    /// <summary>
    /// ���������� ParticleSystem �� Ǯ���ϱ����� Ŭ����
    /// </summary>
    public class ParticleSystemPool : MonoBehaviour
    {
        /// <summary>
        /// ��ƼŬ�ý����� ����ɶ� �ݹ��� �޾Ƽ� Ǯ�� �ǵ��� ���� �ϴ� ������Ʈ
        /// </summary>
        public class ReturnToPool : MonoBehaviour
        {
            public IObjectPool<ParticleSystem> pool { get; set; }

            private ParticleSystem _particleSystem;

            private void Awake()
            {
                _particleSystem = GetComponent<ParticleSystem>();
                var mainModule = _particleSystem.main;
                mainModule.stopAction = ParticleSystemStopAction.Callback; // stopAction �� �ݹ��϶��� OnParticleSystemStopped() �ݹ��� ȣ���.
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
        /// Stack : �����迭 ���, ���Լ��ⱸ��. 
        /// LinkedList : �ܹ��� ���Ḯ��Ʈ ���, ���Լ��ⱸ��. ������ ��ȯ�� First �� ����.
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
        /// ������ ��������
        /// </summary>
        /// <returns> Ǯ���� ������ </returns>
        public ParticleSystem Get()
        {
            return _pool.Get();
        }

        /// <summary>
        /// ������ �ݳ��ϱ�
        /// </summary>
        /// <param name="item"> Ǯ���� ������ </param>
        public void Release(ParticleSystem item)
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
        /// Ǯ���� ������ ����
        /// </summary>
        /// <returns> ������ Ǯ���� ������ </returns>
        ParticleSystem CreatePooledItem()
        {
            ParticleSystem particleSystem = Instantiate(_item);
            particleSystem.gameObject.AddComponent<ReturnToPool>();
            return particleSystem;
        }

        /// <summary>
        /// Ǯ���� �������� ������ ���� ������ ����
        /// </summary>
        /// <param name="pooledItem"> �����پ����� ������ </param>
        void OnGet(ParticleSystem pooledItem)
        {
            pooledItem.gameObject.SetActive(true);
            pooledItem.Play();
        }

        /// <summary>
        /// Ǯ���� �������� �ݳ��Ҷ� ������ ����
        /// </summary>
        /// <param name="pooledItem"> �ݳ��Ϸ��� ������ </param>
        void OnRelease(ParticleSystem pooledItem)
        {
            pooledItem.gameObject.SetActive(false);
        }

        /// <summary>
        /// Ǯ���� �������� Ǯ ��Ͽ��� �����Ҷ� ������ ����
        /// </summary>
        /// <param name="pooledItem"> �����Ϸ��� ������ </param>
        void OnRemove(ParticleSystem pooledItem)
        {
            Destroy(pooledItem.gameObject);
        }
    }
}