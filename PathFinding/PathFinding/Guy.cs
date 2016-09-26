using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PathFinding
{
    public class Guy
    {
        enum Direction
        {
            DOWN = 0, UP, RIGHT, LEFT
        };
        Texture2D sprite;
        Direction direction = Direction.RIGHT;
        int anim = 0;
        Vector2 position;
        IList<Vector2> path;


        public Guy(ContentManager content)
        {
            sprite = content.Load<Texture2D>("NEStalgia_Icons");
            position = new Vector2(2, 2) * Game1.tileWidth;
        }


        public void Update(GameTime gameTime)
        {
            if (path.Count > 0)
            {
                // Kind of hack, but tired to find better solution.
                int tx = (int)(position.X % Game1.tileWidth);
                int ty = (int)(position.Y % Game1.tileWidth);
                int rx = (int)(position.X / Game1.tileWidth);
                int ry = (int)(position.Y / Game1.tileWidth);
                if (tx == 0 && ty == 0 && position / Game1.tileWidth == path[0])
                {
                    path.RemoveAt(0);

                    if (path.Count > 0)
                    {
                        if (path[0].X > rx) direction = Direction.RIGHT;
                        else if (path[0].X < rx) direction = Direction.LEFT;
                        else if (path[0].Y < ry) direction = Direction.UP;
                        else direction = Direction.DOWN;
                    }
                }
                else {
                    anim = ++anim % 2;
                    switch (direction)
                    {
                        case Direction.LEFT: position.X--; break;
                        case Direction.RIGHT: position.X++; break;
                        case Direction.UP: position.Y--; break;
                        case Direction.DOWN: position.Y++; break;
                    }
                }
            }
        }

        public void Walk(IList<Vector2> p)
        {
            if (path == null)
                path = new List<Vector2>();

            foreach (var pos in p)
                path.Add(pos);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, position,
                    new Rectangle(
                        (anim * 4 + (int)direction) * Game1.tileWidth,
                        0, Game1.tileWidth, Game1.tileWidth), Color.White);
        }
    }
}
