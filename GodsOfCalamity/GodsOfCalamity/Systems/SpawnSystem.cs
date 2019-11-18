/* Author:  Cong Trinh (Team Crios)
 * Class:   Cpts421 Senior Design
 * Project: Microsoft Eye-Tracking Game; "Gods of Calamity"
 *
 * Desc:    SpawnSystem generates a Position Coordinate, which will store
 *              and return newly-spawned objects'/disasters' spawning
 *              coordinates used to render new objects onto the map.
 *              
 *          Extra code that is commented out can be used for future prototypes.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodsOfCalamity.Systems
{
    public class SpawnSystem : ISystem
    {   /* Determines where entity (village/disasters) will be spawned */
        Components.PositionComponent village_position;
        readonly int RES_WIDTH = 1920;
        readonly int RES_HEIGHT = 1080;
        readonly int DISASTER_SIZE = 128;
        private int x_coord;
        private int y_coord;

        public SpawnSystem()
        {   /* Sets game res. to 1920*1080px by default */
            this.x_coord = 0;
            this.y_coord = 0;
            village_position = new Components.PositionComponent(0, 0);
        }

        public SpawnSystem(int res_width, int res_height)
        {   /* Sets game res. to custom size (Depending on current Map-Size)*/
            this.RES_WIDTH = res_width;
            this.RES_HEIGHT = res_height;
            this.x_coord = 0;
            this.y_coord = 0;
            village_position = new Components.PositionComponent(0, 0);
        }

        public Components.PositionComponent Spawn(EntityType entity_type)
        {   /* Determines Spawn Locations for each specific entity */

            this.x_coord = 0;
            this.y_coord = 0;

            switch (entity_type)
            {   /*Determines entity type*/
                case EntityType.Village:    //Spawn @middle of map
                    ComputeVillageCoord();
                    break;
                case EntityType.Meteor:     //Spawn @map-edge
                    ComputeMeteorCoord();
                    break;
                case EntityType.Lightning:  //Spawn @Anywhere (Excluding Village)
                    ComputeLighteningCoord();
                    break;
            }

            Components.PositionComponent new_position = new Components.PositionComponent(this.x_coord, this.y_coord);   //Creates Spawn Position/Location

            return new_position;
        }



        public void ComputeVillageCoord ()
        {   /* Village: Spawns in the middle of the map */
            int SpawnRegionWidth = (RES_WIDTH / 3);                         //Accounts for 1/3 of map's width
            int SpawnRegionHeight = (RES_HEIGHT / 3);                       //Accounts for 1/3 of map's height
            this.x_coord = GenRand(SpawnRegionWidth, SpawnRegionWidth * 2);   //(1/3)MapWidth  < xCoord < (2/3)MapWidth
            this.y_coord = GenRand(SpawnRegionHeight, SpawnRegionHeight * 2); //(1/3)MapHeight < yCoord < (2/3)MapHeight
            this.village_position.ChangePos(this.x_coord, this.y_coord);        //Sets village coord
        }

        public void ComputeMeteorCoord()
        {   /* Meteor: Spawns at the corners of the map */
            //int spawn_vert_hori = GenRand(0, 1);    //Determines Spawn @ Vertical or Horizontal side
            //int spawn_min_max = GenRand(0, 1);      //Determines Coord is 0 or MaxLength

            /*
            int[] set_meteor_x = new int[2] { 0, RES_WIDTH };
            int[] set_meteor_y = new int[2] { 0, RES_HEIGHT };

            int meteor_index_x = GenRand(0, 2);
            int meteor_index_y = GenRand(0, 2);

            this.x_coord = set_meteor_x[meteor_index_x];
            this.y_coord = set_meteor_y[meteor_index_y];
            */

            Random randNum = new Random();

            switch (randNum.Next(0, 5))
            {
                case 1:
                    this.x_coord = 0;
                    this.y_coord = 0;
                    break;
                case 2:
                    this.x_coord = 1920;
                    this.y_coord = 0;
                    break;
                case 3:
                    this.x_coord = 0;
                    this.y_coord = 1080;
                    break;
                case 4:
                    this.x_coord = 1920;
                    this.y_coord = 1080;
                    break;


            }
            
            /*FUNCTION: Spawns meteors on all edges of map*/
            /*switch (spawn_vert_hori)
            {
                case 0:                                     //Spawns @Vertical (Left/Right Side)
                    this.x_coord = GenRand(0, this.RES_WIDTH);        //@Any x.value
                    this.y_coord = this.RES_WIDTH * spawn_min_max;    //@y.0 or @y.MaxHeight
                    break;
                case 1:                                     //Spawns @Horizontal (Top/Bottom Side)
                    this.x_coord = this.RES_WIDTH * spawn_min_max;    //@x.0 or @x.MaxWidth
                    this.y_coord = GenRand(0, this.RES_HEIGHT);       //@Any y.value
                    break;
            }*/
        }

        public void ComputeLighteningCoord()
        {   /* Lightening: Randomly spawns around village */

            int[] set_lightening_x = new int[4] { 320 - 64 , 959 - 64, 1598 - 64, 959 - 64 };
            int[] set_lightening_y = new int[4] { 540 - 64, 270 - 64, 540 - 64, 810 - 64 };

            int lightening_index = GenRand(0, 3);

            this.x_coord = set_lightening_x[lightening_index];
            this.y_coord = set_lightening_y[lightening_index];
        }

        public int GenRand(int min, int max)
        {   /* Generates random value */
            Random rnd = new Random();
            int rand_val = rnd.Next(min, max + 1);
            return rand_val;
        }

        public int GenRand(int min1, int max1, int min2, int max2)
        {   /* Generates2 random values */
            Random rnd = new Random();
            int region_return = rnd.Next(0, 2);
            int rand_val_1 = rnd.Next(min1, max1 + 1);
            int rand_val_2 = rnd.Next(min2, max2 + 1);
            if (region_return == 0)
                return rand_val_1;
            return rand_val_2;
        }

        /*public Component SpawnDisasterFire(EntityType disaster_type, Components.PositionComponent disastor_coord)
        {   // Determines Spawn Locations for each specific entity

            this.x_coord = 0;
            this.y_coord = 0;

            switch (disaster_type)
            {   //Determines disaster type
                case EntityType.Meteor:
                    ComputeFireLighteningCoord(disastor_coord); //Spawns fire below lightening
                    break;
                case EntityType.Lightning:
                    ComputeFireLighteningCoord(disastor_coord); //Spawns fire on village (Assuming meteor has impacted)
                    break;
            }

            Components.PositionComponent new_position = new Components.PositionComponent(this.x_coord, this.y_coord);   //Creates Spawn Position/Location

            return new_position;
        }
        
        public void ComputeFireMeteorCoord(Components.PositionComponent meteor_coord)
        {
            //int meteor_x = meteor_coord.getXPos();
            //int meteor_y = meteor_coord.getYPos();
            int village_x = village_position.getXPos();
            int village_y = village_position.getYPos();

            this.x_coord = village_x;
            this.y_coord = village_y;
        }

        public void ComputeFireLighteningCoord(Components.PositionComponent lightening_coord)
        {
            int lightening_x = lightening_coord.getXPos();
            int lightening_y = lightening_coord.getYPos();

            int range = 200; //Determines how far below lightening will spawn a fire

            this.x_coord = lightening_x + range;    //Shifts coord below lightening for fire spawn
            this.y_coord = lightening_y;
        }*/
    }
}
