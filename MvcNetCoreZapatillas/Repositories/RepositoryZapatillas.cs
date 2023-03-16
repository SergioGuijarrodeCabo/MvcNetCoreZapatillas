using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Data;
using MvcNetCoreZapatillas.Models;

#region SQL SERVER
//VUESTRO PROCEDIMIENTO DE PAGINACION DE IMAGENES DE ZAPATILLAS


//CREATE OR ALTER PROCEDURE SP_GET_ZAPATILLAS(@REGISTROSPAGINA INT, @POSICION INT, @IDZAPATILLA INT)
//AS
//	SELECT * FROM (SELECT CAST(ROW_NUMBER() OVER(ORDER BY IDIMAGEN) AS INT) AS POSICION, IDIMAGEN, IMAGEN, IDPRODUCTO FROM IMAGENESZAPASPRACTICA WHERE IDPRODUCTO =@IDZAPATILLA) AS QUERY
//	WHERE QUERY.POSICION >= @POSICION AND QUERY.POSICION < (@POSICION + @REGISTROSPAGINA) 
//GO

#endregion

namespace MvcNetCoreZapatillas.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }


        public List<Zapatilla> GetZapatillas()
        {
            var consulta = this.context.Zapatillas.ToList();
            return consulta;


        }
        public Zapatilla FindZapatilla(int idProducto)
        {
            var consulta = this.context.Zapatillas.FirstOrDefault(x => x.IdProducto == idProducto);
            return consulta;


        }

        public int TotalZapatillas(int idProducto)
        {
            return this.context.ImagenesZapatillas.Count(x => x.IdProducto ==idProducto);

        }

        public ImagenZapatilla ImagenZapatilla(int registrospagina, int posicion, int idProducto)
        {
            string sql = "SP_GET_ZAPATILLAS @REGISTROSPAGINA, @POSICION, @IDZAPATILLA";

            SqlParameter pamReg = new SqlParameter("@REGISTROSPAGINA", registrospagina);
            SqlParameter pamPos = new SqlParameter("@POSICION", posicion);
            SqlParameter pamId = new SqlParameter("@IDZAPATILLA", idProducto);
            var consulta = this.context.ImagenesZapatillas.FromSqlRaw(sql, pamReg, pamPos, pamId);

            ImagenZapatilla imagen = consulta.AsEnumerable().FirstOrDefault();

            return imagen;
        }

    }
}
