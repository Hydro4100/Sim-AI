using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static MainQuest6_TreasureHunter.Tile;

namespace MainQuest6_TreasureHunter
{
    public class TileMap
    {
        private Tile[,] Tiles;
        private int Columns => Tiles.GetLength(0);
        private int Rows => Tiles.GetLength(1);
        private int Size => 12;

        private Texture2D _whitePixelTexture;

        public TileMap(int columns, int rows, Texture2D texture)
        {
            Tiles = new Tile[columns, rows];
            _whitePixelTexture = texture;

            int tileSize = 10;
            int tileBorder = 1;

            int currentX = 0, currentY = 0;
            for (int i = 0; i < Columns; i++, currentX += tileSize + tileBorder * 2)
            {
                for (int j = 0; j < Rows; j++, currentY += tileSize + tileBorder * 2)
                {
                    Tiles[i, j] = new Tile();
                    Tiles[i, j].OutLineColour = Color.Black;
                    //Tiles[i, j].InFillColour = Color.White;
                    Tiles[i, j].OutLineRectangle = new Rectangle(currentX, currentY, tileSize + tileBorder * 2, tileSize + tileBorder * 2);
                    Tiles[i, j].InFillRectangle = new Rectangle(currentX + tileBorder, currentY + tileBorder, tileSize, tileSize);
                    Tiles[i, j].Type = TileType.EMPTY;
                }
                currentY = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    spriteBatch.Draw(_whitePixelTexture, Tiles[i, j].OutLineRectangle, Tiles[i, j].OutLineColour);
                }
            }

            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    spriteBatch.Draw(_whitePixelTexture, Tiles[i, j].InFillRectangle, Tiles[i, j].InFillColour);
                }
            }
        }

        public void ChangeType(Point point, Tile.TileType type)
        {
            int column = point.X / Size;
            int row = point.Y / Size;

            if (InvalidTile(column, row))
                return;

            Tile tile = Tiles[column, row];

            tile.Type = type;
        }

        private bool InvalidTile(int col, int row)
        {
            return col < 0 || col >= Columns || row < 0 || row >= Rows;
        }

        private bool ValidTile(int col, int row)
        {
            return !InvalidTile(col, row);
        }
    }
}
