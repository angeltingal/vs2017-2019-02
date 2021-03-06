﻿using System;
using App.Entities;
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

        [TestMethod]

        public void IntertSP()
        {
            var da = new ArtistDA();
            var artist = new Artist();
            artist.Name = "Alberto12";
            var id = da.insert(artist);

            Assert.IsTrue(id > 0,"El nombre del artista ya existe");
        }

        [TestMethod]
        public void updateSP()
        {
            var da = new ArtistDA();
            var artist = new Artist();
            artist.Name = "Albert1o";
            artist.ArtistId = 279;
            var registrosAfectados = da.update(artist);

            Assert.IsTrue(registrosAfectados > 0, "El nombre del artista se actualizo correctamente");
        }

        [TestMethod]
        public void deleteSP()
        {
            var da = new ArtistDA();
            var artist = new Artist();
            artist.ArtistId = 275;
            var registrosEliminadi= da.delete(artist);

            Assert.IsTrue(registrosEliminadi > 0, "Registro eliminado");
        }



    }

}
