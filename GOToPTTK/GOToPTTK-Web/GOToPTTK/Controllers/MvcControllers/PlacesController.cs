using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Extensions;
using GOToPTTK.Model.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Controllers.MvcControllers
{
    public class PlacesController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly IPlaceListService _placeListService;
            private readonly ITripListService _tripListService;

            public PlacesController(ApplicationDbContext context, IPlaceListService placeListService, ITripListService tripListService)
            {
                _context = context;
                _placeListService = placeListService;
                _tripListService = tripListService; 
            }

            // GET: Places
            public IActionResult Index()
            {
                var places = _context.Miejsce.ToList();
                if (places.Count == 0)
                    RedirectToRoute("NoPlaces");
                // if list is empty display communicate
                return View(places);
            }

            // GET: Places/Delete_Edit
            public IActionResult Delete_Edit()
            {
                var places = _context.Miejsce.ToList();
                if (places.Count == 0)
                    RedirectToRoute("NoPlaces");
            
                return View(places);
            }

            public IActionResult NoPlaces()
            {
                return View();
            }

            public IActionResult ModifySuccess()
            {
                return View();
            }

            public IActionResult Delete_Error()
            {
                return View();
            }

        public IActionResult DeleteSuccess()
            {
                return View();
            }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var miejsce = await _context.Miejsce
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (miejsce == null)
                {
                    return NotFound();
                }

                return View(miejsce);
            }

            // GET: Places/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Places/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,SzerokoscGeograficzna,DlugoscGeograficzna,WysokoscNpm,Nazwa,Opis,GodzinaOtwarcia,GodzinaZamkniecia")] Miejsce miejsce)
            {
                var is_in_list = _placeListService.ExistInPlaceList(_context.Miejsce.ToList(), miejsce);

                if (ModelState.IsValid && !is_in_list)
                {
                    _context.Add(miejsce);
                    await _context.SaveChangesAsync();
                    return RedirectToRoute("CreateSuccess");
                }
                if (is_in_list)
                {
                    ModelState.AddModelError("SzerokoscGeograficzna", "Taki punkt juz istnieje. Wprowadz inne dane.");
                }

                return View(miejsce);
            }

            // GET: Places/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var miejsce = await _context.Miejsce.FindAsync(id);
                if (miejsce == null)
                {
                    return NotFound();
                }
                return View(miejsce);
            }

            // POST: Places/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,SzerokoscGeograficzna,DlugoscGeograficzna,WysokoscNpm,Nazwa,Opis,GodzinaOtwarcia,GodzinaZamkniecia")] Miejsce miejsce)
            {
                var placesSet = _context.Miejsce.AsNoTracking();
                var is_in_list = _placeListService.ExistInPlaceList(placesSet.ToList(), miejsce);

                if (id != miejsce.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid && !is_in_list)
                {
                    _context.Update(miejsce);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ModifySuccess));
                }
                
                if (is_in_list)
                {
                    ModelState.AddModelError("SzerokoscGeograficzna", "Taki punkt juz istnieje. Wprowadz inne dane.");
                }
                return View(miejsce);
            }

            // GET: Places/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var miejsce = await _context.Miejsce
                    .FirstOrDefaultAsync(m => m.Id == id);
                bool can_delete = !_placeListService.IsPlaceInTrips(_context.GradedTripRoutesWithIncludedRoutes(), miejsce);

                if (miejsce == null)
                {
                    return NotFound();
                }

                if (!can_delete)
                    return RedirectToAction(nameof(Delete_Error));

                return View(miejsce);
            }

            // POST: Places/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var miejsce = await _context.Miejsce.FindAsync(id);
                _context.Miejsce.Remove(miejsce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DeleteSuccess));
            }

            private bool MiejsceExists(int id)
            {
                return _context.Miejsce.Any(e => e.Id == id);
            }
        }
    }

