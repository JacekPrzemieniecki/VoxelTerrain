using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (MeshRenderer))]
[RequireComponent(typeof (MeshFilter))]
public class TerrainChunk : MonoBehaviour
{
    public const int ChunkSizeX = 8;
    public const int ChunkSizeY = 16;
    public const int ChunkSizeZ = 8;
    private Mesh _mesh;

    private const int MaxFaceCount = ChunkSizeX*ChunkSizeY*ChunkSizeZ*6;
    private static readonly List<Vector3> WorkingVectors = new List<Vector3>(MaxFaceCount*4);
    private static readonly List<Vector3> WorkingNormals = new List<Vector3>(MaxFaceCount*4);
    private static List<int> workingTriangles = new List<int>(MaxFaceCount*2);

    private void Awake()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    public ChunkData Data;

    public void Regenerate()
    {
        //GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
        transform.position = Data.Offset;
        _mesh.Clear();
        WorkingVectors.Clear();
        workingTriangles.Clear();
        WorkingNormals.Clear();

        Vector3i yVec = new Vector3i(0, 1, 0);
        Vector3i zVec = new Vector3i(0, 0, 1);
        Vector3i xVec = new Vector3i(1, 0, 0);
        GeneratePlanes(Data, zVec, yVec, xVec, ChunkSizeZ, ChunkSizeY, ChunkSizeX);
        GeneratePlanes(Data, yVec, xVec, zVec, ChunkSizeY, ChunkSizeX, ChunkSizeZ);
        GeneratePlanes(Data, xVec, zVec, yVec, ChunkSizeX, ChunkSizeZ, ChunkSizeY);
        _mesh.vertices = WorkingVectors.ToArray();
        _mesh.normals = WorkingNormals.ToArray();
        _mesh.triangles = workingTriangles.ToArray();
    }

    private static void GeneratePlanes(ChunkData data,
                                        Vector3i right, Vector3i up, Vector3i forward,
                                        int sizeRigth, int sizeUp, int sizeForward)
    {
        for (int z = 0; z < sizeRigth; z++)
        {
            Vector3i zPos = right*z;
            for (int y = 0; y < sizeUp; y++)
            {
                Vector3i zyPos = zPos + up*y;
                bool previousFull = data.LocalSampleAt(zyPos - forward);
                for (int x = 0; x < sizeForward; x++)
                {
                    Vector3i pos = zyPos + x*forward;
                    bool full = data.LocalSampleAt(pos);
                    bool generateBack = !full && previousFull;
                    bool generateFront = full && !previousFull;

                    if (generateBack)
                    {
                        GenerateFace(pos, up, right, forward);
                    }
                    else if (generateFront)
                    {
                        GenerateFace(pos + right, up, -1*right, -1*forward);
                    }

                    // Generate the outer face of the chunk
                    previousFull = full;
                }
            }
        }
    }

    private static void GenerateFace(Vector3 root, Vector3 up, Vector3 right, Vector3 forward)
    {
        int vertexOffset = WorkingVectors.Count;
        WorkingVectors.Add(root);
        WorkingVectors.Add(root + up);
        WorkingVectors.Add(root + right);
        WorkingVectors.Add(root + up + right);

        for (int i = 0; i < 4; i++)
        {
            WorkingNormals.Add(forward);
        }

        workingTriangles.AddRange(new[]
        {
            vertexOffset,
            vertexOffset + 1,
            vertexOffset + 2,
            vertexOffset + 2,
            vertexOffset + 1,
            vertexOffset + 3,
        });
    }
}