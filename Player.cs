using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MrPhilGames
{
    public class Player
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Rectangle PositionAsRect
        {
            get
            {
                return new Rectangle(Position.ToPoint(), Program.TileSize);
            }
        }

        public bool IsMan
        {
            get
            {
                return Texture == Program.game.man;
            }
        }
    }
}
