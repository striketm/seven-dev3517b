using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace dev173d
{
    public class Light
    {
        //tipo da luz
        public enum TiposEfeitoBasico
        {
            Nenhum,
            LuzesAmbiente,
            IluminacaoPadrao,
            UmaLuz,

        }
        TiposEfeitoBasico tipoAtual = TiposEfeitoBasico.UmaLuz;

        public BasicEffect efeitoLuz;

        public Light(GraphicsDevice graphicsDevice)
        {
            efeitoLuz = new BasicEffect(graphicsDevice);

            switch (tipoAtual)
            {
                case TiposEfeitoBasico.IluminacaoPadrao:
                    efeitoLuz.EnableDefaultLighting();
                    break;
                case TiposEfeitoBasico.UmaLuz:
                    efeitoLuz.LightingEnabled = true;
                    efeitoLuz.DirectionalLight0.Enabled = true;
                    efeitoLuz.DirectionalLight0.DiffuseColor = Color.Blue.ToVector3();
                    efeitoLuz.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(-1, -1f, 0));
                    break;
            }
        }

    }
}
