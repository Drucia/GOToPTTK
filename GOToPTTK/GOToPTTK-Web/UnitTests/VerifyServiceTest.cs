using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    class VerifyServiceTest
    {
        private VerifyService _verifyService;

        public VerifyServiceTest()
        {
            _verifyService = new VerifyService();
        }

        private List<Wycieczka> CreateTripsList()
        {
            return new List<Wycieczka>()
            {
                new Wycieczka()
                {
                    Id = 1,
                    PrzodownikUzytkownikId = 3,
                },
                new Wycieczka()
                {
                    Id = 5,
                    PrzodownikUzytkownikId = 3,
                },
                new Wycieczka()
                {
                    Id = 3,
                    PrzodownikUzytkownikId = null,
                },
                new Wycieczka()
                {
                    Id = 2,
                    PrzodownikUzytkownikId = 2,
                }
            };
        }

        [Test]
        public void ReturnFalseWhenTripListIsEmpty()
        {
            var tripList = new List<Wycieczka>();
            Assert.IsFalse(_verifyService.CanGuideVerifyTrip(tripList, 3, 1));
        }

        [Test]
        public void ReturnFalseWhenTripHasNoGuide()
        {
            var tripList = CreateTripsList();
            Assert.IsFalse(_verifyService.CanGuideVerifyTrip(tripList, 3, 3));
        }

        [Test]
        public void ReturnFalseWhenTripHasAnotherGuide()
        {
            var tripList = CreateTripsList();
            Assert.IsFalse(_verifyService.CanGuideVerifyTrip(tripList, 2, 3));
        }

        [Test]
        public void ReturnTrueWhenTripHasTheSameGuide()
        {
            var tripList = CreateTripsList();
            Assert.IsTrue(_verifyService.CanGuideVerifyTrip(tripList, 2, 2));
        }
        [Test]
        public void ReturnFalseWhenTripIsNotInList()
        {
            var tripList = CreateTripsList();
            Assert.IsFalse(_verifyService.CanGuideVerifyTrip(tripList, 10, 1));
        }
    }
}
