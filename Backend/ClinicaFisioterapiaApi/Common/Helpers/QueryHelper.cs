using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace ClinicaFisioterapiaApi.Common.Helpers
{
    public static class QueryHelper
    {
        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> query,
            string? sort,
            string defaultSort = "Id")
        {
            if (string.IsNullOrWhiteSpace(sort))
                return query.OrderBy(defaultSort);

            try
            {
                return query.OrderBy(sort);
            }
            catch
            {
                // Fallback de segurança: ordena por padrão se o campo informado for inválido
                return query.OrderBy(defaultSort);
            }
        }
    }
}
