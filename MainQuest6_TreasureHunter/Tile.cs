using Microsoft.Xna.Framework;

namespace MainQuest6_TreasureHunter
{
    public class Tile
    {
        public Rectangle OutLineRectangle;
        public Color OutLineColour;
        public Rectangle InFillRectangle;
        public Color InFillColour
        {
            get
            {
                switch (Type)
                {
                    case TileType.WALL:
                        return Color.HotPink;
                    default:
                        return Color.White;
                }
            }
        }

        public enum TileType { WALL, FOOD, EMPTY };

        public TileType Type { get; set; }
    }
}
