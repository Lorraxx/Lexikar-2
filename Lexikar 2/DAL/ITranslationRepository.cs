using Lexikar_2.DAL.Templates;
using Lexikar_2.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lexikar_2.DAL
{
    public interface ITranslationRepository : IAsyncRepository<Translation>
    {
        Task<IEnumerable<Translation>> GetIndexPage(Expression<Func<Translation, bool>> _where,
                                                    Expression<Func<Translation, string>> _order,
                                                    bool desc,
                                                    int pageNumber,
                                                    int pageSize);
    }
}
