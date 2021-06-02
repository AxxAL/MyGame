
namespace MyGame
{
    public static class GUtility
    {
        public static bool OfTypeBoss(Enemy enemy)
        {
            return enemy.GetType() == typeof(Boss);
        }
    }
}