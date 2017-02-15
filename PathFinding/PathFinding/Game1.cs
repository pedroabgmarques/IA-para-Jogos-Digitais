using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PathFinding
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;
        int[,] map;
        public static int tileWidth = 16;
        Guy guy;

        //Descreve uma ligação entre dois nós
        public class Connection
        {
            public Vector2 from;
            public Vector2 to;
            public int cost;
            public Connection(Vector2 from, Vector2 to)
            {
                this.from = from;
                this.to = to;
                this.cost = 1;
            }
        }

        bool isWalkable(float x, float y)
        {
            return isWalkable((int)x, (int)y);
        }

        List<Connection> getConnections(Vector2 node)
        {
            List<Connection> l = new List<Connection>();
            if(isWalkable(node.X - 1, node.Y)){
                l.Add(new Connection(node, node - Vector2.UnitX));
            }
            if(isWalkable(node.X + 1, node.Y)){
                //path left
                l.Add(new Connection(node, node + Vector2.UnitX));
            }
            if(isWalkable(node.X, node.Y - 1)){
                //path left
                l.Add(new Connection(node, node - Vector2.UnitY));
            }
            if(isWalkable(node.X, node.Y + 1)){
                //path left
                l.Add(new Connection(node, node + Vector2.UnitY));
            }

            return l;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Content.Load<Texture2D>("cobbleset-64");
            guy = new Guy(Content);

            LoadMap();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<Vector2> path = Dijkstra(2 * Vector2.One, 47 * Vector2.One);
            watch.Stop();
            var elapsedMS = watch.ElapsedMilliseconds;
            Console.WriteLine("Djikstra took " + elapsedMS + "ms to calculate a path.");

            guy.Walk(path.ToArray());
            
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            guy.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawBoard();
            guy.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBoard()
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    Point size = new Point(tileWidth, tileWidth);
                    int l = (map[x, y] - 1) % tileWidth;
                    Point target = new Point(l * tileWidth, map[x, y] - 1 - l);



                    spriteBatch.Draw(spriteSheet, new Vector2(x, y) * tileWidth,
                                     new Rectangle(target, size),
                                     Color.White, 0, Vector2.Zero, Vector2.One,
                                     SpriteEffects.None, 0);
                }
            }
        }
        private void LoadMap()
        {
            map = new int[50, 50];
            System.IO.StreamReader file = new System.IO.StreamReader("Content/maze.tmx");
            string line;
            int y = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (Regex.Matches(line, "\\s*<").Count != 0) continue;
                int x = 0;

                foreach (int cell in line.Split(',').TakeWhile((arg) => arg.Length > 0).Select<string, int>((arg) => int.Parse(arg)))
                {
                    map[x++, y] = cell;
                }
                y++;
            }
            file.Close();
        }

        private bool isWalkable(int x, int y)
        {
            return map[x, y] == 45;
        }

        class NodeRecord
        {
            public Vector2 node;
            public Connection connection;
            public int costSoFar;
        }

        class AStarNodeRecord
        {
            public Vector2 node;
            public Connection connection;
            public float costSoFar;
            public float estimatedTotalCost;
        }

        List<Vector2> Dijkstra(Vector2 start, Vector2 end)
        {
            List<NodeRecord> open = new List<NodeRecord>();
            List<NodeRecord> closed = new List<NodeRecord>();
            NodeRecord startRecord = new NodeRecord();
            startRecord.node = start;
            startRecord.connection = null;
            startRecord.costSoFar = 0;

            open.Add(startRecord);

            NodeRecord current = null;
            while (open.Count > 0)
            {
                //procurar no com menor custo
                current = open.OrderBy(arg => arg.costSoFar).First();
                //chegamos ao fim?
                if (current.node == end) 
                    break;
                //obter conexões
                List<Connection> connections = getConnections(current.node);
                //foreach connection
                foreach (var connection in connections)
                {
                    Vector2 endNode = connection.to;
                    //ja processamos este nodo?
                    if (closed.Exists(obj => obj.node == endNode)) 
                        continue;
                    //calcular custo acumulado desta conexao
                    int endNodeCost = current.costSoFar + connection.cost;

                    NodeRecord endNodeRecord = null;
                    bool wasFound = false;
                    endNodeRecord = open.Find(a => a.node == endNode);
                    if (endNodeRecord != null)
                    {
                        //esta na lista
                        wasFound = true;
                        if (endNodeRecord.costSoFar <= endNodeCost)
                            continue;
                    }
                    else
                    {
                        endNodeRecord = new NodeRecord();
                        endNodeRecord.node = endNode;
                    }
                    endNodeRecord.costSoFar = endNodeCost;
                    endNodeRecord.connection = connection;
                    if (!wasFound) open.Add(endNodeRecord);
                }
                open.Remove(current);
                closed.Add(current);
            }

            if (current.node != end) return null;

            List<Connection> path = new List<Connection>();
            while (current.node != start)
            {
                path.Add(current.connection);
                current = closed.Find(x => x.node == current.connection.from);

            }
            path.Reverse();

            List<Vector2> points = path.Select<Connection, Vector2>(c => c.to).ToList();
            points.Add(end);
            return points;
        }

        List<Vector2> AStar(Vector2 start, Vector2 end)
        {

            //Criar nova heuristica
            Heuristic heuristic = new Heuristic(end);

            List<AStarNodeRecord> open = new List<AStarNodeRecord>();
            List<AStarNodeRecord> closed = new List<AStarNodeRecord>();

            AStarNodeRecord startRecord = new AStarNodeRecord();
            startRecord.node = start;
            startRecord.connection = null;
            startRecord.costSoFar = 0f;
            startRecord.estimatedTotalCost = heuristic.GetEstimatedCost(start);

            open.Add(startRecord);

            AStarNodeRecord current = null;
            while (open.Count > 0)
            {
                //procurar no com menor custo
                current = open.OrderBy(arg => arg.estimatedTotalCost).First();
                //chegamos ao fim?
                if (current.node == end)
                    break;
                //obter conexões
                List<Connection> connections = getConnections(current.node);
                //foreach connection
                foreach (var connection in connections)
                {
                    Vector2 endNode = connection.to;
                    //calcular custo acumulado desta ligação
                    float endNodeCost = current.costSoFar + connection.cost;

                    //ja processamos este nodo?
                    AStarNodeRecord endNodeRecord = null;
                    endNodeRecord = closed.Find(x => x.node == endNode);
                    if (endNodeRecord != null)
                    {
                        if (endNodeRecord.costSoFar < endNodeCost) continue;
                        closed.Remove(endNodeRecord);
                    }
                    

                    bool wasFound = false;
                    endNodeRecord = open.Find(a => a.node == endNode);
                    if (endNodeRecord != null)
                    {
                        //esta na lista
                        wasFound = true;
                        if (endNodeRecord.costSoFar <= endNodeCost)
                            continue;
                    }
                    else
                    {
                        endNodeRecord = new NodeRecord();
                        endNodeRecord.node = endNode;
                    }
                    endNodeRecord.costSoFar = endNodeCost;
                    endNodeRecord.connection = connection;
                    if (!wasFound) open.Add(endNodeRecord);
                }
                open.Remove(current);
                closed.Add(current);
            }

            if (current.node != end) return null;

            List<Connection> path = new List<Connection>();
            while (current.node != start)
            {
                path.Add(current.connection);
                current = closed.Find(x => x.node == current.connection.from);

            }
            path.Reverse();

            List<Vector2> points = path.Select<Connection, Vector2>(c => c.to).ToList();
            points.Add(end);
            return points;
        }

    }
}