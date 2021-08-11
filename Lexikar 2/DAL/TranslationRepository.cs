using Lexikar_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Lexikar_2.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Lexikar_2.DAL.Templates;
using X.PagedList;

namespace Lexikar_2.DAL
{
    public class TranslationRepository : EfRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(ApplicationDbContext context) : base(context)
        {
        }

        
        public async Task<IEnumerable<Translation>> GetIndexPage(
            Expression<Func<Translation, bool>>     _where, 
            Expression<Func<Translation, string>>   _order, 
            bool desc,
            int pageNumber,
            int pageSize)
        {
            IQueryable<Translation> query = Context.Set<Translation>();
            if (_where is not null)
            {
                query = query.Where(_where);
            }
            if (desc)
                query = query.OrderByDescending(_order);
            else
                query = query.OrderBy(_order);

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
