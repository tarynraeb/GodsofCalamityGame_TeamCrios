// Team Crios
// Gods of Calamity
// Component.cs
// Authored by Michael Berger
// Components in ECS should have no behavior bound to them. They are here just to store data. Their data is given to systems that manipulate it and then give the new data back.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity
{
    // the enum ComponentType will allow simple extendability when creating a new entity sub-class

    public abstract class Component    // All components will inherit from this cbase class
    {
        // Entity EntityType; // components and entities are tightly coupled, so a component should know which EntityType it is associated with
        public ComponentType CompType { get; private set; }

        public Component()
        { }

        public Component(ComponentType type)
        {
            CompType = type;
        }

        public virtual ComponentType GetComponentType()
        {
            return CompType;
        }
    }
}
