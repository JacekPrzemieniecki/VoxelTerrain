using System.Collections.Generic;

public class TerrainData
{
    private readonly Dictionary<Vector3i, bool> removedVoxels = new Dictionary<Vector3i, bool>();

    public bool SampleAt(Vector3i position)
    {
        if (removedVoxels.ContainsKey(position))
        {
            return removedVoxels[position];
        }
        return position.z < TerrainRoot.ChunksZ * TerrainChunk.ChunkSizeZ - 1 && position.z > 0 &&
               position.y < TerrainRoot.ChunksY * TerrainChunk.ChunkSizeY - 1 && position.y > 0 &&
               position.x < TerrainRoot.ChunksX * TerrainChunk.ChunkSizeX-1;
    }

    public bool RemoveIfCollides(Vector3i position)
    {
        if (!SampleAt(position))
        {
            return false;
        }
        removedVoxels[position] = false;
        return true;
    }
}