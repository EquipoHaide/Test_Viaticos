using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructura.Transversal.Plataforma.Extensiones
{
    public static class IQueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        /*public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        }*/


        public static IQueryable<Entity> DistinctBy<Entity>(this IQueryable<Entity> list, System.Linq.Expressions.Expression<Func<Entity, int>> keySelector)
        {
            return list.GroupBy(keySelector).Select(i => i.FirstOrDefault());
        }
        public static ConsultaPaginada<TResult> Paginar<TResult>(this IQueryable<TResult> list, int pagina = 1, int elementos = 20)
        {
            /*ConsultaPaginada<TResult> result = new ConsultaPaginada<TResult>
            {
                Pagina = pagina,
                ElementosPorPagina = elementos,
                TotalElementos = list.Count(),
                Elementos = list.Skip((pagina - 1) * elementos).Take(elementos).ToList()
            };*/

            ConsultaPaginada<TResult> result = new ConsultaPaginada<TResult>(pagina, elementos, list.Count(), list.Skip((pagina - 1) * elementos).Take(elementos).ToList());

            return result;
        }
    }
}