/* Author:  Cong Trinh (Team Crios)
 * Class:   Cpts421 Senior Design
 * Project: Microsoft Eye-Tracking Game; "Gods of Calamity"
 *
 * Desc:    DisplayHealth will display an object's health when gazed over.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Systems
{
    public class DisplayHealthSystem : ISystem
    {   /*Reads entity's current/max health + displays it during Gaze
         *[Personal Notes] Goals: 1.) Display Entity + Health bar (Bool) / 2.) Implement Gaze Event w/ Eye-Tracker
         */

        //[INSERT EVENT HANDLER HERE]
        public EventHandler<DisplayHealth> OnHoverOverEntity;

        public DisplayHealthSystem() { }

        public void CheckPlayerGaze()
        {   /* If player's gaze is near Entity, display given Entity's HP */

        }
    }

    public class DisplayHealth : EventArgs
    {
        public DisplayHealth()
        {

        }
    }
}
