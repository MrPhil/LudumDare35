using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MrPhilGames
{
    public class GroundTile
    {
        public Vector2 Position;
        public Texture2D Texture;
        public bool Lit = true;

        public GroundTile(int x, int y)
        {
            Position = new Vector2(x, y);
        }
    }
}
