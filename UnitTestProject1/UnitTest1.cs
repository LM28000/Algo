using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace GameJeu
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Joueur j = new Joueur("Louis");
            bool result = j.Contain("fjffjb");
            Assert.AreEqual(false, result);

        }
    }
}
