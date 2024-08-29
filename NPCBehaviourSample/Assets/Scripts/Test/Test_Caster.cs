using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Caster : MonoBehaviour
{
    private void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(Vector3.zero, 10f);
        for (int i = 0; i < cols.Length; i++)
        {
            Debug.Log($"{cols[i].name}");
        }
    }
}
