using UnityEngine;

public class DestroySpawner : MonoBehaviour
{
    public float initVelocity;
    public GameObject Prefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var go = (GameObject) Instantiate(Prefab, transform.position, transform.rotation);
            go.GetComponent<Rigidbody>().velocity = transform.forward*initVelocity;
        }
    }
}