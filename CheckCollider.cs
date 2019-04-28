using UnityEngine;
using System.Collections;

public class CheckCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {

        Debug.Log("Interaction!!");

        if (col.gameObject.name == "Ellen")
        {
            Debug.Log("Interaction with Ellen!!");
        }
    }
}
