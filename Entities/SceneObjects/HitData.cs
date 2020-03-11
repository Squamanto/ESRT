﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRT.Entities.Materials;

namespace ESRT.Entities
{
    public class HitData
    {
        public Vector3 HitPosition { get; private set; }
        public Vector3 Normal { get; private set; }
        public (float u, float v) TextureCoords { get; private set; }
        public Material Material { get; private set; }

        public HitData(Vector3 hitPosition, Vector3 normal, (float u, float v) textureCoords, Material material)
        {
            HitPosition = hitPosition;
            Normal = normal;
            TextureCoords = textureCoords;
            Material = material;
        }
    }
}