using Microsoft.VisualStudio.TestTools.UnitTesting;
using OverEngineering.Logic;
using System;

namespace Tests
{
    [TestClass]
    public class UrlQueryBuilderTests
    {
        [TestMethod]
        public void BuildLevelQueryTest()
        {
            var builder = new UrlQueryBuilder
            {
                From = DateTime.Parse("2020-11-23T00:00:00"),
                To = DateTime.Parse("2020-11-24T00:00:00")
            };
            var actual = builder.BuildLevelQuery();
            var expected = "wasserstand/isar/muenchen-tieraerztl-hochschule-16516008/messwerte/tabelle?beginn=23.11.2020&ende=24.11.2020";

            Assert.AreEqual(expected, actual);
        }
    }
}
