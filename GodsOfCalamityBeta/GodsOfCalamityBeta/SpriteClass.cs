// This class is necessary to create a valid sprite.
// It will compose SpriteCOmponent

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamityBeta
{
    public class SpriteClass
    {
        private float scale { get; }
        public Microsoft.Xna.Framework.Graphics.Texture2D texture { get; }
        public SpriteClass(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice, string textureName, float scale)
        {
            this.scale = scale;
            if (texture == null)
            {
                using (var stream = Microsoft.Xna.Framework.TitleContainer.OpenStream(textureName))
                {
                    texture = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(graphicsDevice, stream);
                }
            }
        }
    }
}
