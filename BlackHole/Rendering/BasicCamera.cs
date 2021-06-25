using OpenTK;
using System;

namespace BlackHole
{
    public class BasicCamera : Camera
    {
        public BasicCamera(Vector3 position, Vector3 rotation) : base(position, rotation)
        {
            MoveSpeed = 0.02f;
            RotateSpeed = 0.002f;
            ZoomSpeed = 0.005f;
        }

        public BasicCamera(Vector3 position) : this(position, Vector3.Zero) { }

        public BasicCamera(float x, float y, float z) : this(new Vector3(x, y, z)) { }

        public BasicCamera(float x, float y, float z, float rx, float ry, float rz)
            : this(new Vector3(x, y, z), new Vector3(rx, ry, rz)) { }

        public override Matrix4 GetViewMatrix()
        {
            return Matrix4.CreateTranslation(Position) * GetRotationMatrix();
        }

        public override Matrix4 GetInvViewMatrix()
        {
            return GetInvRotationMatrix() * Matrix4.CreateTranslation(-Position);
        }

        public override void Rotate(float x, float y, float z)
        {
            Rotation += new Vector3(x, y, z);
        }

        public override void Translate(float x, float y, float z)
        {
            Vector4 change = GetRotationMatrix() * new Vector4(x, y, z, 1);
            Position += new Vector3(change.X, change.Y, change.Z);
        }
    }
}
