using Microsoft.Xna.Framework;
using System;

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
                    case TileType.FOOD:
                        return Color.CornflowerBlue;
                    default:
                        float distance = DistanceToFood / 30;
                        float t = Math.Clamp(distance, 0f, 1f);
                        if (t < 0.5f)
                        {
                            // Green to Yellow: interpolate red from 0 to 255
                            float r = t / 0.5f; // 0 to 1
                            return new Color((int)(r * 255), 255, 0);
                        }
                        else
                        {
                            // Yellow to Red: interpolate green from 255 to 0
                            float g = (1f - t) / 0.5f; // 1 to 0
                            return new Color(255, (int)(g * 255), 0);
                        }
                }
            }
        }

        public enum TileType { WALL, FOOD, EMPTY };

        public TileType Type { get; set; }

        public float DistanceToFood { get; set; }
    }
}
