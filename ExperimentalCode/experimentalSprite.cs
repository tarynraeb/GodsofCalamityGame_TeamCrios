public SpriteClass (GraphicsDevice graphicsDevice, string textureName, float scale, int entityID)	// consider renaming this class to SetSpriteSystem
{
	Entity capturedEntity = EntityManager.Get(entityID);
	capturedEntity.UpdateScaleSystem(scale);	// update an entity's scale component
  
  if (capturedEntity.GetTextureSystem() == null)
  {
    using (var stream = TitleContainer.OpenStream(textureName))
    {
      capturedEntity.SetTextureSystem(Texture2D.FromStream(graphicsDevice, stream));
    }
  }
}

// The following are members of the Sprite Class
// Above is the constructor
// I want to convert the constructor to a system, and the memeber fields to components of an entity

public Texture2D texture
{
  get;
}

// The x and y variables represent the sprite’s current position on the plane
// It’s important to note that, for this class, x and y represent the coordinates of the center of the sprite, (the default origin is the top-left corner).
// This is makes rotating sprites easier, as they will rotate around whatever origin they are given, and rotating around the center gives us a uniform spinning motion.
public float x
{
  get;
  set;
}

public float y
{
  get;
  set;
}

// The angle variable is the sprite’s current angle in degrees (0 being upright, 90 being tilted 90 degrees clockwise).
public float angle
{
  get;
  set;
}

// After this, we have dX, dY, and dA, which are the per-second rates of change for the x, y, and angle variables respectively
public float dX
{
  get;
  set;
}

public float dY
{
  get;
  set;
}

public float dA
{
  get;
  set;
}

public float scale
{
  get;
  set;
}