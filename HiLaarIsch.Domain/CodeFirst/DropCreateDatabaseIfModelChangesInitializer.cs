using System.Data.Entity;

namespace HiLaarIsch.Domain
{

    public class DropCreateDatabaseIfModelChangesInitializer
        : DropCreateDatabaseIfModelChanges<HiLaarischEntities>
    {
        public override void InitializeDatabase(HiLaarischEntities context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(HiLaarischEntities context)
        {
            var admin = new UserEntity
            {
                Email = "admin@manegearnhem.nl",
                EmailConfirmed = true,
                PasswordHash = "AGtWGj6m/ICb16LNfGINdZTceQH9xdE9l9QbFKhuv0DaoDI2Ja/AeVCCK8BZey3I9g==",
                Role = Components.Role.Admin,
            };

            context.Users.Add(admin);

            base.Seed(context);
        }
    }
}