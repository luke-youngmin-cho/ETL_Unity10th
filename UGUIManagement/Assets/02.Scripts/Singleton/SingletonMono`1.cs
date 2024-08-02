using UnityEngine;

namespace Demo.Singleton
{
    public abstract class SingletonMono<T> : MonoBehaviour
        where T : SingletonMono<T>
    {
        public static T instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new GameObject(nameof(T)).AddComponent<T>();
                }

                return s_instance;
            }
        }

        private static T s_instance;
    }
}
