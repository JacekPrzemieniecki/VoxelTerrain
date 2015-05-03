using UnityEngine;

public class VoxelDestroyer : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5);        
    }

    private void Update()
    {
        if (TerrainRoot.RemoveVoxelAt(transform.position))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (TerrainRoot.RemoveVoxelAt(transform.position))
        {
            Destroy(gameObject);
        }
    }
}