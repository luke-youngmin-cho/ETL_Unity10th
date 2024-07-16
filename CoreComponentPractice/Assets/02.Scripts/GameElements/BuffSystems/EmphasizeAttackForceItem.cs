using UnityEngine;

namespace ETL10.GameElements.BuffSystems
{
    public class EmphasizeAttackForceItem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BuffController controller))
            {
                controller.Register(new EmphasizeAttackForce(20f, 30));
                Destroy(gameObject);
            }
        }
    }
}
