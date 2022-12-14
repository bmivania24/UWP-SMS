USE [DB_SMS]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 10/10/2022 11:10:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[idMessage] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ToNumber] [varchar](50) NOT NULL,
	[FromNumber] [varchar](50) NOT NULL,
	[Message] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[idMessage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResponsesMessageTw]    Script Date: 10/10/2022 11:10:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResponsesMessageTw](
	[IdResponsesMessageTw] [int] IDENTITY(1,1) NOT NULL,
	[IdMessage] [int] NOT NULL,
	[DateSent] [datetime] NOT NULL,
	[ConfirmationCode] [varchar](500) NOT NULL,
 CONSTRAINT [PK_ResponsesMessageTw] PRIMARY KEY CLUSTERED 
(
	[IdResponsesMessageTw] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResponsesMessageTw]  WITH CHECK ADD  CONSTRAINT [FK_ResponsesMessageTw_Messages] FOREIGN KEY([IdMessage])
REFERENCES [dbo].[Messages] ([idMessage])
GO
ALTER TABLE [dbo].[ResponsesMessageTw] CHECK CONSTRAINT [FK_ResponsesMessageTw_Messages]
GO
/****** Object:  StoredProcedure [dbo].[sp_Messages]    Script Date: 10/10/2022 11:10:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Messages]
@option		int = null,
@ToNumber	varchar(50) = null,
@FromNumber varchar(50) = null,
@Message	varchar(250) = null
AS
BEGIN
	if @option = 1
	begin
		INSERT INTO [dbo].[Messages]
           ([DateCreated]
           ,[ToNumber]
           ,[FromNumber]
           ,[Message])
     VALUES
           (getdate()
           ,@ToNumber	
           ,@FromNumber 
           ,@Message	)

		   SELECT CAST(scope_identity() AS int)
	end

	if @option = 2
	BEGIN 
		SELECT * FROM Messages
	END

	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ResponseMessages]    Script Date: 10/10/2022 11:10:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ResponseMessages]
@option		int = null,
@IdMessage	int 	= null,
@DateSent	datetime= null,
@ConfirmationCode varchar(500)= null
AS
BEGIN
	if @option = 1
	begin
		INSERT INTO [dbo].[ResponsesMessageTw]
           ([IdMessage]
           ,[DateSent]
           ,[ConfirmationCode])
		VALUES
           (@IdMessage		 
           ,getdate()		 
           ,@ConfirmationCode)

		   SELECT CAST(scope_identity() AS int)
	end

	if @option = 2
	BEGIN 
		SELECT [IdResponsesMessageTw]
      ,a.[IdMessage]
      ,a.[DateSent]
      ,a.[ConfirmationCode]
	  ,b.ToNumber
	  ,b.FromNumber 
	  ,b.Message 
	  ,DateCreated 
		FROM [dbo].[ResponsesMessageTw] a
		INNER JOIN [dbo].[Messages] b ON a.IdMessage = b.idMessage 
		order by IdResponsesMessageTw desc
	END

	
END

-- exec sp_ResponseMessages 1,2,null,'sucessfull'
GO
