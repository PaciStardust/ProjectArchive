namespace ProjectArchive.Projects.DungeonTest
{
    static class Items
    {
        public static Item_Tile none = new Item_Tile("Empty", ItemType.None, 0, false, ' ', ItemRarity.Common, "Nothing here.");

        public static Item_Tile tree = new Item_Tile("Tree", ItemType.Axe, 1, true, '▲', ItemRarity.Common, "A tree.");

        public static Item_Tool axeWood = new Item_Tool("Wooden Axe", ItemType.Axe, 1, ItemRarity.Common, "A basic wooden axe.");
    }
}
