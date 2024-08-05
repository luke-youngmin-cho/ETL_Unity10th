using System;
using System.Reflection;

namespace Demo.Singleton
{
    public abstract class Singleton<T>
        where T : Singleton<T>
    {
        public static T instance
        {
            get
            {
                if (s_instance == null)
                {
                    /* Reflection : 
                     * 런타임중에 메타데이터에 접근하는 기능
                     */
                    //ConstructorInfo constructorInfo = typeof(T).GetConstructor(null);
                    //
                    //if (constructorInfo != null)
                    //    s_instance = (T)constructorInfo.Invoke(null);

                    s_instance = (T)Activator.CreateInstance(typeof(T));
                }

                return s_instance;
            }
        }

        private static T s_instance;
    }
}
