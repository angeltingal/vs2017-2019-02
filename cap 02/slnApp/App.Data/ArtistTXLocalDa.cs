﻿using App.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public class ArtistTXLocalDa : BaseConnection
    {
        /// <summary>
        /// Permite obtener la cantidad de registros 
        /// que existe en la tabla artista
        /// </summary
        /// <returns>Retorna el numero de registro 
        /// </returns>
        public int GetCount()
        {
            var result = 0;

            var sql = "SELECT COUNT(1) FROM Artist";
            /*1.- creando la instancia del objeto connection*/
            using (IDbConnection
                cn = new SqlConnection(base.ConnectionString))
            {
                /*2.- Creando el objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open(); //* Abriendo la canexion a la base de datos */
                result = (int)cmd.ExecuteScalar();
            }


            return result;
        }

        public object Get(int v)
        {
            throw new NotImplementedException();
        }

        public List<Artist> GetAll(string filterByName = "")
        {
            var result = new List<Artist>();
            var sql = "SELECT * FROM Artist WHERE LIKE @paramFilterName";

            using (IDbConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open();

                filterByName = $"%{filterByName}%";

                cmd.Parameters.Add(
                    new SqlParameter(@"paramFilterName", filterByName));

                var reader = cmd.ExecuteReader();
                var indice = 0;

                while (reader.Read())
                {
                    var artist = new Artist();
                    indice = reader.GetOrdinal("ArtistId");
                    artist.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    artist.Name = reader.GetString(indice);

                    result.Add(artist);
                }

            }
            return result;

        }

        public Artist get(int id)
        {
            var result = new Artist();
            var sql = "SELECT * FROM Artist WHERE ArtisId = @paramID";

            using (IDbConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open();
                //configurandi los parametros
                cmd.Parameters.Add(
                new SqlParameter("@paramID", id)
                );

                var reader = cmd.ExecuteReader();
                var indice = 0;

                while (reader.Read())
                {
                    var artist = new Artist();
                    indice = reader.GetOrdinal("ArtistId");
                    result.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    result.Name = reader.GetString(indice);

                }

                return result;
            }


        }


        public List<Artist> GetAllsp(string filterByName = "")
        {
            var result = new List<Artist>();
            var sql = "";

            using (IDbConnection cn = new SqlConnection(this.ConnectionString))
            {
                IDbCommand cmd = new SqlCommand(sql);

                /*ahora es un procedimiento almacenado */
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                filterByName = $"%{filterByName}%";

                cmd.Parameters.Add(
                    new SqlParameter(@"@FilterByName", filterByName));

                var reader = cmd.ExecuteReader();
                var indice = 0;

                while (reader.Read())
                {
                    var artist = new Artist();
                    indice = reader.GetOrdinal("ArtistId");
                    artist.ArtistId = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("Name");
                    artist.Name = reader.GetString(indice);

                    result.Add(artist);
                }

            }
            return result;
        }

        ///INSERTANDO A LA BASE DE DATOS//
        public int insert(Artist entity)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection
                (this.ConnectionString))
            {
                cn.Open();
                ///Iniciando el bloque de transaccion local
                var transaccion = cn.BeginTransaction();
                try
                {
                    IDbCommand cmd = new SqlCommand("usp_InsertArtist");
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(
                        new SqlParameter("@pName", entity.Name));
                    //ASOSCIANDO LA TRANSACCION LOCAL DEL OBJETO COMMAND
                    cmd.Transaction = transaccion;

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    //generando una exepcion 
                    //throw new exception ("Error");

                    //confirmando la transaccion 
                            transaccion.Commit();
                }

                catch (Exception ex)
                {
                    //cancelando la transaccion con el metodo RollBack
                    transaccion.Rollback();
                }
            }
            return result;
        }



        /// <summary>
        /// ACTUALIZANDO LOS DATOS EN LA BASE DE DATOS//
        /// </summary>
        /// <param</param>
        /// <returns></returns>
        public int update(Artist entity)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection
                (this.ConnectionString))
            {
                cn.Open();
            var transaccion = cn.BeginTransaction();
                try
                {
                    IDbCommand cmd = new SqlCommand("ups_UpdateArtist");
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(
                        new SqlParameter("@pName", entity.Name));
                    cmd.Parameters.Add(
                        new SqlParameter("@pId", entity.ArtistId));

                    //asociando la transaccon local con el objeto command
                    cmd.Transaction = transaccion;

                    result = cmd.ExecuteNonQuery();

                    //confirmando la transaccion
                    transaccion.Commit();
                }

                catch(Exception ex)
                {
                    //anulando o cancelando la transaccion con el metodo Rollback
                    transaccion.Rollback();
                } 
            }

            return result;
        }
        /// <summary>
        /// ELIMINANDO LOS DATOS DATOS DE LA BASE DE DATOS
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int delete(Artist entity)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection
                (this.ConnectionString))
            {
                cn.Open();

                var transaccion = cn.BeginTransaction();
                try
                {
                    IDbCommand cmd = new SqlCommand("ups_DeleteArtist");
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(
                        new SqlParameter("@pId", entity.ArtistId));

                    //asociando la transaccon local con el objeto command
                    cmd.Transaction = transaccion;

                    result = cmd.ExecuteNonQuery();

                    transaccion.Commit();
                }
                
                catch (Exception ex)
                {
                    transaccion.Rollback();
                }
            }

            return result;
        }

    }
}