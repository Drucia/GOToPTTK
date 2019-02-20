using System;
using System.Collections.Generic;
using System.Data;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Services;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class RouteListServiceTest
    {
        private RouteListService _routeListService;

        public RouteListServiceTest()
        {
            _routeListService = new RouteListService();
        }

        private List<OdcinekPunktowany> CreateRouteList()
        {
            return new List<OdcinekPunktowany>()
            {
                 new OdcinekPunktowany()
                {
                Id = 2,
                PoczatekId = 3,
                KoniecId = 4,
                WykazTrasId = 1,
                GrupaGorskaId = 1
                },
                 new OdcinekPunktowany()
                {
                Id = 3,
                PoczatekId = 5,
                KoniecId = 6,
                WykazTrasId = 1,
                GrupaGorskaId = 1
                }
            };
        }

        [Test]
        public void ReturnFalseWhenRouteIsNull()
        {
            OdcinekPunktowany route = null;
            var routeList = new List<OdcinekPunktowany>(){new OdcinekPunktowany()};
            Assert.IsFalse(_routeListService.EquivalentRouteExistsInRouteList(routeList, route));
        }

        [Test]
        public void ReturnFalseWhenRouteDoesNotExistInRouteList()
        {
            OdcinekPunktowany route = new OdcinekPunktowany()
            {
                Id = 1,
                PoczatekId = 1,
                KoniecId = 2,
                WykazTrasId = 1,
                GrupaGorskaId = 1
            };
            var routeList = CreateRouteList();
            Assert.IsFalse(_routeListService.EquivalentRouteExistsInRouteList(routeList, route));
        }

        [Test]
        public void ReturnFalseWhenRouteListIsEmpty()
        {
            OdcinekPunktowany route = new OdcinekPunktowany()
            {
                Id = 1,
                PoczatekId = 1,
                KoniecId = 2,
                WykazTrasId = 1,
                GrupaGorskaId = 1
            };
            var routeList = new List<OdcinekPunktowany>();
            Assert.IsFalse(_routeListService.EquivalentRouteExistsInRouteList(routeList, route));
        }

        [Test]
        public void ReturnFalseWhenRouteListIsNull()
        {
            OdcinekPunktowany route = new OdcinekPunktowany()
            {
                Id = 1,
                PoczatekId = 1,
                KoniecId = 2,
                WykazTrasId = 1,
                GrupaGorskaId = 1
            };
            Assert.IsFalse(_routeListService.EquivalentRouteExistsInRouteList(null, route));
        }


        [Test]
        public void ReturnTrueWhenEquivalentRouteExistsInRouteList()
        {
            var route = new OdcinekPunktowany()
            {
                Id = -1,
                PoczatekId = 3,
                KoniecId = 4,
                WykazTrasId = 1,
                GrupaGorskaId = 1
            };

            Assert.IsTrue(_routeListService.EquivalentRouteExistsInRouteList(CreateRouteList(), route));
        }
    }
    
}
