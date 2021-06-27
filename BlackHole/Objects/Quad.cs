using OpenTK;

namespace BlackHole
{
    public class Quad : PositionTransform
    {
        private const float NEAR = 1.0f;
        private Vector3[] _vertices =
        {
            new Vector3(-1.0f, -1.0f, NEAR),
            new Vector3(+1.0f, -1.0f, NEAR),
            new Vector3(+1.0f, +1.0f, NEAR),
            new Vector3(-1.0f, +1.0f, NEAR)
        };
        protected override Vector3[] vertices => _vertices;

        private uint[] _indices = {
             0,  2,  1,      0,  3,  2
        };
        protected override uint[] indices => _indices;

        public Quad()
        {
            Color = new Vector4(1.0f);
            Initialize();
        }
    }
}
