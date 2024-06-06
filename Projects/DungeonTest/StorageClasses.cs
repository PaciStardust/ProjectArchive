namespace ProjectArchive.Projects.DungeonTest
{
    internal class Slot //Class used for storage arrays
    {
        public Item_Base Item { get; set; }
        public int Amount { get; set; }

        public Slot()
        {
            Item = Items.none;
            Amount = 0;
        }

        public Slot(Item_Base item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public string PrintInfo()
        {
            return Item.InvInfo(Amount);
        }
        public string PrintInfoHeld()
        {
            return Item.HeldInfo(Amount);
        }
    }

    internal static class Storage
    {
        public static void ListLoop(string name, Slot[] array) //Lists all items in pages of 12 and lets you move around with cursor
        {
            int cursorPos = 0;
            int cursorMax = 0;
            List<int> filledIDs = new List<int>();

            bool requiresReload = true;

            while (true)
            {
                Console.Clear();

                //Prepare Variables
                string slottext = "";

                if (requiresReload)
                {
                    filledIDs = new List<int>();

                    //Counting items and storing IDs
                    for (int i = 0; i < array.Length; i++)
                        if (array[i].Amount > 0)
                            filledIDs.Add(i);
                    cursorMax = filledIDs.Count - 1;

                    requiresReload = false;
                }

                //Cursor wrapping, cursed but works lmao
                if (cursorPos < 0)
                {
                    int cursorTarget = cursorPos + (cursorMax / 12 + 1) * 12;
                    cursorPos = cursorTarget > cursorMax ? cursorMax : cursorTarget;
                }
                else if (cursorPos >= filledIDs.Count)
                {
                    if (cursorPos == filledIDs.Count)
                        cursorPos = 0;
                    else if (cursorPos < (cursorMax / 12 + 1) * 12)
                        cursorPos = cursorMax;
                    else
                        cursorPos %= 12;
                }

                //Code to actually display the items, redo Info printing
                if (filledIDs.Count > 0)
                {
                    int startIndex = cursorPos - cursorPos % 12;
                    for (int i = startIndex; i < startIndex + 12; i++)
                    {
                        if (i >= filledIDs.Count)
                            break;
                        else
                        {
                            if (i == cursorPos)
                                slottext += $" > {array[filledIDs[i]].PrintInfo()}\n";
                            else
                                slottext += $"   {array[filledIDs[i]].PrintInfo()}\n";
                        }
                    }
                }
                else
                {
                    slottext += " > No items available\n";
                }
                Console.WriteLine($"{name} ({filledIDs.Count}/{array.Length}):\n\n{slottext}\n(Page {cursorPos / 12 + 1}/{cursorMax / 12 + 1})");

                //Input, add way to select item (equip)
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        cursorPos--;
                        continue;

                    case ConsoleKey.S:
                        cursorPos++;
                        continue;

                    case ConsoleKey.D:
                        cursorPos += 12;
                        continue;

                    case ConsoleKey.A:
                        cursorPos -= 12;
                        continue;

                    case ConsoleKey.Spacebar:
                        SelectedLoop(Player.Items, filledIDs[cursorPos]);
                        requiresReload = true;
                        continue;

                    case ConsoleKey.Escape:
                        return;
                }
            }
        }

        public static void SelectedLoop(Slot[] container, int id)
        {
            int cursorPos = 0;

            List<string> selectedList;
            if (container == Player.Items)
                selectedList = new List<string> { "Hold", "Drop" };
            else
                selectedList = new List<string> { "Hold", "Drop" };

            while (true)
            {
                Console.Clear();

                Console.WriteLine(container[id].Item.FullInfo(container[id].Amount));

                string printstring = "";
                for (int i = 0; i < selectedList.Count; i++)
                {
                    if (cursorPos == i)
                        printstring += $" > {selectedList[i]}\n";
                    else
                        printstring += $"   {selectedList[i]}\n";
                }
                Console.WriteLine($"\nWhat would you like to do?\n{printstring}");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        cursorPos = --cursorPos < 0 ? selectedList.Count - 1 : cursorPos;
                        continue;

                    case ConsoleKey.S:
                        cursorPos = ++cursorPos >= selectedList.Count ? 0 : cursorPos;
                        continue;

                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.Spacebar:
                        switch (selectedList[cursorPos])
                        {
                            case "Hold":
                                HoldItem(container, id);
                                return;

                            case "Drop":
                                if (TryDrop(container[id].Item, container[id].Amount, ref World.MapTile(Player.WorldPos).Floor(Player.Pos)))
                                    container[id] = new Slot();
                                return;

                            default:
                                continue;
                        }
                }
            }
        }

        public static bool TryStore(Item_Base item, int amount, Slot[] array) //Stores items into a slot array and returns if it managed to do so
        {
            if (amount == 0)
                return true;

            int emptyslot = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Item == item)
                {
                    array[i].Amount += amount;
                    return true;
                }

                if (emptyslot == -1 && array[i].Amount == 0)
                    emptyslot = i;
            }

            if (emptyslot != -1)
            {
                array[emptyslot] = new Slot(item, amount);
                return true;
            }
            else
                return false;
        }

        public static bool TryDrop(Item_Base item, int amount, ref Slot target)
        {
            if (target.Amount != 0)
                return false;

            target = new Slot(item, amount);
            return true;
        }


        public static bool TryRemove(Item_Base item, int amount, Slot[] array) //Removes item, return true if actually succeeded
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Item == item)
                {
                    array[i].Amount -= amount;

                    if (array[i].Amount < 1)
                        array[i] = new Slot();

                    return true;
                }
            }
            return false;
        }
        public static void Remove(int id, int amount, Slot[] array)
        {
            array[id].Amount -= amount;

            if (array[id].Amount < 1)
                array[id] = new Slot();
        }

        public static void HoldItem(Slot[] container, int id)
        {
            Slot item = container[id];

            if (item.Item == Player.HeldItem.Item)
            {
                Player.HeldItem.Amount += item.Amount;
                Remove(id, item.Amount, container);
            }
            else
            {
                Slot tempHeld = new Slot(Player.HeldItem.Item, Player.HeldItem.Amount);
                Player.HeldItem = new Slot(item.Item, item.Amount);
                Remove(id, item.Amount, container);
                TryStore(tempHeld.Item, tempHeld.Amount, container);
            }
        }
    }
}
