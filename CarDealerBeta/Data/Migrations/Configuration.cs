namespace Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.CarEntities>
    {

        private const string ADMIN_ROLE = "Admin";
        private const string SALES_ROLE = "Sales";
        private const string DISABLED_ROLE = "Disabled";

        string sqlSeedScript = @"INSERT INTO Style ([Description])
                        VALUES ('car'), ('suv'), ('truck'), ('van')

                        INSERT INTO Transmission ([Description])
                        VALUES ('automatic'), ('manual')

                        INSERT INTO Color ([Name])
                        VALUES ('black'), ('white'), ('gray'), ('red'), ('silver'), ('blue')

                        INSERT INTO Make ([Name])
                        VALUES ('Honda'), ('Toyota'), ('Mazda'), ('Lexus')
                        INSERT INTO Model (MakeId, [Name])
                        VALUES
	                        (1, 'Civic'), (1, 'Accord'),
	                        (2, 'Corolla'), (2, 'Camry'),
	                        (3, 'MX-5 Miata'), (3, 'CX-3'),
	                        (4, 'RX'), (4, 'IS') 
                        INSERT INTO Vehicle
	                        (Vin, ModelId, [Year], New, 
	                        StyleId, TransmissionId, InteriorId, ExteriorId,
	                        Mileage, MSRP, [Description], ImageFile)
                        VALUES
	                        ('1234567890ABCDEFG', 2, '2017', 1, 1, 1, 2, 3, 50, 22000, 'New Honda Acc', 'placeholder.img'),
	                        ('2345678901BCDEFGH', 3, '2007', 0, 1, 1, 2, 3, 100000, 18000, 'Old Toyota Corolla', 'placeholder.img'),
	                        ('3456789012CDEFGHI', 5, '2015', 0, 1, 1, 2, 3, 30000, 28000, 'Used Mazda Miata', 'placeholder.img'),
	                        ('4567890123DEFGHIJ', 8, '2012', 0, 1, 1, 2, 3, 55000, 34000, 'Usd Lexus IS', 'placeholder.img')
                        INSERT INTO Stock (VehicleId, FloorPrice, Available, Feature)
                        VALUES
	                        (1, 22000, 1, 1),
	                        (2, 15500, 1, 0),
	                        (3, 19000, 0, 0),
	                        (4, 16000, 1, 0)
                        INSERT INTO Special ([Name], [Description])
                        VALUES
	                        ('Free sticker', 'Get a free bumper sticker with the purchase of a new car.'),
	                        ('Walk-in special', '$300 off if you mention this special!')

                        INSERT INTO Customer
	                        (FirstName, LastName, Phone, Email,
	                        Street1, Street2, City, StateId, ZipCode)
                        VALUES ('Leeroy', 'Jenkins', '5555678', 'anything@fake.com', '123 Main St.', '', 'Louisville', 2, '40203')
	                    

                        INSERT INTO PurchaseType ([Description])
                        VALUES ('Bank finance'), ('Cash'), ('Dealer finance')
                        INSERT INTO Sales (CustomerId, SalesPersonId, VehicleId, [Date], PurchasePrice, PurchaseTypeId)
                        VALUES (1, 1, 3, '1/23/2017', 18500, 1)
                        INSERT INTO Contact (FirstName, LastName, Phone, Email)
                        VALUES ('Nat', 'Pagle', '', 'not@fake.com')
                        INSERT INTO Inquiry (ContactId, [Message], VehicleId)
                        VALUES (1, 'What are your hours?', NULL)";
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.CarEntities context)
        {
            CreateUser(context, "admin@example.com", ADMIN_ROLE, "Admin", "User");
            CreateUser(context, "sales@example.com", SALES_ROLE, "Sales", "User");
            CreateUser(context, "mike@example.com", ADMIN_ROLE, "Mike", "Chow");


            context.States.AddOrUpdate(s => s.Name, (new State { Code = "AL", Name = "Alabama" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "AK", Name = "Alaska" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "AZ", Name = "Arizona" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "AR", Name = "Arkansas" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "CA", Name = "California" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "CO", Name = "Colorado" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "CT", Name = "Connecticut" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "DE", Name = "Delaware" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "DC", Name = "District Of Columbia" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "FL", Name = "Florida" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "GA", Name = "Georgia" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "HI", Name = "Hawaii" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "ID", Name = "Idaho" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "IL", Name = "Illinois" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "IN", Name = "Indiana" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "IA", Name = "Iowa" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "KS", Name = "Kansas" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "KY", Name = "Kentucky" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "LA", Name = "Louisiana" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "ME", Name = "Maine" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "MD", Name = "Maryland" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "MA", Name = "Massachusetts" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "MI", Name = "Michigan" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "MN", Name = "Minnesota" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "MS", Name = "Mississippi" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "MO", Name = "Missouri" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "MT", Name = "Montana" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "NE", Name = "Nebraska" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "NV", Name = "Nevada" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "NH", Name = "New Hampshire" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "NJ", Name = "New Jersey" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "NM", Name = "New Mexico" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "NY", Name = "New York" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "NC", Name = "North Carolina" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "ND", Name = "North Dakota" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "OH", Name = "Ohio" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "OK", Name = "Oklahoma" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "OR", Name = "Oregon" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "PA", Name = "Pennsylvania" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "RI", Name = "Rhode Island" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "SC", Name = "South Carolina" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "SD", Name = "South Dakota" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "TN", Name = "Tennessee" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "TX", Name = "Texas" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "UT", Name = "Utah" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "VT", Name = "Vermont" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "VA", Name = "Virginia" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "WA", Name = "Washington" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "WV", Name = "West Virginia" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "WI", Name = "Wisconsin" }));
            context.States.AddOrUpdate(s => s.Name, (new State { Code = "WY", Name = "Wyoming" }));
            context.SaveChanges();
            if (context.Vehicles.Count() == 0)
            {
                context.Database.ExecuteSqlCommand(sqlSeedScript);
            }
            context.SaveChanges();

        }

        private static void CreateUser(CarEntities context, string email, string role, string firstName, string lastName)
        {
            if (!context.Users.Any(s => s.Email == email))
            {
                var user = new Models.User
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Password = "Pass@word1",
                    Role = role
                };
                Salesperson sales = new Salesperson();
                sales.User = user;

                context.Users.Add(user);
                context.Salespersons.Add(sales);
                context.SaveChanges();
            }
        }
    }
}
