using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Entity
{
    class PauseButton : BaseEntity
    {
        //internal static readonly object gameObject;

        public PauseButton() : base(EntityType.PauseButton)
        {
            id = Guid.NewGuid();
        }

        public static bool enabled { get; internal set; }
        public static object gamepaused { get; internal set; }

        internal static void gameObject(bool v)
        {
            throw new NotImplementedException();
        }
    }
    class MenuResume : BaseEntity
    {
        internal static bool enabled;

        public MenuResume() : base(EntityType.MenuResume)
        {
            id = Guid.NewGuid();
        }

       // internal static void gameObject(bool v)
        //{
         //   throw new NotImplementedException();
        //}
    }
    class MenuQuit : BaseEntity
    {
        internal static bool enabled;

        public MenuQuit() : base(EntityType.MenuResume)
        {
            id = Guid.NewGuid();
        }
    }

}
