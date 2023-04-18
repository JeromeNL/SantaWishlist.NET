namespace SantaWishlist.Data.Seeders.Interfaces;

public interface ISeeder<T>
{
    List<T> GetSeeds();
}