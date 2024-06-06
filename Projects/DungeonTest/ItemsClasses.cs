namespace ProjectArchive.Projects.DungeonTest
{
    enum ItemType
    {
        None, //DEBUG

        Tile,

        Axe
    }

    enum ItemRarity
    {
        Common,
        Uncommon,
        Rare
    }

    abstract class Item_Base //Add a use function
    {
        public string Name { get; set; }
        public char Icon { get; set; }
        public ItemType Type { get; set; }
        public ItemRarity Rarity { get; set; }
        public string Description { get; set; }

        public string InvInfo(int amount)
        {
            return $"{Icon} {Name} ({amount}x) [{Rarity} {Type}]";
        }
        public string HeldInfo(int amount)
        {
            return $"{Name} ({amount}x) [{Rarity} {Type}]";
        }

        public abstract string FullInfo(int amount);
        public abstract void Use(Vector2 target);
    }

    class Item_Tile : Item_Base
    {
        public int RequiredStrength { get; }
        public ItemType RequiredTool { get; }
        public bool IsSolid { get; }

        public Item_Tile(string name, ItemType reqtool, int reqstr, bool solid, char icon, ItemRarity rarity, string desc)
        {
            Name = name;
            Icon = icon;
            Type = ItemType.Tile;
            Rarity = rarity;
            Description = desc;

            RequiredStrength = reqstr;
            RequiredTool = reqtool;
            IsSolid = solid;
        }

        public override string FullInfo(int amount)
        {
            return $"{Icon} {Name} ({amount}x):\n > Rarity: {Rarity}\n > Type:{Type}\n > Tool: {RequiredTool} ({RequiredStrength})\n > Info: {Description}";
        }

        public override void Use(Vector2 target)
        {
            ref Item_Tile targetTile = ref World.MapTile(Player.WorldPos).Tile(target);

            if (targetTile != Items.none)
                return;

            targetTile = this;
            Player.HeldItem.Amount--;

            if (Player.HeldItem.Amount < 1)
                Player.HeldItem = new Slot();
        }
    }

    class Item_Tool : Item_Base
    {
        public int Strength { get; }

        public Item_Tool(string name, ItemType tooltype, int strength, ItemRarity rarity, string desc)
        {
            Name = name;
            Icon = '-'; //TEMP ICON
            Type = tooltype;
            Rarity = rarity;
            Description = desc;

            Strength = strength;
        }

        public override string FullInfo(int amount)
        {
            return $"{Icon} {Name} ({amount}x):\n > Rarity: {Rarity}\n > Type:{Type}\n > Power: {Strength}\n > Info: {Description}";
        }

        public override void Use(Vector2 target)
        {
            ref Item_Tile targetTile = ref World.MapTile(Player.WorldPos).Tile(target);

            if (targetTile.RequiredTool != Type || targetTile.RequiredStrength > Strength)
                return;

            if (Storage.TryStore(targetTile, 1, Player.Items))
                targetTile = Items.none;
            else if (Storage.TryDrop(targetTile, 1, ref World.MapTile(Player.WorldPos).Floor(target)))
                targetTile = Items.none;
        }
    }
}
