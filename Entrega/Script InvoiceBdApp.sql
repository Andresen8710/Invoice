USE [InvoiceBDApp]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 3/26/2022 4:30:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Identification] [varchar](50) NOT NULL,
	[Birthdate] [date] NOT NULL,
	[Email] [varchar](50) NULL,
	[Age]  AS ((CONVERT([int],CONVERT([char](10),getdate(),(112)))-CONVERT([char](10),CONVERT([date],[Birthdate]),(112)))/(10000)),
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetails]    Script Date: 3/26/2022 4:30:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetails](
	[InvoiceId] [int] NOT NULL,
	[RowNumber] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[TaxValue] [money] NOT NULL,
	[SubTotal]  AS ([Price]*[Quantity]),
	[Total]  AS ([Price]*[Quantity]+[TaxValue]),
 CONSTRAINT [PK_InvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC,
	[RowNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 3/26/2022 4:30:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[TaxValue] [money] NULL,
	[SubTotal] [money] NULL,
	[Total]  AS ([TaxValue]+[SubTotal]),
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 3/26/2022 4:30:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Description] [varchar](200) NULL,
	[Price] [money] NOT NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (1, N'Peggy', N'Bertrand', N'21203', CAST(N'1987-12-26' AS Date), N'peggy@gmail.com')
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (2, N'Luna', N'Salazar', N'21302', CAST(N'2000-05-01' AS Date), N'luna@hotmail.com')
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (3, N'Martha', N'Salazar', N'1140', CAST(N'1975-02-05' AS Date), N'mona@gmail.com')
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (4, N'Estefania', N'Franco', N'2150', CAST(N'1991-09-17' AS Date), NULL)
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (5, N'Louis', N'Bertrand', N'0160', CAST(N'1940-07-16' AS Date), NULL)
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (6, N'Felipe', N'Bertrand', N'2050', CAST(N'1988-10-13' AS Date), N'Felipe@gmail.com')
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (7, N'Francois', N'Bertrand', N'9091', CAST(N'2008-05-22' AS Date), N'Francois@gmail.com')
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (8, N'Carlos', N'Hurtado', N'0250', CAST(N'1978-12-21' AS Date), NULL)
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (9, N'Alejandra', N'Buitrago', N'3020', CAST(N'1999-12-22' AS Date), N'Alebu@gmail.com')
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (10, N'David', N'Hurtado', N'3130', CAST(N'1995-09-26' AS Date), N'david@gmail.com')
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (11, N'Katty', N'Salazar', N'21214', CAST(N'2007-12-26' AS Date), N'Katty@gmail.com')
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [Identification], [Birthdate], [Email]) VALUES (33, N'Hachy', N'Salazar', N'15235413', CAST(N'2016-02-02' AS Date), N'hachy@gmail.com')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (1, 1, 1, 130.0000, 1, 20.8000)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (2, 1, 1, 130.0000, 2, 20.8000)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (3, 1, 2, 62.0000, 1, 9.9200)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (4, 1, 3, 25.0000, 1, 4.0000)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (5, 1, 2, 62.0000, 1, 9.9200)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (6, 1, 2, 62.0000, 1, 9.9200)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (7, 1, 2, 62.0000, 1, 9.9200)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (8, 1, 6, 110.0000, 1, 17.6000)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (9, 1, 6, 110.0000, 1, 17.6000)
INSERT [dbo].[InvoiceDetails] ([InvoiceId], [RowNumber], [ProductId], [Price], [Quantity], [TaxValue]) VALUES (10, 1, 6, 110.0000, 1, 17.6000)
GO
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (1, CAST(N'2022-03-26T02:40:41.100' AS DateTime), 1, 1, 20.8000, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (2, CAST(N'2022-03-26T02:40:41.100' AS DateTime), 1, 2, 20.8000, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (3, CAST(N'2022-03-26T20:12:11.457' AS DateTime), 2, 1, 9.9200, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (4, CAST(N'2000-03-26T20:12:11.457' AS DateTime), 5, 1, 4.0000, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (5, CAST(N'2000-03-26T20:12:11.457' AS DateTime), 4, 1, 9.9200, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (6, CAST(N'2000-04-26T20:12:11.457' AS DateTime), 4, 1, 9.9200, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (7, CAST(N'2000-02-21T20:12:11.457' AS DateTime), 4, 1, 9.9200, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (8, CAST(N'2000-02-23T20:12:11.457' AS DateTime), 6, 1, 17.6000, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (9, CAST(N'2000-04-25T20:12:11.457' AS DateTime), 6, 1, 17.6000, 0.0000)
INSERT [dbo].[Invoices] ([Id], [Date], [CustomerId], [Quantity], [TaxValue], [SubTotal]) VALUES (10, CAST(N'2000-04-25T20:12:11.457' AS DateTime), 9, 1, 17.6000, 0.0000)
SET IDENTITY_INSERT [dbo].[Invoices] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [Stock]) VALUES (1, N'Chunky Adulto X25', N'ALimento Chunky 25 Kg', 130.0000, 97)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [Stock]) VALUES (2, N'Nesgar Spectra 30-60kg', N'Antiparasitario', 62.0000, 6)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [Stock]) VALUES (3, N'Arnes Antijalones M', N'Arnes Perro Mediano', 25.0000, 199)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [Stock]) VALUES (4, N'Arnes Antijalones G', N'Arnes Perro Grande', 35.0000, 100)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [Stock]) VALUES (5, N'Nesgar Spectra 15-30', N'Antiparasitario', 45.0000, 5)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [Stock]) VALUES (6, N'Chunky Cachorro X25kg', N'Alimento Chunky 25 kg', 110.0000, 5)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[InvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetails_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceDetails] CHECK CONSTRAINT [FK_InvoiceDetails_Invoices]
GO
ALTER TABLE [dbo].[InvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceDetails] CHECK CONSTRAINT [FK_InvoiceDetails_Products]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Customers]
GO
