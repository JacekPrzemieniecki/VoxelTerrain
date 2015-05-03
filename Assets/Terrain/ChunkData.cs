public class ChunkData
{
    public ChunkData(TerrainData data, Vector3i offset)
    {
        Offset = offset;
        this.data = data;
    }

    public Vector3i Offset;

    private TerrainData data;

    public void RemoveVoxel(Vector3i position)
    {
        data.RemoveIfCollides(position);
    }

    public bool LocalSampleAt(Vector3i position)
    {
        var globalPos = position + Offset;
        return data.SampleAt(globalPos);
    }

    public bool SampleAt(Vector3i position)
    {
        return data.SampleAt(position);
    }
}