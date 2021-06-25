using OpenTK;

namespace BlackHole
{
    public class Cube : PositionTransform
    {
        private Vector3[] _vertices =
        {
            // front
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(+1.0f, -1.0f, -1.0f),
            new Vector3(+1.0f, +1.0f, -1.0f),
            new Vector3(-1.0f, +1.0f, -1.0f), 
            // top
            new Vector3(-1.0f, +1.0f, -1.0f),
            new Vector3(+1.0f, +1.0f, -1.0f),
            new Vector3(+1.0f, +1.0f, +1.0f),
            new Vector3(-1.0f, +1.0f, +1.0f), 
            // right
            new Vector3(+1.0f, -1.0f, -1.0f),
            new Vector3(+1.0f, -1.0f, +1.0f),
            new Vector3(+1.0f, +1.0f, +1.0f),
            new Vector3(+1.0f, +1.0f, -1.0f), 
            // back
            new Vector3(+1.0f, -1.0f, +1.0f),
            new Vector3(-1.0f, -1.0f, +1.0f),
            new Vector3(-1.0f, +1.0f, +1.0f),
            new Vector3(+1.0f, +1.0f, +1.0f), 
            // left
            new Vector3(-1.0f, -1.0f, +1.0f),
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, +1.0f, -1.0f),
            new Vector3(-1.0f, +1.0f, +1.0f), 
            // bottom
            new Vector3(-1.0f, -1.0f, +1.0f),
            new Vector3(+1.0f, -1.0f, +1.0f),
            new Vector3(+1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, -1.0f, -1.0f)
        };
        protected override Vector3[] vertices => _vertices;

        private uint[] _indices = {
             0,  2,  1,      0,  3,  2,
             4,  6,  5,      4,  7,  6,
             8, 10,  9,      8, 11, 10,
            12, 14, 13,     12, 15, 14,
            16, 18, 17,     16, 19, 18,
            20, 22, 21,     20, 23, 22
        };
        protected override uint[] indices => _indices;

        public Cube()
        {
            Color = new Vector4(0.8f, 0.3f, 0.6f, 0.3f);
            Initialize();
        }
    }
}
