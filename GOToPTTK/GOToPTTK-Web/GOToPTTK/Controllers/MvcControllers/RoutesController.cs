using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GOToPTTK.Model.Extensions;

namespace GOToPTTK.Controllers.MvcControllers
{
    [Route("wykaz/{routeListId}/trasy", Name = "Routes")]
    public class RoutesController : Controller
    {
        private const int DEFAULT_PAGE = 1;
        private const int DEFAULT_PAGE_SIZE = 10;
        private readonly ApplicationDbContext _dbContext;
        private readonly IRouteListService _routeListService;

        public RoutesController(ApplicationDbContext dbContext, IRouteListService routeListService)
        {
            _dbContext = dbContext;
            _routeListService = routeListService;
        }

        // GET: Routes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Routes/Create
        [Route("dodaj", Name = "CreateRoute")]
        public ActionResult Create()
        {
            var places = _dbContext.Miejsce.ToList();
            var mountainGroups = _dbContext.GrupaGorska.ToList();
            ViewBag.Groups = new SelectList(mountainGroups, "Id", "Nazwa", null, "RegionGorski");
            ViewBag.Places = new SelectList(places, "Id", "Nazwa");
            
            return View();
        }

        // POST: Routes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("dodaj", Name = "CreateRoute")]
        public ActionResult Create(OdcinekPunktowany route, int routeListId)
        {
            bool routeExistsInRouteList = false;
            var routes = _routeListService.GetRoutesFromRouteList(_dbContext.OdcinekPunktowany.ToList(), routeListId);
            if (ModelState.IsValid && (routeExistsInRouteList = _routeListService.EquivalentRouteExistsInRouteList(routes, route)) == false)
            {
                route.WykazTrasId = routeListId;
                _dbContext.OdcinekPunktowany.Add(route);
                _dbContext.SaveChanges();
                return RedirectToRoute("RouteCreated");
            }
            else
            {
                var places = _dbContext.Miejsce.ToList();
                var mountainGroups = _dbContext.GrupaGorska.ToList();
                ViewBag.Places = new SelectList(places, "Id", "Nazwa");
                ViewBag.RouteExists = routeExistsInRouteList;
                ViewBag.Groups = new SelectList(mountainGroups, "Id", "Nazwa", null, "RegionGorski");
                return View(route);
            }
        }

        [Route("dodano", Name = "RouteCreated")]
        public ActionResult CreateSuccessful()
        {
            return View();
        }

        [Route("lista-pusta", Name = "EmptyList")]
        public ActionResult NoRoutes()
        {
            return View();
        }

        [Route("lista", Name = "ListRoutes")]
        public ActionResult Routes(int routeListId, int page = DEFAULT_PAGE, int pageSize=DEFAULT_PAGE_SIZE)
        {
            var routes = (_routeListService.GetRoutesFromRouteList(_dbContext.GradedRoutesWithIncludedRelatedData(), routeListId));
            if(routes.Count == 0)
            {
                return RedirectToRoute("EmptyList");
            }

            IList<OdcinekPunktowany> paginatedRoutes;
            try
            {
                paginatedRoutes = routes.PaginateList(pageSize, page);
            }
            catch (Exception)
            {
                paginatedRoutes = routes.PaginateList(DEFAULT_PAGE_SIZE, DEFAULT_PAGE);
            }

            ViewBag.PageCount = Math.Ceiling((decimal)routes.Count / pageSize);
            ViewBag.PageId = page;
            ViewBag.PageSize = pageSize;
            return View(paginatedRoutes);
        }

        // GET: Routes/Create
        [Route("edytuj/{routeId}", Name = "ModifyRoute")]
        public ActionResult Modify(int routeListId, int routeId)
        {
            var route = _dbContext.OdcinekPunktowany.FirstOrDefault(r => r.Id == routeId);
            var places = _dbContext.Miejsce.ToList();
            ViewBag.Places = new SelectList(places, "Id", "Nazwa");
            var mountainGroups = _dbContext.GrupaGorska.ToList();
            ViewBag.Groups = new SelectList(mountainGroups, "Id", "Nazwa", null, "RegionGorski");
            return View(route);
        }

        // POST: Routes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edytuj/{routeId}", Name = "ModifyRoute")]
        public ActionResult Modify(OdcinekPunktowany route, int routeListId, int routeId)
        {
            bool routeExistsInRouteList = false;
            var routeSet = _dbContext.OdcinekPunktowany.AsNoTracking();
            var routes = _routeListService.GetRoutesFromRouteList(routeSet.ToList(), routeListId);
            if (ModelState.IsValid && (routeExistsInRouteList = _routeListService.EquivalentRouteExistsInRouteList(routes, route)) == false)
            {   
                _dbContext.Update(route);
                _dbContext.SaveChanges();
                return RedirectToRoute("RouteModified");
            }
            else
            {
                var places = _dbContext.Miejsce.ToList();
                ViewBag.Places = new SelectList(places, "Id", "Nazwa");
                var mountainGroups = _dbContext.GrupaGorska.ToList();
                ViewBag.Groups = new SelectList(mountainGroups, "Id", "Nazwa", null, "RegionGorski");
                ViewBag.RouteExists = routeExistsInRouteList;
                return View(route);
            }
        }

        [Route("zmodyfikowano", Name = "RouteModified")]
        public ActionResult ModifySuccessful()
        {
            return View();
        }

       

        // GET: Routes/Delete/5
        [Route("usun/{routeId}", Name="DeleteRoute")]
        public ActionResult Delete(int routeListId, int routeId)
        {
            return View();
        }

        // POST: Routes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("usun/{routeId}", Name ="DeleteRoute")]
        public ActionResult DeletePost(int routeListId, int routeId)
        {
            var route = _dbContext.OdcinekPunktowany.Include(r => r.WykazTras).FirstOrDefault(r => r.Id == routeId);
            var routeList = route.WykazTras;
            if(routeList.Obowiazuje)
            {
                return RedirectToRoute("RouteNotDeleted");
            }
            else
            {
                _dbContext.Remove(route);
                _dbContext.SaveChanges();
                return RedirectToRoute("RouteDeleted");
            }
        }

        [Route("usunieto", Name = "RouteDeleted")]
        public ActionResult DeleteSuccessful()
        {
            return View();
        }

        [Route("nieusunieto", Name = "RouteNotDeleted")]
        public ActionResult DeleteUnsuccessful()
        {
            return View();
        }

    }
}