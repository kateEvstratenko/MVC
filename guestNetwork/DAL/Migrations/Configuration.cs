using guestNetwork.Models;

namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<UnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UnitOfWork context)
        {
            context.Languages.AddOrUpdate(
                            x => x.Name,
                            new Language() { Name = "English" },
                            new Language() { Name = "Italian" },
                            new Language() { Name = "Français" },
                            new Language() { Name = "Español" },
                            new Language() { Name = "Italiano" },
                            new Language() { Name = "Deutsch" },
                            new Language() { Name = "Português" },
                            new Language() { Name = "Русский" },
                            new Language() { Name = "日本人" }
                            );
        }
    }
}
