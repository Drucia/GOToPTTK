using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    class PlaceListServiceTest
    {
        private PlaceListService _placeListService;

        public PlaceListServiceTest()
        {
            _placeListService = new PlaceListService();
        }

        private List<Miejsce> CreatePlaceList()
        {
            return new List<Miejsce>()
            {
                new Miejsce()
                {
                    Id = 1,
                    Nazwa = "Kuźnice",
                    SzerokoscGeograficzna = 49.2700000M,
                    DlugoscGeograficzna = 19.9800000M,
                    WysokoscNpm = 1025
                },
                new Miejsce()
                {
                    Id = 2,
                    Nazwa = "Potok Bystra",
                    SzerokoscGeograficzna = 49.2689870M,
                    DlugoscGeograficzna = 19.9814760M,
                    WysokoscNpm = 1019
                },
                new Miejsce()
                {
                    Id = 3,
                    SzerokoscGeograficzna = 49.2449030M,
                    DlugoscGeograficzna = 20.0064210M,
                    WysokoscNpm = 1511
                }
            };
        }

        private List<Odcinek> CreateRoutesList()
        {
            return new List<Odcinek>()
            {
                new Odcinek()
                {
                    Id = 1,
                    OdcinekPunktowanyId = 2,
                    OdcinekWłasnyId = null,
                    WycieczkaId = 4,
                    Zweryfikowany = "OCZEKUJACY",
                    OdcinekPunktowany = new OdcinekPunktowany()
                    {
                        Id = 2,
                        PoczatekId = 3,
                        KoniecId = 4,
                        WykazTrasId = 1,
                        GrupaGorskaId = 5
                    }
                },
                new Odcinek()
                {
                    Id = 2,
                    OdcinekPunktowanyId = null,
                    OdcinekWłasnyId = 1,
                    WycieczkaId = 4,
                    Zweryfikowany = "OCZEKUJACY",
                    OdcinekWłasny = new OdcinekWłasny()
                    {
                        Id = 2,
                        PoczatekId = 1,
                        KoniecId = 5,
                        GrupaGorskaId = 4
                    }

                },
            };
        }

        [Test]
        public void ReturnFalseWhenPlaceIsNull()
        {
            Miejsce place = null;
            var placeList = new List<Miejsce>() { new Miejsce() };
            Assert.IsFalse(_placeListService.ExistInPlaceList(placeList, place));
        }

        [Test]
        public void ReturnFalseWhenPlaceListIsEmpty()
        {
            Miejsce place = new Miejsce()
            {
                Id = 1,
                Nazwa = "Kuźnice",
                SzerokoscGeograficzna = 49.2700000M,
                DlugoscGeograficzna = 19.9800000M,
                WysokoscNpm = 1025
            };
            var placeList = new List<Miejsce>();
            Assert.IsFalse(_placeListService.ExistInPlaceList(placeList, place));
        }

        [Test]
        public void ReturnTrueWhenPlaceListHasThisPlace()
        {
            Miejsce place = new Miejsce()
            {
                Id = 4,
                Nazwa = "",
                SzerokoscGeograficzna = 49.2700000M,
                DlugoscGeograficzna = 19.9800000M,
                WysokoscNpm = 1025
            };
            var placeList = CreatePlaceList();
            Assert.IsTrue(_placeListService.ExistInPlaceList(placeList, place));
        }

        [Test]
        public void ReturnTrueWhenIsTripRouteWhichHasThisPlace()
        {
            Miejsce place = new Miejsce()
            {
                Id = 1,
                Nazwa = "Kuźnice",
                SzerokoscGeograficzna = 49.2700000M,
                DlugoscGeograficzna = 19.9800000M,
                WysokoscNpm = 1025
            };
            var routes = CreateRoutesList();
            Assert.IsTrue(_placeListService.IsPlaceInTrips(routes, place));
        }

        [Test]
        public void ReturnFalseWhenAnyTripRouteHasThisPlace()
        {
            Miejsce place = new Miejsce()
            {
                Id = 10,
                Nazwa = "",
                SzerokoscGeograficzna = 39.0700000M,
                DlugoscGeograficzna = 29.9800000M,
                WysokoscNpm = 1025
            };
            var routes = CreateRoutesList();
            Assert.IsFalse(_placeListService.IsPlaceInTrips(routes, place));
        }

        [Test]
        public void ReturnFalseWhenTripRouteIsEmpty()
        {
            Miejsce place = new Miejsce()
            {
                Id = 10,
                Nazwa = "",
                SzerokoscGeograficzna = 39.0700000M,
                DlugoscGeograficzna = 29.9800000M,
                WysokoscNpm = 1025
            };
            var routes = new List<Odcinek>();
            Assert.IsFalse(_placeListService.IsPlaceInTrips(routes, place));
        }

        [Test]
        public void ReturnTrueWhenPlaceIsNullInTrips()
        {
            Miejsce place = null;
            var routes = CreateRoutesList();
            Assert.IsTrue(_placeListService.IsPlaceInTrips(routes, place));
        }
    }
}
