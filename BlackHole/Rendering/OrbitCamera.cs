using OpenTK;
using System;

namespace BlackHole
{
    public class OrbitCamera : Camera
    {
        public OrbitCamera(Vector3 position, Vector3 rotation) : base(position, rotation)
        {
            MoveSpeed = 0.01f;
            RotateSpeed = 0.002f;
            ZoomSpeed = 0.005f;
        }

        public OrbitCamera(Vector3 position) : this(position, Vector3.Zero) { }

        public OrbitCamera(float x, float y, float z) : this(new Vector3(x, y, z)) { }

        public OrbitCamera(float x, float y, float z, float rx, float ry, float rz)
            : this(new Vector3(x, y, z), new Vector3(rx, ry, rz)) { }

        public override Matrix4 GetViewMatrix()
        {
            return GetRotationMatrix() * Matrix4.CreateTranslation(Position);
        }

        public override Matrix4 GetInvViewMatrix()
        {
            return Matrix4.CreateTranslation(-Position) * GetInvRotationMatrix();
        }

        public override void Rotate(float x, float y, float z)
        {
            Rotation += new Vector3(x, y, z);
        }

        public override void Translate(float x, float y, float z)
        {
            Position += new Vector3(x, y, z);
        }
    }
}
