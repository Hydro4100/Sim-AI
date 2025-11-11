using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
                    Tiles[i, j].OutLineRectangle = new Rectangle(currentX, currentY, tileSize + tileBorder * 2, tileSize + tileBorder * 2);
                    Tiles[i, j].InFillRectangle = new Rectangle(currentX + tileBorder, currentY + tileBorder, tileSize, tileSize);
                    
                    Tiles[i, j].Type = TileType.EMPTY;
                    Tiles[i, j].DistanceToFood = int.MaxValue;
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

            if (type == TileType.FOOD)
            {
                AddFood(column, row);
            }
            else if (type == TileType.WALL)
            {
                UpdateDistances();
            }
        }

        private bool InvalidTile(int col, int row)
        {
            return col < 0 || col >= Columns || row < 0 || row >= Rows;
        }

        private bool ValidTile(int col, int row)
        {
            return !InvalidTile(col, row);
        }

        public void AddFood(int column, int row)
        {
            if (InvalidTile(column, row))
                return;

            var queue = new Queue<(int x, int y)>();
            queue.Enqueue((column, row));
            Tiles[column, row].DistanceToFood = 0;

            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                float currentDistance = Tiles[x, y].DistanceToFood;

                for (int dir = 0; dir < 4; dir++)
                {
                    int nx = x + dx[dir];
                    int ny = y + dy[dir];

                    if (ValidTile(nx, ny))
                    {
                        Tile neighbour = Tiles[nx, ny];
                        if (neighbour.Type != Tile.TileType.WALL)
                        {
                            if (neighbour.DistanceToFood > currentDistance + 1)
                            {
                                neighbour.DistanceToFood = currentDistance + 1;
                                queue.Enqueue((nx, ny));
                            }
                        }
                    }
                }
            }
        }

        public void UpdateDistances()
        {
            List<(int, int)> foodLocations = new List<(int, int)>();

            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                    if (Tiles[i, j].Type == Tile.TileType.FOOD)
                        foodLocations.Add((i, j));

            // Reset all distances
            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                    Tiles[i, j].DistanceToFood = int.MaxValue;

            ClearPath();

            foreach ((int, int) foodLocation in foodLocations)
            {
                AddFood(foodLocation.Item1, foodLocation.Item2);
            }
        }

        public void ClearPath()
        {
            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                    Tiles[i, j].OutLineColour = Color.Black;
        }

        public void FindNewPath(Point point)
        {
            ClearPath();

            int column = point.X / Size;
            int row = point.Y / Size;

            if (InvalidTile(column, row))
                return;

            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };

            int x = column;
            int y = row;

            while (Tiles[x, y].Type != Tile.TileType.FOOD)
            {
                Tiles[x, y].OutLineColour = Color.White;

                float minDistance = float.MaxValue;
                int nextX = x, nextY = y;

                for (int dir = 0; dir < 4; dir++)
                {
                    int nx = x + dx[dir];
                    int ny = y + dy[dir];

                    if (ValidTile(nx, ny))
                    {
                        Tile neighbor = Tiles[nx, ny];
                        if (neighbor.DistanceToFood < minDistance)
                        {
                            minDistance = neighbor.DistanceToFood;
                            nextX = nx;
                            nextY = ny;
                        }
                    }
                }

                // If we can't move, break to avoid infinite loop
                if (nextX == x && nextY == y)
                    break;

                x = nextX;
                y = nextY;
            }

            // Mark the food tile as well
            Tiles[x, y].OutLineColour = Color.White;
        }
    }
}
