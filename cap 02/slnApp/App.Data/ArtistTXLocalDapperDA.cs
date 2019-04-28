using App.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace App.Data
{
    public class ArtistTXLocalDapperDA : BaseConnection
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
               result = cn.ExecuteScalar<int>(sql);
            }
            return result;
        }


        public List<Artist> GetAll(string filterByName = "")
        {
            var result = new List<Artist>();
            var sql = "SELECT * FROM Artist WHERE Name LIKE @paramFilterByName";

            using (IDbConnection cn = new SqlConnection(this.ConnectionString))
            {
                result = cn.Query<Artist>(sql,
                    new {paramFilterByName = filterByName}
                    ).ToList();
            }
            return result;

        }



        public Artist get(int id)
        {
            var result = new Artist();

            var sql = "SELECT * FROM Artist WHERE ArtisId = @paramID";

            using (IDbConnection cn = new SqlConnection(this.ConnectionString))
            {

             result =  cn.QueryFirstOrDefault<Artist>(sql,
                    new { paramID = id}
                    );                
            }

            return result;

        }



        public List<Artist> GetAllsp(string filterByName = "")
        {
            var result = new List<Artist>();
            var sql = "usp_GetAll";

            using (IDbConnection cn = new SqlConnection(this.ConnectionString))
            {
                cn.Query<Artist>(sql,
                    new { filterByName = filterByName}, commandType:
                    CommandType.StoredProcedure).ToList();
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
              result =  cn.ExecuteScalar<int>("usp_InsertArtist", new { pName = entity.Name }, 
                    commandType: CommandType.StoredProcedure);
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
               result = cn.Execute("ups_UpdateArtist", new { pName = entity.Name, pId = entity.ArtistId },
                    commandType: CommandType.StoredProcedure);
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
                result = cn.Execute("ups_DeleteArtist", new {  pId = entity.ArtistId},
                   commandType: CommandType.StoredProcedure);
            }

            return result;
        }

    }
}
