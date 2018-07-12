-- full query to create tables and stored procedures for the Forum App

create database Test
go

USE Test
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NULL,
	[NickName] [nvarchar](256) NOT NULL,
	[City] [nvarchar](256) NULL,
	[Country] [nvarchar](256) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Topics](
	[TopicID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[CreatedDate] [datetime] NULL
 CONSTRAINT [PK_dbo.Topics] PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Threads](
	[ThreadID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[TopicID] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[Inactive] [bit] NULL
 CONSTRAINT [PK_dbo.Threads] PRIMARY KEY CLUSTERED 
(
	[ThreadID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Posts](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ThreadID] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL
 CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [Test]
GO
/****** Object:  StoredProcedure [dbo].[GetPosts]    Script Date: 07/11/2018 00:22:59 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE PROCEDURE 
--	[dbo].[GetPosts]
--	@ThreadID INT
--	AS
--	SELECT Posts.PostID,
--	Posts.UserID,
--	Posts.ThreadID,
--	Posts.Text,
--	Posts.CreatedDate,
--	Users.NickName,
--	Users.City,
--	Users.Country,
--	Users.CreatedDate as UserCreatedOn,
--	B.NumPosts
--	FROM Posts
--	left join Users on Posts.UserID = Users.UserID
--	CROSS APPLY (
--		SELECT Count(*) as NumPosts
--		FROM Posts 
--	WHERE Users.UserID = Posts.UserID) AS B
--	WHERE ThreadID = @ThreadID


--GO
/****** Object:  StoredProcedure [dbo].[GetThreadDetails]    Script Date: 07/11/2018 00:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE PROCEDURE 
	[dbo].[GetThreadDetails]
	@ThreadID INT
	AS
	SELECT *
	FROM Threads
	WHERE ThreadID = @ThreadID
GO
/****** Object:  StoredProcedure [dbo].[GetThreads]    Script Date: 07/11/2018 00:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


 CREATE PROCEDURE 
	[dbo].[GetThreads]
	@TopicID int
	AS           
		SELECT *
FROM Threads
	OUTER APPLY (
			SELECT TOP(1) 
				Posts.PostID, 
				Posts.Text, 
				Posts.CreatedDate as PostCreatedOn, 
				Users.NickName
			FROM Posts 
			LEFT JOIN Users on Users.UserID = Posts.UseriD
		WHERE Posts.ThreadID = Threads.ThreadID ORDER BY PostCreatedOn DESC) AS C
		CROSS APPLY (
			SELECT Count(*) as NumPosts
			FROM Posts 
		WHERE Threads.ThreadID = Posts.ThreadID) as B
		where TopicID = @TopicID


GO
/****** Object:  StoredProcedure [dbo].[GetTopicDetails]    Script Date: 07/11/2018 00:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE PROCEDURE 
	[dbo].[GetTopicDetails]
	@TopicID INT
	AS
	SELECT *
	FROM Topics
	WHERE TopicID = @TopicID
GO
/****** Object:  StoredProcedure [dbo].[GetTopics]    Script Date: 07/11/2018 00:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE PROCEDURE 
	[dbo].[GetTopics]
	AS           
		SELECT	*
		FROM Topics
		OUTER APPLY (
			SELECT TOP(1) 
				Posts.PostID, 
				Posts.Text, 
				Posts.CreatedDate as PostCreatedOn, 
				Threads.Name as ThreadName,
				Users.NickName
			FROM Posts 
			LEFT JOIN Threads on Posts.ThreadID = Threads.ThreadID 
			LEFT JOIN Users on Users.UserID = Posts.UseriD
		WHERE Threads.TopicID = Topics.TopicID ORDER BY PostCreatedOn DESC) AS C
		CROSS APPLY (
			SELECT Count(*) as NumPosts
			FROM Posts 
			LEFT JOIN Threads on Posts.ThreadID = Threads.ThreadID 
		WHERE Threads.TopicID = Topics.TopicID) as B

GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 07/11/2018 00:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE 
	[dbo].[GetUser] 
		@UserID int = NULL,
		@Email nvarchar(256) = NULL,
		@Password nvarchar(256) = NULL
	AS           
	IF (@UserID IS NULL)
		BEGIN
			SELECT * 
			FROM Users 
			WHERE Users.Email = @Email AND Users.Password = @Password
		END
	ELSE
		BEGIN 
			SELECT * 
			FROM Users 
			WHERE Users.UserID = @UserID
		END
GO
/****** Object:  StoredProcedure [dbo].[InsertThread]    Script Date: 07/11/2018 00:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE 
	[dbo].[InsertThread]
        @UserID int,
		@TopicID int,
		@Name nvarchar(256),
		@Description nvarchar(256) = NULL
	AS           
	INSERT INTO Threads (UserID, TopicID, Name, Description, CreatedDate)
	OUTPUT INSERTED.*
	VALUES (@UserID, @TopicID, @Name, @Description, GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 07/11/2018 00:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE 
	[dbo].[InsertUser] 
        @NickName nvarchar(256),
		@Password nvarchar(256),
		@Email nvarchar(256) = NULL,
		@City nvarchar(256) = NULL,
		@Country nvarchar(256) = NULL
	AS           
	INSERT INTO Users (NickName, Password, Email, City, Country, CreatedDate)
	OUTPUT INSERTED.*
	VALUES (@NickName, @Password, @Email, @City, @Country, GETDATE())

GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE 
	[dbo].[InsertPost] 
        @ThreadID int,
		@UserID int,
		@Text nvarchar(max)
	AS           
	INSERT INTO Posts (ThreadID, UserID, Text, CreatedDate)
	OUTPUT INSERTED.*
	VALUES (@ThreadID, @UserID, @Text, GETDATE())

GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE 
	[dbo].[InsertTopic] 
		@UserID int,
		@Name nvarchar(256)
	AS           
	INSERT INTO Topics (UserID, Name, CreatedDate)
	OUTPUT INSERTED.*
	VALUES (@UserID, @Name, GETDATE())
GO

/****** Object:  Table [dbo].[Posts]    Script Date: 07/11/2018 00:22:59 ******/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE 
	[dbo].[GetPosts]
	@ThreadID INT,
	@SortOrder nvarchar(10) = NULL  
	AS
	SELECT Posts.PostID,
	Posts.UserID,
	Posts.ThreadID,
	Posts.Text,
	Posts.CreatedDate,
	Users.NickName,
	Users.City,
	Users.Country,
	Users.CreatedDate as UserCreatedOn,
	B.NumPosts
	FROM Posts
	left join Users on Posts.UserID = Users.UserID
	CROSS APPLY (
		SELECT Count(*) as NumPosts
		FROM Posts 
	WHERE Users.UserID = Posts.UserID) AS B
	WHERE ThreadID = @ThreadID
	ORDER by CASE WHEN @SortOrder = 'ASC' THEN Posts.CreatedDate END,
         CASE WHEN @SortOrder = 'DESC' OR @SortOrder is NULL THEN Posts.CreatedDate END DESC

GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE 
	[dbo].[CloseThread]
		@ThreadID int
	AS           
	Update Threads
	set Inactive = 'True'
	OUTPUT INSERTED.*
	where ThreadID = @ThreadID
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE 
	[dbo].[StartThread]
		@ThreadID int
	AS           
	Update Threads
	set Inactive = 'False'
	OUTPUT INSERTED.*
	where ThreadID = @ThreadID
GO