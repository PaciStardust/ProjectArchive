namespace ProjectArchive.Projects.DungeonTest
{
    //Static player class
    static class Player
    {
        public static Vector2 Pos;
        public static Vector2 WorldPos;
        public static string Name;
        public static Slot[] Items;
        public static Direction Direction;
        public static Slot HeldItem = new Slot();

        public static void InitPlayer(string name, Vector2 worldpos, Vector2 pos) //Sets all important values on startup and fills the inventory
        {
            Name = name;
            Pos = pos;
            WorldPos = worldpos;
            Direction = Direction.North;

            Items = new Slot[24];
            for (int i = 0; i < Items.Length; i++)
                Items[i] = new Slot();
        }

        public static string PrintLocation() //Utility function to write location when printing map
        {
            return $"{WorldPos.X * World.Size.X + Pos.X}X {WorldPos.Y * World.Size.Y + Pos.Y}Y (Facing {Direction})";
        }

        public static void Move(Direction direction)
        {
            //Change direction if not facing said direction
            if (Direction != direction)
            {
                Direction = direction;
                return;
            }

            //Sets target
            Vector2 target = direction switch
            {
                Direction.North => new Vector2(Pos.Y - 1, Pos.X),
                Direction.South => new Vector2(Pos.Y + 1, Pos.X),
                Direction.East => new Vector2(Pos.Y, Pos.X + 1),
                Direction.West => new Vector2(Pos.Y, Pos.X - 1),
                _ => new Vector2(Pos.Y, Pos.X)
            };

            //Wraparound Code
            Vector2 worldTarget = new Vector2(WorldPos.Y, WorldPos.X);
            Vector2 maxsize = World.Size;
            if (target.Y < 0)
            {
                target.Y = maxsize.Y - 1;
                worldTarget.Y--;
            }
            else if (target.Y >= maxsize.Y)
            {
                target.Y = 0;
                worldTarget.Y++;
            }
            if (target.X < 0)
            {
                target.X = maxsize.X - 1;
                worldTarget.X--;
            }
            else if (target.X >= maxsize.X)
            {
                target.X = 0;
                worldTarget.X++;
            }

            //Stops if target is solid or out of bounds
            if (worldTarget.Y >= maxsize.Y || worldTarget.Y < 0 || worldTarget.X >= maxsize.X || worldTarget.X < 0)
                return;
            if (World.MapTile(worldTarget).Tile(target).IsSolid)
                return;

            //Sets player position
            Pos = target;
            WorldPos = worldTarget;
        }

        public static void TryUse()
        {
            if (HeldItem.Amount == 0)
                return;

            Vector2 target = Direction switch
            {
                Direction.North => new Vector2(Pos.Y - 1, Pos.X),
                Direction.South => new Vector2(Pos.Y + 1, Pos.X),
                Direction.East => new Vector2(Pos.Y, Pos.X + 1),
                Direction.West => new Vector2(Pos.Y, Pos.X - 1),
                _ => new Vector2(-1, -1)
            };

            if (target.X < 0 || target.Y < 0 || target.X >= World.Size.X | target.Y >= World.Size.Y)
                return;

            HeldItem.Item.Use(target);
        }

        public static void TryPickup()
        {
            ref Slot floorSlot = ref World.MapTile(WorldPos).Floor(Pos);

            if (floorSlot.Amount == 0)
                return;

            if (Storage.TryStore(floorSlot.Item, floorSlot.Amount, Items))
                floorSlot = new Slot();
        }
    }
}
