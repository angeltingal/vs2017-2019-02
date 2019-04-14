using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Test
{
    [TestClass]
    public class ArtistDAUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var da = new ArtistDA();
            Assert.IsTrue(da.GetCount() > 0);
        }

        [TestMethod]
        public void GetAll()
        {
            var da = new ArtistDA();
            var listado = da.GetAll();
            Assert.IsTrue(listado.Count > 0);
        }


        //[TestMethod]
        //public void Get()
        //{
        //    var da = new ArtistDA();
        //    var entity = da.Get(2);

        //    Assert.IsTrue(entity>0);
        //}



        [TestMethod]
        public void GetAllsp()
        {
            var da = new ArtistDA();
            var listado = da.GetAllsp("Aerosmith");

            Assert.IsTrue(listado.Count > 0);
        }
    }

}
