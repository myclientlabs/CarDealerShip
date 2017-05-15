USE Car
GO

INSERT INTO Style ([Description])
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

INSERT INTO Salesperson (FirstName, LastName)
VALUES ('Sally', 'Sergeant'), ('Jackson', 'Reno')

INSERT INTO [State] ([Name], Code)
VALUES ('Ohio', 'OH'), ('Kentucky', 'KY'), ('Indiana', 'IN'), ('Tennessee', 'TN')

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
VALUES (1, 'What are your hours?', NULL)