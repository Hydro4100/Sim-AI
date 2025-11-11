using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MainQuest6_TreasureHunter
{
    public class Game6 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _whitePixelTexture;
        private TileMap _tileMap;
        private NavMesh _navMesh;

        public Game6()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _navMesh = new NavMesh();

            NavMeshNode[] nodes = new NavMeshNode[33];

            nodes[0] = new NavMeshNode(new Rectangle(10, 10, 80, 200), Color.Green);
            nodes[1] = new NavMeshNode(new Rectangle(10, 210, 80, 50), Color.Blue);
            nodes[2] = new NavMeshNode(new Rectangle(10, 260, 80, 30), Color.Green);
            nodes[3] = new NavMeshNode(new Rectangle(90, 210, 10, 50), Color.Green);
            nodes[4] = new NavMeshNode(new Rectangle(100, 210, 50, 50), Color.Blue);
            nodes[5] = new NavMeshNode(new Rectangle(100, 260, 50, 30), Color.Green);
            nodes[6] = new NavMeshNode(new Rectangle(100, 130, 50, 80), Color.Green);
            nodes[7] = new NavMeshNode(new Rectangle(100, 80, 50, 50), Color.Blue);
            nodes[8] = new NavMeshNode(new Rectangle(150, 80, 30, 50), Color.Green);
            nodes[9] = new NavMeshNode(new Rectangle(180, 80, 50, 50), Color.Blue);
            nodes[10] = new NavMeshNode(new Rectangle(230, 80, 40, 50), Color.Green);
            nodes[11] = new NavMeshNode(new Rectangle(270, 80, 50, 50), Color.Blue);
            nodes[12] = new NavMeshNode(new Rectangle(270, 70, 50, 10), Color.Green);
            nodes[13] = new NavMeshNode(new Rectangle(270, 10, 50, 60), Color.Blue);
            nodes[14] = new NavMeshNode(new Rectangle(320, 10, 80, 60), Color.Green);
            nodes[15] = new NavMeshNode(new Rectangle(220, 10, 50, 60), Color.Green);
            nodes[16] = new NavMeshNode(new Rectangle(210, 20, 10, 40), Color.Blue);
            nodes[17] = new NavMeshNode(new Rectangle(100, 10, 110, 60), Color.Green);
            nodes[18] = new NavMeshNode(new Rectangle(180, 130, 50, 10), Color.Green);
            nodes[19] = new NavMeshNode(new Rectangle(180, 140, 50, 150), Color.Blue);
            nodes[20] = new NavMeshNode(new Rectangle(160, 140, 20, 150), Color.Green);
            nodes[21] = new NavMeshNode(new Rectangle(230, 140, 110, 150), Color.Green);
            nodes[22] = new NavMeshNode(new Rectangle(320, 80, 30, 50), Color.Green);
            nodes[23] = new NavMeshNode(new Rectangle(350, 80, 50, 50), Color.Blue);
            nodes[24] = new NavMeshNode(new Rectangle(350, 130, 50, 70), Color.Green);
            nodes[25] = new NavMeshNode(new Rectangle(350, 200, 50, 50), Color.Blue);
            nodes[26] = new NavMeshNode(new Rectangle(350, 250, 50, 40), Color.Green);
            nodes[27] = new NavMeshNode(new Rectangle(400, 200, 10, 50), Color.Green);
            nodes[28] = new NavMeshNode(new Rectangle(410, 200, 80, 50), Color.Blue);
            nodes[29] = new NavMeshNode(new Rectangle(410, 250, 80, 40), Color.Green);
            nodes[30] = new NavMeshNode(new Rectangle(410, 150, 80, 50), Color.Green);
            nodes[31] = new NavMeshNode(new Rectangle(430, 140, 40, 10), Color.Blue);
            nodes[32] = new NavMeshNode(new Rectangle(410, 10, 80, 130), Color.Green);

            AddEdges(nodes, new (int, int)[]
            {
                (0, 1), (1, 2), (1, 3), (3, 4) , (4, 5),
                (4, 6), (6, 7), (7, 8), (8, 9), (9, 10),
                (10, 11), (11, 12), (12, 13), (13, 14), (13, 15),
                (15, 16), (16, 17), (9, 18), (18, 19), (19, 20),
                (19, 21), (11, 22), (22, 23), (23, 24), (24, 25),
                (25, 26), (25, 27), (27, 28), (28, 29), (28, 30),
                (30, 31), (31, 32)
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _whitePixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            _whitePixelTexture.SetData(new Color[] { Color.White });

            _tileMap = new TileMap(50, 30, _whitePixelTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    _tileMap.ChangeType(Mouse.GetState().Position, Tile.TileType.WALL);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.F))
                {
                    _tileMap.ChangeType(Mouse.GetState().Position, Tile.TileType.FOOD);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    _tileMap.FindNewPath(Mouse.GetState().Position);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //_tileMap.Draw(_spriteBatch);

            foreach (NavMeshNode node in _navMesh.Graph.GetAllNodes())
            {
                _spriteBatch.Draw(_whitePixelTexture, node.Rectangle, node.Colour);
            }

            foreach (NavMeshNode node in _navMesh.Graph.GetAllNodes())
            {
                foreach ((NavMeshEdge edge, NavMeshNode neighbour) in _navMesh.Graph.GetEdges(node))
                {
                    DrawLine(node.Centre, neighbour.Centre, new(Color.Red, 0.125f), 2f);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void AddEdges(NavMeshNode[] nodes, (int, int)[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                _navMesh.Graph.AddEdge(nodes[edges[i].Item1], new NavMeshEdge(Vector2.Distance(nodes[edges[i].Item1].Centre, nodes[edges[i].Item2].Centre)), nodes[edges[i].Item2]);
                _navMesh.Graph.AddEdge(nodes[edges[i].Item2], new NavMeshEdge(Vector2.Distance(nodes[edges[i].Item1].Centre, nodes[edges[i].Item2].Centre)), nodes[edges[i].Item1]);
            }
        }

        public void DrawLine(Vector2 start, Vector2 end, Color colour, float thickness = 2f)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            float length = edge.Length();

            _spriteBatch.Draw(_whitePixelTexture, start, null, colour, angle, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0f);
        }
    }
}
