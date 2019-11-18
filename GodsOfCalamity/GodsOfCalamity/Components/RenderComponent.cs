using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Components
{
    public class RenderComponent : Component        // essentially just a container for the image files. This will contain a reference to an asset, and when the entity needs to render the sprite it will get it's location from here
    {

        public string assetPath { get; private set; }

        public RenderComponent() : base(ComponentType.Render) { }

        public RenderComponent(string path) : base(ComponentType.Render)
        {
            assetPath = path;
        }

        public void SetAssetPath(string newAsset)
        {
            assetPath = newAsset;
        }

        public string GetAssetRef()
        {
            return assetPath;
        }

    }
}
