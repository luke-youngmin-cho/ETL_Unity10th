using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

namespace ETL10.ObjectPool
{
    public enum PoolTag
    {
        Bullet,
        BulletExplosionEffect,
        BloodEffect,
    }

    public static class Pool
    {
        static Dictionary<PoolTag, IObjectPool<GameObject>> s_poolTable;

        public static GameObject Get(PoolTag tag)
        {
            return s_poolTable[tag].Get();
        }

        public static void Release(PoolTag tag, GameObject pooledItem)
        {
            s_poolTable[tag].Release(pooledItem);
        }
    }
}
