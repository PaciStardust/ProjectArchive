namespace ProjectArchive.Projects.ConsoleDefense
{
    abstract class Enemy
    {
        public string Appearance;
        public Int2D Heading;
        public Int2D Position;
        public int Speed;
        public int Health;

        public bool Move(MapTile[,] tiles, Int2D edges) //NEEDS SUPPORT FOR SPEED UNDER 1
        {
            for (int i = 0; i < Speed; i++)
            {
                if (Position.Y % 3 == 1 && Position.X % 3 == 1)
                {
                    Heading = tiles[Position.Y / 3, Position.X / 3].Tag switch
                    {
                        TileTag.EnemyPathTurnUp => new Int2D(-1, 0),
                        TileTag.EnemyPathTurnDown => new Int2D(1, 0),
                        TileTag.EnemyPathTurnLeft => new Int2D(0, -1),
                        TileTag.EnemyPathTurnRight => new Int2D(0, 1),
                        _ => Heading
                    };
                }

                Position = new Int2D(Position.Y + Heading.Y, Position.X + Heading.X);

                if (Position.Y < 0 || Position.X < 0 || Position.Y >= edges.Y || Position.X >= edges.X)
                    return true;
            }
            return false;
        }
    }

    class Enemy_Basic : Enemy
    {
        public Enemy_Basic(string appear, int health, int speed, Int2D pos, Int2D heading)
        {
            Appearance = appear;
            Position = pos;
            Health = health;
            Heading = heading;
            Speed = speed;
        }
    }
}
