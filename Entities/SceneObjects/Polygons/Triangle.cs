﻿using ESRT.Entities.Materials;

namespace ESRT.Entities.SceneObjects.Polygons
{
    public class Triangle : IIntersectable
    {
        public Material Material { get; private set; }

        public VertexData A { get; protected set; }
        public (float u, float v) A_TextureCoords { get; protected set; }

        public VertexData B { get; private set; }
        public (float u, float v) B_TextureCoords { get; protected set; }

        public VertexData C { get; private set; }
        public (float u, float v) C_TextureCoords { get; protected set; }

        public bool CastShadows { get; private set; }

        public Triangle(Material material, bool castShadows,
            VertexData a, VertexData b, VertexData c,
            (float u, float v) a_TextureCoords, (float u, float v) b_TextureCoords, (float u, float v) c_TextureCoords)
        {
            Material = material;
            CastShadows = castShadows;
            A = a;
            B = b;
            C = c;
            A_TextureCoords = a_TextureCoords;
            B_TextureCoords = b_TextureCoords;
            C_TextureCoords = c_TextureCoords;
        }

        public bool Intersect(Ray ray, out HitData hitData)
        {
            Vector3 E = ray.Start - A.Position;
            Vector3 C1 = -1 * ray.Direction;
            Vector3 C2 = B.Position - A.Position;
            Vector3 C3 = C.Position - A.Position;
            Matrix3x3 matrix = new Matrix3x3(C1.x, C2.x, C3.x,
                                                C1.y, C2.y, C3.y,
                                                C1.z, C2.z, C3.z);
            (float t, float lambda2, float lambda3) result = MathUtility.SolveLinearEquationSystem(matrix, E);

            bool wasHit = result.lambda2 >= 0 && result.lambda3 >= 0 && result.lambda2 + result.lambda3 <= 1;

            if (!wasHit || result.t < 0)
            {
                hitData = HitData.NoHit;
                return false;
            }

            float lambda1 = 1 - result.lambda2 - result.lambda3;

            Vector3 hitPosition = ray.Start + result.t * ray.Direction;
            Vector3 normal;
            if (Material.ShadingMode == Shading.Smooth)
            {
                normal = lambda1 * A.Normal + result.lambda2 * B.Normal + result.lambda3 * C.Normal;
            }
            else
            {
                normal = FlatNormal;
            }
            (float u, float v) interpolatedTextureCoords;
            interpolatedTextureCoords.u = lambda1 * A_TextureCoords.u + result.lambda2 * B_TextureCoords.u + result.lambda3 * C_TextureCoords.u;
            interpolatedTextureCoords.v = lambda1 * A_TextureCoords.v + result.lambda2 * B_TextureCoords.v + result.lambda3 * C_TextureCoords.v;

            hitData = new HitData(hitPosition, normal, interpolatedTextureCoords, Material);
            return true;
        }

        public Vector3 FlatNormal
        {
            get => (B.Position - A.Position).CrossProduct(C.Position - A.Position).Normalize();
        }

        public float SurfaceArea
        {
            get
            {
                Vector3 AB = (B.Position - A.Position);
                Vector3 AC = (C.Position - A.Position);
                float angle = AB.Angle(AC);
                return 0.5f * AB.Length * AC.Length * angle;
            }
        }
    }
}