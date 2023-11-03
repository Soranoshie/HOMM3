namespace Inheritance.MapObjects
{
    public interface IMapObject
    {
        void Interact(Player player);
    }

    public interface IOwner : IMapObject
    {
        int Owner { set; }
    }

    public interface IArmy : IMapObject
    {
        Army Army { get; }
    }

    public interface ITreasure : IMapObject
    {
        Treasure Treasure { get; }
    }

    public class Dwelling : IOwner
    {
        public int Owner { get; set; }

        public void Interact(Player player)
        {
            Owner = player.Id;
        }
    }

    public class Mine : IOwner, IArmy, ITreasure
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }

        public void Interact(Player player)
        {
            if (!player.CanBeat(Army))
            {
                player.Die();
                return;
            }

            Owner = player.Id;
            player.Consume(Treasure);
        }
    }

    public class Creeps : IArmy, ITreasure
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }

        public void Interact(Player player)
        {
            if (!player.CanBeat(Army))
            {
                player.Die();
                return;
            }

            player.Consume(Treasure);
        }
    }

    public class Wolves : IArmy
    {
        public Army Army { get; set; }

        public void Interact(Player player)
        {
            // логика волка
        }
    }

    public class ResourcePile : ITreasure
    {
        public Treasure Treasure { get; set; }

        public void Interact(Player player)
        {
            player.Consume(Treasure);
        }
    }

    public static class Interaction
    {
        public static void Make(Player player, IMapObject mapObject)
        {
            mapObject.Interact(player);
        }
    }
}