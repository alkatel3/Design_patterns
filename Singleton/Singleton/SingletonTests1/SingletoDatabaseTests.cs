using Autofac;
using NUnit.Framework;
using Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Tests
{
    [TestFixture]
    public class SingletoDatabaseTests
    {
        private IContainer container;

        [SetUp]
        private void SetUp()
        {
            var cb =new ContainerBuilder()
        }

        [Test]
        public void IsSingletonTest()
        {
            var db = SingletoDatabase.Instance;
            var db2 = SingletoDatabase.Instance;

            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletoDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void SingletonTotalPopulationTest()
        {
            // запит до живої бази даних
            // крихкий тест
            var rf = new SingletonRecordFinder();
            var names = new[] { "Seoul", "Mexico City" };
            int tp = rf.TotalPopulation(names);

            Assert.That(tp, Is.EqualTo(17_500_000 + 17_400_000));
        }

        [Test]
        public void DependentTotalPopulationTest()
        {
            var db = new DummyDatabase();
            var rf = new CongigurableRecordFinder();

            Assert.That(
                rf.TotalPopulation(new[] { "anpha", "gamma" }),
                Is.EqualTo(4));
        }
    }
}