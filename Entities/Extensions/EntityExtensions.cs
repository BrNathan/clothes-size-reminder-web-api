namespace Entities.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsEntityNull(this IEntity entity)
        {
            return entity == null;
        }

        public static bool IsEntityEmpty(this IEntity entity)
        {
            return !entity.Id.HasValue;
        }
    }
}
