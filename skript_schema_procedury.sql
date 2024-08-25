/* DELETE vsech tabulek
DROP TABLE BannedWords
DROP TABLE SongInPl
DROP TABLE Collab
DROP TABLE UserGenre
DROP TABLE SongsGenre
DROP TABLE Genre
DROP TABLE Payment
DROP TABLE Playlist
DROP TABLE Comments
DROP TABLE Song
DROP TABLE Album
DROP TABLE [User]*/

CREATE TABLE [User] (
  userID INT IDENTITY NOT NULL PRIMARY KEY,
  first_name VARCHAR(100) NOT NULL,
  last_name VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL,
  address VARCHAR(45) NOT NULL,
  role VARCHAR(15) NOT NULL,
  password VARCHAR(100) NOT NULL,
  telephone VARCHAR(12) NULL,
  artistN VARCHAR(100) NULL,
  description TEXT NULL,
  label VARCHAR(100) NULL,
  status VARCHAR(15) NULL DEFAULT 'Active',
  CONSTRAINT ch_role_stat CHECK (role LIKE 'Administrator' OR role LIKE 'User' OR role LIKE 'Interpret'),
  CONSTRAINT ch_status_stat CHECK (status LIKE 'Active' OR status LIKE 'Blocked' OR status LIKE 'Inactive'),
  CONSTRAINT ch_role_tel CHECK (CASE WHEN (role='Administrator' OR role = 'Interpret' ) AND telephone IS NULL THEN 0 ELSE 1 END = 1),
  CONSTRAINT ch_email_name CHECK (email LIKE '%@%.%')
);

CREATE TABLE Album (
  albumID INT IDENTITY NOT NULL PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  dateA DATE NOT NULL,
  userID INT NOT NULL,
  CONSTRAINT fk_album_user
	FOREIGN KEY (userID) REFERENCES [User] (userID)
);

CREATE TABLE Song (
  songID INT IDENTITY NOT NULL PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  length TIME NOT NULL,
  orderID INT NOT NULL,
  listened INT NULL,
  live INT NULL,
  albumID INT NOT NULL,
  CONSTRAINT fk_song_album
	FOREIGN KEY (albumID) REFERENCES Album (albumID)
);

CREATE TABLE Comments (
  comID INT IDENTITY NOT NULL PRIMARY KEY,
  text VARCHAR(max) NULL,
  stars INT NOT NULL DEFAULT 1,
  date DATE NOT NULL,
  songID INT NULL,
  userID INT NOT NULL,
  albumID INT NULL,
  CONSTRAINT fk_Comments_Song
    FOREIGN KEY (songID) REFERENCES Song (songID),
  CONSTRAINT fk_Comments_User
    FOREIGN KEY (userID) REFERENCES [User] (userID),
  CONSTRAINT fk_Comments_Album
    FOREIGN KEY (albumID) REFERENCES Album (albumID),
  CONSTRAINT ch_comments_stars CHECK (stars BETWEEN 1 AND 5)
);

CREATE TABLE Playlist (
  playlistID INT IDENTITY NOT NULL PRIMARY KEY,
  userID INT NOT NULL,
  length TIME NOT NULL,
  name VARCHAR(100) NULL,
  CONSTRAINT fk_Playlist_User
    FOREIGN KEY (userID) REFERENCES [User] (userID)
);

CREATE TABLE Payment (
  paymentID INT IDENTITY NOT NULL PRIMARY KEY,
  cost FLOAT NOT NULL,
  datePay DATE NOT NULL,
  dateToPay DATE NOT NULL,
  userID INT NOT NULL,
  CONSTRAINT fk_Payment_User
    FOREIGN KEY (userID) REFERENCES [User] (userID),
  CONSTRAINT ch_payment_date CHECK (datePay < dateToPay)
);

CREATE TABLE Genre (
  genreID INT IDENTITY NOT NULL PRIMARY KEY,
  name VARCHAR(50) NOT NULL
);

CREATE TABLE BannedWords (
  bwID INT IDENTITY NOT NULL PRIMARY KEY,
  name VARCHAR(200) NOT NULL
);

CREATE TABLE SongsGenre (
  songID INT NOT NULL,
  genreID INT NOT NULL,
  PRIMARY KEY (songID, genreID),
  CONSTRAINT fk_Song__Genre
    FOREIGN KEY (songID) REFERENCES Song (songID),
  CONSTRAINT fk_Gener_Song
    FOREIGN KEY (genreID) REFERENCES Genre (genreID)
);

CREATE TABLE UserGenre (
  genreID INT NOT NULL,
  userID INT NOT NULL,
  PRIMARY KEY (genreID, userID),
  CONSTRAINT fk_User__Genre
    FOREIGN KEY (userID) REFERENCES [User] (userID),
  CONSTRAINT fk_Gener_User
    FOREIGN KEY (genreID) REFERENCES Genre (genreID)
);

CREATE TABLE Collab (
  songID INT NOT NULL,
  userID INT NOT NULL,
  PRIMARY KEY (songID, userID),
  CONSTRAINT fk_User_Song
    FOREIGN KEY (songID) REFERENCES Song (songID),
  CONSTRAINT fk_Song_User
    FOREIGN KEY (userID) REFERENCES [User] (userID),
);

CREATE TABLE SongInPL (
  songID INT NOT NULL,
  playlistID INT NOT NULL,
  PRIMARY KEY (songID, playlistID),
  CONSTRAINT fk_Playlist_Song
    FOREIGN KEY (songID) REFERENCES Song (songID),
  CONSTRAINT fk_Song_Playlist
    FOREIGN KEY (playlistID) REFERENCES Playlist (playlistID)
);

GO
CREATE OR ALTER PROCEDURE [dbo].[TAutoZmenaStavuUzivatele]
AS
DECLARE
	@active INT,
	@uID INT
	BEGIN TRAN
		BEGIN TRY
			DECLARE c_user CURSOR LOCAL FOR SELECT userID FROM [User] WHERE role = 'User' AND status != 'Blocked';
			open c_user
			FETCH NEXT FROM c_user INTO @uID
			WHILE @@FETCH_STATUS = 0
			BEGIN
				SELECT @active = count(*) 
				FROM Payment
				WHERE @uID = userID AND GETDATE() <= dateToPay AND GETDATE() >= datePay

				if @active != 0 
					UPDATE [user] SET status = 'Active' WHERE userID = @uID;
				ELSE
					UPDATE [user] SET status = 'Inactive' WHERE userID = @uID; 


				FETCH NEXT FROM c_user INTO @uID

			end
			close c_user
			deallocate c_user

			COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
		PRINT 'Error occured while updating status of users';
		ROLLBACK TRANSACTION
	END CATCH
GO


GO
CREATE OR ALTER TRIGGER [dbo].[TCenzuraKomentaru]
ON Comments FOR INSERT, UPDATE
AS
DECLARE 
	@text VARCHAR(max),
	@name VARCHAR(200),
	@cID INT,
	@stars INT,
	@userID INT,
	@albumID INT,
	@songID INT
BEGIN TRAN
	BEGIN TRY
		SELECT @text = I.[text], @cID = I.comID, @stars = I.stars, @userID = I.userID, @albumID = I.albumID, @songID = I.songID
		FROM inserted I

		DECLARE c_word CURSOR LOCAL FOR SELECT [name] FROM BannedWords;
		open c_word
		FETCH NEXT FROM c_word INTO @name
		WHILE @@FETCH_STATUS = 0
		BEGIN
			SELECT @text = REPLACE(@text, @name, '****')

			FETCH NEXT FROM c_word INTO @name
		end
		close c_word
		deallocate c_word


		UPDATE Comments
		SET [text] = @text, date = GETDATE()
		WHERE comID = @cID

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT('Error ocured while inserting comment')
		ROLLBACK TRANSACTION
	END CATCH
GO

GO
CREATE OR ALTER PROCEDURE [dbo].[PSeznamSkladeb]
	@name VARCHAR(max)
AS
BEGIN
	if @name = ''
		SELECT s.songID, s.name, s.length, s.live, s.listened, (SELECT artistN FROM [User] WHERE userID = 
		(SELECT userID FROM album WHERE albumID = s.albumID)) as art ,(SELECT name FROM 
		album WHERE albumID = s.albumID) as alb ,AVG(c.STARS) as stars
		FROM Song s
		LEFT JOIN Comments c on c.songID = s.songID
		GROUP BY s.songID, s.name, s.length, s.live, s.listened, s.albumID
	else
		SELECT s.songID, s.name, s.length, s.live, s.listened, (SELECT artistN FROM [User] WHERE userID = 
		(SELECT userID FROM album WHERE albumID = s.albumID)) as art ,(SELECT name FROM 
		album WHERE albumID = s.albumID) as alb ,AVG(c.STARS) as stars
		FROM Song s
		LEFT JOIN Comments c on c.songID = s.songID
		WHERE s.name LIKE '%' + @name + '%' OR (SELECT artistN FROM [User] WHERE userID = 
		(SELECT userID FROM album WHERE albumID = s.albumID)) LIKE '%' + @name + '%' OR (SELECT name FROM 
		album WHERE albumID = s.albumID) LIKE '%' + @name + '%'
		GROUP BY s.songID, s.name, s.length, s.live, s.listened, s.albumID
END
GO

GO
CREATE OR ALTER PROCEDURE [dbo].[PSeznamAlb]
	@name VARCHAR(max)
AS
BEGIN
	if @name = ''
		SELECT a.albumID, a.name, a.dateA, (SELECT artistN FROM [User] WHERE userID = 
		a.userID) as art ,AVG(c.STARS) as stars
		FROM Album a
		LEFT JOIN Comments c on c.albumID = a.albumID
		GROUP BY a.albumID, a.name, a.dateA, a.userID
	else
		SELECT a.albumID, a.name, a.dateA, (SELECT artistN FROM [User] WHERE userID = 
		a.userID) as art ,AVG(c.STARS) as stars
		FROM Album a
		LEFT JOIN Comments c on c.albumID = a.albumID
		WHERE a.name LIKE '%' + @name + '%' OR (SELECT artistN FROM [User] WHERE userID = 
		a.userID) LIKE '%' + @name + '%'
		GROUP BY a.albumID, a.name, a.dateA, a.userID
END
GO

GO
CREATE OR ALTER PROCEDURE [dbo].[PPridaniPoslechnutiNaSkladbe]
	@skladba INT,
	@playing INT
AS
BEGIN
	if @playing = 1
		UPDATE Song
		SET live = (SELECT live FROM Song WHERE songID = @skladba) + 1, listened = (SELECT listened FROM Song WHERE songID = @skladba) + 1
		WHERE songID = @skladba
	else 
		UPDATE Song
		SET live = (SELECT live FROM Song WHERE songID = @skladba) - 1
		WHERE songID = @skladba
END
GO


GO
CREATE OR ALTER PROCEDURE [dbo].[PNovaPlatba]
	@userID INT,
	@money FLOAT,
	@toDate INT,
	@creditData INT
AS
DECLARE
	@sub DATE
BEGIN
	BEGIN TRAN
		BEGIN TRY

			IF @creditData = 0
				SELECT 1 / 0 AS Error;
			ELSE
				SELECT TOP 1 @sub = p.dateToPay
				FROM [User] u
				JOIN Payment p on p.userID = u.userID
				WHERE GETDATE() >= p.datePay AND GETDATE() <= p.dateToPay AND u.userID = @userID
				ORDER BY p.dateToPay DESC

				if(GETDATE() < @sub)
					INSERT INTO Payment(cost, datePay, dateToPay, userID)
					VALUES(@money, @sub, DATEADD(MONTH, @toDate, @sub), @userID)
				else
					INSERT INTO Payment(cost, datePay, dateToPay, userID)
					VALUES(@money, GETDATE(), DATEADD(MONTH, @toDate, GETDATE()), @userID)
					
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
				PRINT('Wrong credit data inserted')
				ROLLBACK TRANSACTION
		END CATCH
END
GO

