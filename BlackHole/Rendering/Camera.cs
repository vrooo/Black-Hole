using OpenTK;
using System;

namespace BlackHole
{
    public abstract class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public float ZNear { get; set; } = 1;
        public float ZFar { get; set; } = 200;
        public float FOV { get; set; } = (float)(Math.PI / 4.0);

        public float MoveSpeed { get; set; }
        public float RotateSpeed { get; set; }
        public float ZoomSpeed { get; set; }

        public Camera(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public Camera(Vector3 position) : this(position, Vector3.Zero) { }

        public Camera(float x, float y, float z) : this(new Vector3(x, y, z)) { }

        public Camera(float x, float y, float z, float rx, float ry, float rz)
            : this(new Vector3(x, y, z), new Vector3(rx, ry, rz)) { }

        public Matrix4 GetRotationMatrix()
        {
            return Matrix4.CreateRotationZ(Rotation.Z) *
                   Matrix4.CreateRotationY(Rotation.Y) *
                   Matrix4.CreateRotationX(Rotation.X);
        }

        public Matrix4 GetInvRotationMatrix()
        {
            return Matrix4.CreateRotationX(-Rotation.X) *
                   Matrix4.CreateRotationY(-Rotation.Y) *
                   Matrix4.CreateRotationZ(-Rotation.Z);
        }

        public abstract Matrix4 GetViewMatrix();
        public abstract Matrix4 GetInvViewMatrix();

        public Matrix4 GetProjectionMatrix(float width, float height)
        {
            return Matrix4.CreatePerspectiveFieldOfView(FOV, width / height, ZNear, ZFar);
        }

        public abstract void Rotate(float x, float y, float z);

        public abstract void Translate(float x, float y, float z);
    }
}
