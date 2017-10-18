using MyStoreDesktop.Connection;
using MyStoreDesktop.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreDesktop.DAO
{
     public class ProductsDAO
    {
        public List<ProductsCategorySupplier> RetrieveAll()
        {
            List<ProductsCategorySupplier> productos = new List<ProductsCategorySupplier>();
            try
            {
                string sql = "SELECT P.ProductID, P.ProductName,P.QuantityPerUnit,P.UnitPrice,P.UnitsInStock,P.UnitsOnOrder,P.ReorderLevel,P.Discontinued ,C.CategoryName,S.CompanyName FROM Products as P";
                        sql += " INNER JOIN Categories as C  ON P.CategoryID = C.CategoryID INNER JOIN Suppliers as S ON P.SupplierID = S.SupplierID ";
                SqlConnection conexion = ConexionSqlServer.ObtenerConexion();
                SqlCommand cmd = new SqlCommand(sql, conexion);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ProductsCategorySupplier producto = new ProductsCategorySupplier()
                    {
                        ProductID = rd.GetInt32(0),
                        ProductName = rd.GetString(1),
                        QuantityPerUnit = rd.GetString(2),
                        UnitPrice = rd.GetDecimal(3),
                        UnitsInStock = rd.GetInt16(4),
                        UnitsOnOrder = rd.GetInt16(5),
                        ReorderLevel = rd.GetInt16(6),
                        Discontinued = rd.GetBoolean(7),
                        CategoryName = rd.GetString(8),
                        CompanyName = rd.GetString(9)
                    };
                    productos.Add(producto);
                }
                conexion.Close();
                return productos;
            }
            catch
            {
                throw;
            }
        }
    }
}
