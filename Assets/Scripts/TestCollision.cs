using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision! {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger! {other.gameObject.name}");
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 look = transform.TransformDirection(Vector3.forward); // Local -> World
        //RaycastUtil.Raycast(transform.position + Vector3.up, look, 10, (hit) => { Debug.Log($"RayCast! {hit.collider.gameObject.name}"); }, true);
        RaycastUtil.RaycastAll(transform.position + Vector3.up
                                , look
                                , 10
                                , (hit) => { Debug.Log($"RayCast! {hit.collider.gameObject.name}"); }
                                , true);
    }
}
