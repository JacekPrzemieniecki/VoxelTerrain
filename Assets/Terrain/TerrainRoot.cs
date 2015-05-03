using UnityEngine;

public class TerrainRoot : MonoBehaviour
{
    public const int ChunksX = 30;
    public const int ChunksY = 3;
    public const int ChunksZ = 30;
    private static TerrainRoot _instance;
    private TerrainChunk[][][] _chunks;
    private TerrainChunk _tempChunk;
    private TerrainData _terrainData;
    public GameObject ChunkPrefab;

    public static bool RemoveVoxelAt(Vector3 position)
    {
        var pos = new Vector3i((int) position.x, (int) position.y, (int) position.z);
        return _instance.RemoveIfCollides(pos);
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _chunks = new TerrainChunk[ChunksX][][];
        _terrainData = new TerrainData();
        for (var x = 0; x < ChunksX; x++)
        {
            _chunks[x] = new TerrainChunk[ChunksY][];

            for (var y = 0; y < ChunksY; y++)
            {
                _chunks[x][y] = new TerrainChunk[ChunksZ];
                for (var z = 0; z < ChunksZ; z++)
                {
                    var go = Instantiate(ChunkPrefab);
                    var newChunk = go.GetComponent<TerrainChunk>();
                    _chunks[x][y][z] = newChunk;
                    newChunk.Data = new ChunkData(_terrainData,
                        new Vector3i(x*TerrainChunk.ChunkSizeX, y*TerrainChunk.ChunkSizeY, z*TerrainChunk.ChunkSizeZ));
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void Start()
    {
        foreach (var chunkArrOfArr in _chunks)
        {
            foreach (var chunkArr in chunkArrOfArr)
            {
                foreach (var terrainChunk in chunkArr)
                {
                    terrainChunk.Regenerate();
                }
            }
        }
    }

    private TerrainChunk GetChunkForPosition(Vector3i position)
    {
        var x = position.x/TerrainChunk.ChunkSizeX;
        var y = position.y/TerrainChunk.ChunkSizeY;
        var z = position.z/TerrainChunk.ChunkSizeZ;
        if (x < 0 || y < 0 || z < 0 || x > ChunksX - 1 || y > ChunksY - 1 || z > ChunksZ - 1)
        {
            return null;
        }
        return _chunks[x][y][z];
    }

    private void RegenerateNeighbors(Vector3i position)
    {
        var localX = position.x%TerrainChunk.ChunkSizeX;
        if (localX == 0)
        {
            position.x -= 1;
            RegenerateAtPosition(position);
        }
        if (localX == TerrainChunk.ChunkSizeX - 1)
        {
            position.x += 1;
            RegenerateAtPosition(position);
        }

        var localY = position.y%TerrainChunk.ChunkSizeY;
        if (localY == 0)
        {
            position.y -= 1;
            RegenerateAtPosition(position);
        }
        if (localY == TerrainChunk.ChunkSizeY - 1)
        {
            position.y += 1;
            RegenerateAtPosition(position);
        }

        var localZ = position.z%TerrainChunk.ChunkSizeZ;
        if (localZ == 0)
        {
            position.z -= 1;
            RegenerateAtPosition(position);
        }
        if (localZ == TerrainChunk.ChunkSizeZ - 1)
        {
            position.z += 1;
            RegenerateAtPosition(position);
        }
    }

    private void RegenerateAtPosition(Vector3i position)
    {
        var neighbor = GetChunkForPosition(position);
        if (neighbor != null)
        {
            neighbor.Regenerate();
        }
    }

    public bool RemoveIfCollides(Vector3i position)
    {
        if (_terrainData.RemoveIfCollides(position))
        {
            var chunk = GetChunkForPosition(position);
            chunk.Regenerate();
            RegenerateNeighbors(position);
            return true;
        }

        return false;
    }
}