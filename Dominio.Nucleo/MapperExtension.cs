
namespace Dominio.Nucleo
{
    public static class MapperExtension
    {
        /// <summary>
        /// Obtiene una entidad apartir del modelo.
        /// </summary>
        public static TEntity ToEntity<TEntity>(this IModel model)
            where TEntity : IEntity
        {
            return MainMapper.Instance.Mapper.Map<TEntity>(model);
        }
        /// <summary>
        /// Obtiene una entidad con los valores del modelo.
        /// </summary>
        public static TEntity ToEntity<TEntity>(this IModel model, TEntity entity)
            where TEntity : IEntity
        {
            return (TEntity)MainMapper.Instance.Mapper.Map(model, entity, model.GetType(), entity.GetType());
        }
        /// <summary>
        /// Obtiene un modelo a partir de la entidad.
        /// </summary>
        public static TModel ToModel<TModel>(this IEntity entity)
            where TModel : IModel
        {
            return MainMapper.Instance.Mapper.Map<TModel>(entity);
        }
        /// <summary>
        /// Copia un modelo en una nuevo modelo.
        /// </summary>
        public static TModel Copy<TModel>(this TModel entity)
            where TModel : IModel
        {
            return (TModel)MainMapper.Instance.Mapper.Map(entity, entity.GetType(), entity.GetType());
        }
    }
}