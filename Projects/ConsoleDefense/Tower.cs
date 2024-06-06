using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive.Projects.ConsoleDefense
{
    abstract class Tower
    {
        public string Name;
        public string Description;
        public string[] Appearance;
        public int Range;
        public int Damage;
        public int Cooldown;
        public int MaxCooldown;

        public abstract void Attack(List<Enemy> enemies, Int2D position);

        public void Attack(List<Enemy> enemies, int ypos, int xpos)
        {
            Attack(enemies, new Int2D(ypos, xpos));
        }
    }

    class Tower_AllDamage : Tower
    {
        public Tower_AllDamage(string name, string desc, string[] appear, int range, int damage, int cooldown)
        {
            Name = name;
            Description = desc;
            Appearance = appear;
            Range = range;
            Damage = damage;
            MaxCooldown = cooldown;
            Cooldown = 0;
        }

        public override void Attack(List<Enemy> enemies, Int2D position)
        {
            if (Cooldown != 0)
            {
                Cooldown--;
                return;
            }
            else
                Cooldown = MaxCooldown;

            Int2D minrange = new Int2D(position.Y - Range, position.X - Range);
            Int2D maxrange = new Int2D(position.Y + Range, position.X + Range);

            foreach (Enemy enemy in enemies)
            {
                if (enemy.Position.Y < minrange.Y || enemy.Position.Y > maxrange.Y || enemy.Position.X < minrange.X || enemy.Position.X > maxrange.X)
                    continue;

                enemy.Health -= Damage;
            }
        }
    }

    class Tower_SingleTarget : Tower
    {
        private int Shots;
        private bool Multi;

        public Tower_SingleTarget(string name, string desc, string[] appear, int range, int damage, int cooldown, int shots, bool multi)
        {
            Name = name;
            Description = desc;
            Appearance = appear;
            Range = range;
            Damage = damage;
            MaxCooldown = cooldown;
            Cooldown = 0;
            Shots = shots;
            Multi = multi;
        }

        public override void Attack(List<Enemy> enemies, Int2D position)
        {
            if (Cooldown != 0)
            {
                Cooldown--;
                return;
            }
            else
                Cooldown = MaxCooldown;

            Int2D minrange = new Int2D(position.Y - Range, position.X - Range);
            Int2D maxrange = new Int2D(position.Y + Range, position.X + Range);

            int shotsLeft = Shots;

            foreach (Enemy enemy in enemies)
            {
                if (shotsLeft <= 0)
                    return;

                if (enemy.Position.Y < minrange.Y || enemy.Position.Y > maxrange.Y || enemy.Position.X < minrange.X || enemy.Position.X > maxrange.X)
                    continue;

                while (shotsLeft > 0 && enemy.Health > 0)
                {
                    enemy.Health -= Damage;
                    shotsLeft--;
                }

                if (!Multi)
                    return;
            }
        }
    }

    class Tower_MultiTarget : Tower
    {
        private int MaxTargets;

        public Tower_MultiTarget(string name, string desc, string[] appear, int range, int damage, int cooldown, int maxtargets)
        {
            Name = name;
            Description = desc;
            Appearance = appear;
            Range = range;
            Damage = damage;
            MaxCooldown = cooldown;
            Cooldown = 0;
            MaxTargets = maxtargets;
        }

        public override void Attack(List<Enemy> enemies, Int2D position)
        {
            if (Cooldown != 0)
            {
                Cooldown--;
                return;
            }
            else
                Cooldown = MaxCooldown;

            Int2D minrange = new Int2D(position.Y - Range, position.X - Range);
            Int2D maxrange = new Int2D(position.Y + Range, position.X + Range);

            int shotsLeft = MaxTargets;

            foreach (Enemy enemy in enemies)
            {
                if (shotsLeft <= 0)
                    return;

                if (enemy.Position.Y < minrange.Y || enemy.Position.Y > maxrange.Y || enemy.Position.X < minrange.X || enemy.Position.X > maxrange.X)
                    continue;

                enemy.Health -= Damage;
                shotsLeft--;
            }
        }
    }

    static class ListTowers
    {
        public static readonly Tower_SingleTarget Turret_Simple = new Tower_SingleTarget("Simple Turret", " > 1 Damage\n > 2 Ticks\n > 6 Range", new string[] { "░│░", "─O─", "░│░" }, 6, 1, 1, 1, false);
    }
}
