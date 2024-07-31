using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class MotionTrailGenerator : MonoBehaviour
{
    public bool isOn { get; private set; }

    /// <summary>
    /// �ܻ� 
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class DummyMesh : MonoBehaviour
    {
        public MotionTrailGenerator generator { get; set; }
        public float lifeTime { get; set; }
        public MeshFilter meshFilter { get; private set; }
        public MeshRenderer meshRenderer { get; private set; }
        private float _timer;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (_timer < 0)
                Off();
            else
                _timer -= Time.deltaTime;
        }

        public void On()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            _timer = lifeTime;
            transform.parent = null;
            meshRenderer.enabled = true;
        }

        public void Off()
        {
            generator.Return(this);
            transform.parent = generator.transform;
            meshRenderer.enabled = false;
        }
    }

    private Stack<DummyMesh> _stack;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _material;
    [Range(1f, 30f)]
    [SerializeField] private float _rate; // �ʴ� �ܻ� ��
    [Range(0.01f, 20f)]
    [SerializeField] private float _lifeTime; // �ܻ� �����ֱ�
    [Range(1, 100)]
    [SerializeField] private int _capacity; // �ܻ� Ǯ�� �뷮
    private float _timer;

    private void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _stack = new Stack<DummyMesh>(_capacity);

        for (int i = 0; i < _capacity; i++)
        {
            DummyMesh dummyMesh = new GameObject("MotionTrailDummyMesh").AddComponent<DummyMesh>();
            dummyMesh.generator = this;
            dummyMesh.lifeTime = _lifeTime;
            dummyMesh.meshRenderer.material = _material;
            _stack.Push(dummyMesh);
        }
    }

    private void Start()
    {
        On(); // Just for testing.
    }

    private void Update()
    {
        if (isOn)
            Generate();
    }

    /// <summary>
    /// Ǯ���� ���� ������ �ܻ��� ������
    /// </summary>
    private DummyMesh Get()
    {
        DummyMesh dummyMesh = _stack.Pop();
        _skinnedMeshRenderer.BakeMesh(dummyMesh.meshFilter.mesh);
        dummyMesh.On();
        return dummyMesh;
    }

    /// <summary>
    /// �ܻ��� Ǯ�� ��ȯ
    /// </summary>
    private void Return(DummyMesh dummyMesh)
    {
        _stack.Push(dummyMesh);
    }

    public void On() => isOn = true;
    
    public void Off() => isOn = false;

    private void Generate()
    {
        if (_timer < 0)
        {
            if (_stack.Count > 0)
            {
                Get();
                _timer = 1.0f / _rate;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
