using NUnit.Framework;
using Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.Tests
{
    [TestFixture]
    public class SingletoDatabaseTests
    {
        [Test]
        public void IsSingleton()
        {
            var db = SingletoDatabase.Instance;
            var db2 = SingletoDatabase.Instance;

            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletoDatabase.Count, Is.EqualTo(1));
        }
    }
}