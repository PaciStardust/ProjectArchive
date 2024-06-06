using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive.Projects.ConsoleDefense
{
    class Game
    {
        //Refs
        private Tower[,] Towers;
        private List<Enemy> Enemies;
        private Map Map;

        //Stats
        private int Health;
        private int MaxHealth;

        //Utility
        private Int2D Edges;

        public Game(Map map)
        {
            Map = map;
            Enemies = new List<Enemy>();
            Towers = new Tower[map.Size.Y, map.Size.X];

            Health = Map.StartHealth;
            MaxHealth = Map.StartHealth;

            Edges = new Int2D(Map.Size.Y * 3, Map.Size.X * 3);
        }

        public void Start()
        {
            //Draw the map onto graphics screen
            Graphics.Add(Graphics.Boxify(Map.DrawMap()), 1, 1);

            while (true)
            {
                PlacementLoop();
                DefendingLoop();
            }
        }

        private void PlacementLoop()
        {
            //Sets cursor
            Int2D cursorlimit = new Int2D(Map.Size.Y - 1, Map.Size.X - 1);
            Int2D cursor = new Int2D(0, 0);

            while (true)
            {
                //Sets right window
                MapTile currenttile = Map.Floor[cursor.Y, cursor.X];
                Tower currenttower = Towers[cursor.Y, cursor.X];
                if (currenttower == null)
                    Graphics.Add(Graphics.Boxify($" --- Tile Info --- \n\n{currenttile.Name} [{currenttile.Appearance[1][1]}]\n\nCan be used?\n > {currenttile.Tag == TileTag.Placable}"), 1, Map.Size.X * 3 + 4);
                else
                    Graphics.Add(Graphics.Boxify($" --- Tile Info --- \n\n{currenttower.Name} [{currenttower.Appearance[1][1]}]\n\n{currenttower.Description}"), 1, Map.Size.X * 3 + 4);
                //Sets bottom window
                Graphics.Add(Graphics.Boxify($"Health: {Health} / {MaxHealth}"), Map.Size.Y * 3 + 4, 1);
                //Draws graphics
                Graphics.DrawCursor(cursor.Y * 3 + 3, cursor.X * 3 + 3);
                //Clears windows
                Graphics.Clear(1, Map.Size.X * 3 + 4);
                Graphics.Clear(Map.Size.Y * 3 + 4, 1);

                //Input management
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        cursor.Y = Math.Max(0, cursor.Y - 1);
                        break;

                    case ConsoleKey.A:
                        cursor.X = Math.Max(0, cursor.X - 1);
                        break;

                    case ConsoleKey.S:
                        cursor.Y = Math.Min(cursorlimit.Y, cursor.Y + 1);
                        break;

                    case ConsoleKey.D:
                        cursor.X = Math.Min(cursorlimit.X, cursor.X + 1);
                        break;

                    case ConsoleKey.Enter:
                        if (currenttile.Tag == TileTag.Placable)
                        {
                            if (Towers[cursor.Y, cursor.X] == null)
                            {
                                Towers[cursor.Y, cursor.X] = ListTowers.Turret_Simple;
                                Graphics.Add(ListTowers.Turret_Simple.Appearance, cursor.Y * 3 + 2, cursor.X * 3 + 2);
                            }
                            else
                            {
                                Towers[cursor.Y, cursor.X] = null;
                                Graphics.Add(Map.Floor[cursor.Y, cursor.X].Appearance, cursor.Y * 3 + 2, cursor.X * 3 + 2);
                            }
                        }
                        break;

                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        private void DefendingLoop()
        {
            //DEBUG
            int spawned = 0;
            bool waveOver = false;
            Random r = new Random();

            while (true)
            {
                //Debug wave sim
                if (spawned < 10)
                {
                    if (r.Next(0, 2) == 0)
                        Enemies.Add(new Enemy_Basic("a", 10, 1, Map.StartLocation, Map.StartFacing)); //Debug Enemy
                    else
                        Enemies.Add(new Enemy_Basic("A", 10, 1, Map.StartLocation, Map.StartFacing));
                    spawned++;
                }
                else
                    waveOver = true;

                //Creates a list of enemies to remove
                List<Enemy> toRemove = new List<Enemy>();

                //Sets bottom window
                Graphics.Add(Graphics.Boxify($"Health: {Health} / {MaxHealth}"), Map.Size.Y * 3 + 4, 1);
                //Draws map
                Graphics.Draw();
                //Clears windows
                Graphics.Clear(Map.Size.Y * 3 + 4, 1);

                //Damage Enemies and remove dead ones
                for (int y = 0; y < Map.Size.Y; y++)
                    for (int x = 0; x < Map.Size.X; x++)
                        if (Towers[y, x] != null)
                            Towers[y, x].Attack(Enemies, y * 3 + 1, x * 3 + 1);
                foreach (Enemy enemy in Enemies)
                {
                    if (enemy.Health > 0)
                        continue;

                    toRemove.Add(enemy);
                    //Add money here
                }
                if (toRemove.Count > 0)
                {
                    for (int i = 0; i < toRemove.Count; i++)
                        Enemies.Remove(toRemove[i]);
                    toRemove = new List<Enemy>();
                }

                //Move Enemies and remove out of bounds ones
                foreach (Enemy enemy in Enemies)
                {
                    if (enemy.Move(Map.Floor, Edges))
                    {
                        toRemove.Add(enemy);
                        Health -= enemy.Health;
                        //Check for game over
                    }
                }
                if (toRemove.Count > 0)
                {
                    for (int i = 0; i < toRemove.Count; i++)
                        Enemies.Remove(toRemove[i]);
                    toRemove = new List<Enemy>();
                }

                if (Enemies.Count == 0 && waveOver)
                    return; //Wave complete event here

                //Draw Enemies
                foreach (Enemy enemy in Enemies)
                {
                    Console.SetCursorPosition(enemy.Position.X + 2, enemy.Position.Y + 2);
                    Console.Write(enemy.Appearance);
                }

                Thread.Sleep(200);
            }
        }
    }
}
