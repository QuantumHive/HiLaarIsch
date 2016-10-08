namespace HiLaarIsch.Domain
{
    public static class EntityFrameworkSqlProviderServicesFix
    {
        //http://stackoverflow.com/a/19130718/2040663
        /// <summary>
        /// Call this class from an external assembly, so that the compiler will output the EntityFramework.SqlServer assembly in the bin directory.
        /// This assembly is required in order for Entity Framework to work with the Sql Provider
        /// </summary>
        public static void IncludeSqlProviderServicesInstance()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
