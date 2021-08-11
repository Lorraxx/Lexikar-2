using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lexikar_2.Data;
using Lexikar_2.Models;
using Microsoft.AspNetCore.Authorization;
using Lexikar_2.DAL;
using Lexikar_2.DAL.Templates;
using System.Linq.Expressions;

namespace Lexikar_2.Controllers
{
    using WherePredicate    = Expression<Func<Translation, bool>>;
    using OrderByPredicate  = Expression<Func<Translation, string>>;

    public class TranslationsController : Controller
    {
        private const int _defaultPageSize = 20;

        private ITranslationRepository translationRepository;

        public TranslationsController(ITranslationRepository translationRepository)
        {
            this.translationRepository = translationRepository;
        }
        
        // GET: Translations
        /// <summary>
        ///     Displays filtered and paged index page ordered by
        ///     either Original or Translated column.
        /// </summary>
        /// <param name="sortOrder">
        ///     What column to sort by and in what order. 
        ///     Options:    Original, 
        ///                 Original_desc, 
        ///                 Translated_desc, 
        ///                 Empty_string
        /// </param>
        /// <param name="currentFilter"> 
        ///     Keeps track of a string by which should 
        ///     be the results filtered.
        /// </param>                                       
        /// <param name="searchString">
        ///     Used to change the currentFilter string.
        /// </param>                                        
        /// <param name="page">
        ///     Number of page to display.
        /// </param>                                                
        /// <returns></returns>
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            #region Constants
            // Order parameters
            const string 
                orderByOriginal         = "Original",
                orderByOriginal_Desc    = "Original_desc",
                orderByTranslated       = "",
                orderByTranslated_Desc  = "Translated_desc";
            
            const int 
                pageSize                = _defaultPageSize;
            #endregion

            #region Variables
            int
                pageNumber              = (page ?? 1);
            
            
            // Filter / order settings
            
            bool 
                useDescendingOrder      = false;
            WherePredicate 
                wherePredicate          = null;
            OrderByPredicate
                orderByPredicate        = null;
            
            #region ViewBag
            ViewBag.CurrentSort         = sortOrder;
            ViewBag.TranslatedSortParm  = string.IsNullOrEmpty(sortOrder)
                                        ? orderByTranslated_Desc
                                        : orderByTranslated;
            
            ViewBag.OriginalSortParm    = sortOrder == orderByOriginal
                                        ? orderByOriginal_Desc
                                        : orderByOriginal;
            #endregion ViewBag
            #endregion Variables

            // Display filer to user
            if (searchString == null)
                searchString = currentFilter;
            else
                page = 1;

            ViewBag.CurrentFilter = searchString;

            // Set filter
            if (!String.IsNullOrEmpty(searchString))
            {
                wherePredicate = (t => t.Original   .Contains(searchString)
                                    || t.Translated .Contains(searchString));
            }

            // Set Order
            switch (sortOrder)
            {
                case orderByOriginal_Desc:
                    orderByPredicate = t => t.Original;
                    useDescendingOrder = true;
                    break;
                case orderByOriginal:
                    orderByPredicate = t => t.Original;
                    useDescendingOrder = false;
                    break;
                case orderByTranslated_Desc:
                    orderByPredicate = t => t.Translated;
                    useDescendingOrder = true;
                    break;
                default:
                    orderByPredicate = t => t.Translated;
                    useDescendingOrder = false;
                    break;
            }

            return View(await translationRepository.
                GetIndexPage
                (
                    wherePredicate, 
                    orderByPredicate, 
                    useDescendingOrder, 
                    pageNumber, 
                    pageSize
                ));
        }

        //GET: Translations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await translationRepository.GetById((int)id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // GET: Translations/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Translations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,Nominal,Verbal,Other,Original,Translated,Source,Definition,Systematics,Note")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                await translationRepository.Add(translation);
                return RedirectToAction(nameof(Index));
            }
            return View(translation);
        }

        // GET: Translations/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await translationRepository.GetById((int)id);
            if (translation == null)
            {
                return NotFound();
            }
            return View(translation);
        }

        // POST: Translations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("ID,Nominal,Verbal,Other,Original,Translated,Source,Definition,Systematics,Note")] Translation translation)
        {
            if (id != translation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await translationRepository.Update(translation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await TranslationExists(translation.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(translation);
        }

        // GET: Translations/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await translationRepository.FirstOrDefault(t => t.ID == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // POST: Translations/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var translation = await translationRepository.GetById(id);
            await translationRepository.Remove(translation);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TranslationExists(int id)
        {
            return await translationRepository.Any(t => t.ID == id);
        }
    }
}
