using UnityEngine;

public struct Vector3i
{
    public int x;
    public int y;
    public int z;

    public Vector3i(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString()
    {
        return string.Format("x:{0}, y:{1}, z:{2}", x, y, z);
    }

    public override int GetHashCode()
    {
        return (x << 16) ^ (y << 8) ^ z;
    }

    public static implicit operator Vector3(Vector3i v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    public static Vector3i operator +(Vector3i lhs, Vector3i rhs)
    {
        return new Vector3i(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
    }

    public static Vector3i operator -(Vector3i lhs, Vector3i rhs)
    {
        return new Vector3i(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
    }

    public static Vector3i operator *(Vector3i lhs, int rhs)
    {
        return new Vector3i(lhs.x*rhs, lhs.y*rhs, lhs.z*rhs);
    }

    public static Vector3i operator *(int rhs, Vector3i lhs)
    {
        return new Vector3i(lhs.x*rhs, lhs.y*rhs, lhs.z*rhs);
    }

    public static Vector3i operator /(Vector3i lhs, int rhs)
    {
        return new Vector3i(lhs.x/rhs, lhs.y/rhs, lhs.z/rhs);
    }
}