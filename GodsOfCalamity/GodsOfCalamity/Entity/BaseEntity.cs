using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GodsOfCalamity.Entity
{

    public abstract class BaseEntity
    {
        protected Guid id;
        public EntityType EntType { get; private set; }
        public List<Component> componentparts = new List<Component>();
        public UserControl sprite;

        public BaseEntity(EntityType type)
        {
            EntType = type;
        }
        public void AddComponent(Component givenComponent)
        {
            componentparts.Add(givenComponent);
        }
        public Guid getID()
        {
            return id;
        }
    }
}

