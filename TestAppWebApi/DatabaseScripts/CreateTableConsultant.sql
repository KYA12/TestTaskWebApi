USE [ShopDataBase]
GO

/****** Object:  Table [dbo].[Consultant]    Script Date: 24.03.2020 17:17:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Consultant](
	[ConsultantID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[DateHiring] [datetime] NULL,
	[ShopID] [int] NULL,
 CONSTRAINT [PK_Consultant] PRIMARY KEY CLUSTERED 
(
	[ConsultantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Consultant] ADD  CONSTRAINT [DF_Consultant_DateHiring]  DEFAULT (getdate()) FOR [DateHiring]
GO

ALTER TABLE [dbo].[Consultant]  WITH CHECK ADD  CONSTRAINT [FK_Consultant_Shop] FOREIGN KEY([ShopID])
REFERENCES [dbo].[Shop] ([ShopID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Consultant] CHECK CONSTRAINT [FK_Consultant_Shop]
GO


