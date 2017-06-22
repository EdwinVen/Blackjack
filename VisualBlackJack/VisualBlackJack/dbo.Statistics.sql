

/****** Object:  Table [dbo].[Statistics]    Script Date: 12/2/2016 11:49:12 PM ******/


CREATE TABLE [dbo].[Statistics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Winner] [nchar](50) NULL,
	[UserScore] [int] NULL,
	[UserCardsDrawn] [int] NULL,
	[ComputerScore] [int] NULL,
	[ComputerCardsDrawn] [int] NULL,
 CONSTRAINT [PK_Statistics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

