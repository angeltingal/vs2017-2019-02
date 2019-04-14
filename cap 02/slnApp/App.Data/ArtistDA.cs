using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data
{
    public  class ArtistDA:BaseConnection
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
                cn =new SqlConnection(base.ConnectionString ))
            {
                /*2.- Creando el objeto command*/
                IDbCommand cmd = new SqlCommand(sql);
                cmd.Connection = cn;
                cn.Open(); //* Abriendo la canexion a la base de datos */
                result = (int)cmd.ExecuteScalar();
            }


                return result;
        }
       }
}
