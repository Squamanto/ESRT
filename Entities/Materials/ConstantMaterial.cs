﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESRT.Entities.Materials
{
    public class ConstantMaterial : Material
    {
        private Color color;
        private float ambient;
        private float diffuse;
        private float specular;
        private float reflective;
        private float transmissive;

        public override float Ambient(float u, float v)
        {
            return ambient;
        }

        public override float Diffuse(float u, float v)
        {
            return diffuse;
        }

        public override float Specular(float u, float v)
        {
            return specular;
        }

        public override float Reflective(float u, float v)
        {
            return reflective;
        }

        public override float Transmissive(float u, float v)
        {
            return transmissive;
        }

        public override Color Color(float u, float v)
        {
            return color;
        }

        public ConstantMaterial(Color color, float ambient, float diffuse, float specular, float reflective, float transmissive)
        {
            this.color = color;
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.reflective = reflective;
            this.transmissive = transmissive;
        }
    }
}