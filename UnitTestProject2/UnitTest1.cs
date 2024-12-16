using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClasseJoueur;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Joueur joueur = new Joueur("test");
            Assert.AreEqual("test", joueur.name);
        }

        [TestMethod]
        public bool ContainTest()
        {
            Joueur joueur = new Joueur("test");
            Assert.AreEqual(false, joueur.Contain("test"));
        }

        [TestMethod]
        public void ScoreTest()
        {
            Joueur joueur = new Joueur("test");
            Assert.AreEqual(0, joueur.score);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Joueur joueur = new Joueur("test");
            Assert.AreEqual(false, joueur.ia);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Joueur joueur = new Joueur("test");
            Assert.AreEqual(0, joueur.indice);
        }
    }
}
