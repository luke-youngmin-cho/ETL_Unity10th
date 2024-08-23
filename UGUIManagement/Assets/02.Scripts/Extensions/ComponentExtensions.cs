using UnityEngine;

/*
 * 확장함수는 static 클래스에 static 멤버함수를 정의하고,
 * 첫번째 파라미터에 참조받을 객체타입으로 한뒤 this 키워드를 붙여준다.
 */

namespace Demo.Extensions
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// 자식을 재귀적으로 찾는 함수
        /// </summary>
        /// <param name="component"> root </param>
        /// <param name="name"> 찾을자식의 이름 </param>
        public static Transform FindChildWithName(this Component component, string name)
        {
            foreach (Transform child in component.transform)
            {
                if (child.name.Equals(name))
                {
                    return child;
                }
                else
                {
                    Transform grandChild = FindChildWithName(child, name);

                    if (grandChild)
                        return grandChild;
                }
            }

            return null;
        }
    }
}