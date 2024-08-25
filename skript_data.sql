INSERT INTO [User] (first_name, last_name, email, address, role, password )
VALUES ('Jožko', 'Vajda', 'neco@neco.cz' ,'Nevim 12 Mesto1', 'User', 'jozvaj'), 
		('Random', 'Týpek', 'rt@rt.rt', 'Random 10 Mesto2', 'User', 'typecek')

INSERT INTO [User] (first_name, last_name, email, address, role, password, telephone)
VALUES ('Op', 'Spravce', 'op@sprave.op', 'Spravcova 15 Mesto3' , 'Administrator' ,'opsp', '123456789'), 
		('Druhy', 'Spravce', 'druhy@sp.cz', 'Spravcova 16 Mesto3' , 'Administrator' ,'pops', '987654321')

INSERT INTO [User] (first_name, last_name, email, address, role, password, telephone, artistN, description, label)
VALUES ('Emo', 'Kid', 'emo@kid.cz', 'Emo 12 Mesto1', 'Interpret', 'punknotdead', '777777777', 'Emo Gang', 'We like emo', 'AverageEmoEnjoyers'),
	   ('Metal', 'Guy', 'devil@hell.com', 'Hellraiser 66 Mesto6', 'Interpret', 'hell666', '666666666', 'Hell Fanatics', 'We play only the most brutal metal, like metallica', 'Hell-o'),
	   ('Average', 'Pop', 'pop@life.depression', 'Amazing 48 Mesto 5', 'Interpret', 'lifeisb', '456123789', 'Pop group name', 'Do u like average mind numbing beat and lyrics about depression? Then you are at the right place', 'Life is Beautiful'),
	   ('Rap', 'Boss', 'rap@rap.rap', 'Rap street 56 New York', 'Interpret', 'skrrraa', '568945781', 'Dawgs', 'Ey yo sup, whats up? ey ou sit.', 'Fast speech')


INSERT INTO Album (name, dateA, userID)
VALUES ('Death and Sorrow', GETDATE(), (SELECT userID 
										FROM [User]
										WHERE first_name LIKE 'Emo' AND last_name LIKE 'Kid')),
		('Sorrow and Death', GETDATE(), (SELECT userID 
										FROM [User]
										WHERE first_name LIKE 'Emo' AND last_name LIKE 'Kid')),
	    ('Hell Is Upon Us', GETDATE(), (SELECT userID 
										FROM [User]
										WHERE first_name LIKE 'Metal' AND last_name LIKE 'Guy')),
		('Angels From HELL', GETDATE(), (SELECT userID 
										FROM [User]
										WHERE first_name LIKE 'Metal' AND last_name LIKE 'Guy')),
		('Happy Ranibows', GETDATE(), (SELECT userID 
										FROM [User]
										WHERE first_name LIKE 'Average' AND last_name LIKE 'Pop')),
		('Sunshine album', GETDATE(), (SELECT userID 
										FROM [User]
										WHERE first_name LIKE 'Average' AND last_name LIKE 'Pop')),
		('These streets', GETDATE(), (SELECT userID 
										FROM [User]
										WHERE first_name LIKE 'Rap' AND last_name LIKE 'Boss'))


INSERT INTO Song(name, length, orderID, listened, live, albumID)
VALUES ('Maturita formalita', '00:02:20', 1, 20, 0, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'These streets')),
       ('Základka mi staèí', '00:01:50', 2, 6000000, 2,  (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'These streets')),
	   ('Cut off your limbs', '00:03:20', 1, 500, 50, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Death and Sorrow')),
	   ('Brains out', '00:03:10', 2, 8000, 12, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Death and Sorrow')),
	   ('Darkness within me', '00:04:40', 3, 15000, 80, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Death and Sorrow')),
	   ('Unicorns', '00:2:40', 1, 500000, 80, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Happy Ranibows')),
	   ('I <3 u', '00:3:40', 1, 80000000, 8000, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Sunshine album')),
	   ('Generic beat', '00:8:40', 2, 900009999, 15000, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Sunshine album')),
	   ('Your masters', '00:5:40', 1, 500000, 40, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Hell Is Upon Us')),
	   ('My  masters', '00:6:40', 2, 500001, 39, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Hell Is Upon Us')),
	   ('Masters of masters', '00:8:40', 3, 600050, 20, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Hell Is Upon Us')),
	   ('Devil wings', '00:5:10', 2, 250000, 5, (SELECT albumID
														   FROM Album
														   WHERE name LIKE 'Angels From HELL'))
														   


INSERT INTO Genre(name)
VALUES ('Metal'), ('Pop'), ('Rap'), ('Metalcore'), ('Rock'), ('Powermetal'),
       ('Death Metal'), ('Emo Rock'), ('Punk'), ('Dubstep'), ('Lofi Hip-Hop'), ('Hip-Hop'), ('Jazz'),
	   ('Classical'), ('Opera'), ('Drumstep'), ('Hard Rock'), ('Beatbox')


INSERT INTO SongsGenre(genreID, songID)
VALUES ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Metal'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Your masters'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'PowerMetal'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Your masters'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Metal'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Cut off your limbs'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Death Metal'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Cut off your limbs'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Rap'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Maturita formalita'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Rap'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Základka mi staèí'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Metal'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Brains out'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Death Metal'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Brains out'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Rock'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Darkness within me'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Emo Rock'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Darkness within me'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Pop'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Unicorns'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Lofi Hip-Hop'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'Generic beat'
		 )),
		 ((SELECT genreID
		 FROM Genre
		 WHERE name LIKE 'Pop'),(
		 SELECT songID
		 FROM Song
		 WHERE name LIKE 'I <3 u'
		 ))

INSERT INTO Payment(cost, datePay, dateToPay, userID)
VALUES (1000, GETDATE(), GETDATE() + 365, (SELECT userID
									 FROM [User]
									 WHERE first_name LIKE 'Jožko' AND last_name LIKE 'Vajda'))

INSERT INTO Comments(text, stars, date, songID, userID, albumID)
VALUES ('nejlepsi pisnicka', 5, GETDATE(), (SELECT songID
											FROM Song
											WHERE name LIKE 'Generic beat'), (SELECT userID
																			FROM [User]
																			WHERE first_name LIKE 'Jožko' AND last_name LIKE 'Vajda'), NULL),
       ('Uslo to', 3, GETDATE(), (SELECT songID
								FROM Song
								WHERE name LIKE 'Generic beat'), (SELECT userID
																 FROM [User]
																WHERE first_name LIKE 'Metal' AND last_name LIKE 'Guy'), NULL),
		('Odpad', 1, GETDATE(), (SELECT songID
								FROM Song
								WHERE name LIKE 'Generic beat'), (SELECT userID
																FROM [User]
																WHERE first_name LIKE 'Emo' AND last_name LIKE 'Kid'), NULL),
		('Meh', 2, GETDATE(), (SELECT songID
								FROM Song
								WHERE name LIKE 'Generic beat'), (SELECT userID
																FROM [User]
																WHERE first_name LIKE 'Random' AND last_name LIKE 'Týpek'), NULL),
		('Prvni :D', 5, GETDATE(), (SELECT songID
								FROM Song
								WHERE name LIKE 'Brains out'), (SELECT userID
																FROM [User]
																WHERE first_name LIKE 'Metal' AND last_name LIKE 'Guy'), NULL)

INSERT INTO Playlist(userID, length, name)
VALUES ((SELECT userID
		FROM [User]
		WHERE first_name LIKE 'Jožko' AND last_name LIKE 'Vajda'), '00:10:00', NULL), 
		((SELECT userID
		FROM [User]
		WHERE first_name LIKE 'Random' AND last_name LIKE 'Týpek'), '00:20:00', 'Muj metal')

INSERT INTO SongInPL(songID, playlistID)
VALUES ((SELECT songID
		FROM Song
		WHERE name LIKE 'Maturita formalita'),
		(SELECT playlistID
		FROM Playlist
		JOIN [User] u on u.userID = Playlist.userID
		WHERE first_name LIKE 'Jožko' AND last_name LIKE 'Vajda'
		)),
		((SELECT songID
		FROM Song
		WHERE name LIKE 'Darkness within me'),(
		SELECT playlistID
		FROM Playlist
		JOIN [User] u on u.userID = Playlist.userID
		WHERE first_name LIKE 'Jožko' AND last_name LIKE 'Vajda'
		)),
		((SELECT songID
		FROM Song
		WHERE name LIKE 'Darkness within me'),(
		SELECT playlistID
		FROM Playlist
		JOIN [User] u on u.userID = Playlist.userID
		WHERE first_name LIKE 'Random' AND last_name LIKE 'Týpek'
		)),
		((SELECT songID
		FROM Song
		WHERE name LIKE 'Your masters'),(
		SELECT playlistID
		FROM Playlist
		JOIN [User] u on u.userID = Playlist.userID
		WHERE first_name LIKE 'Random' AND last_name LIKE 'Týpek'
		)),
		((SELECT songID
		FROM Song
		WHERE name LIKE 'My  masters'),(
		SELECT playlistID
		FROM Playlist
		JOIN [User] u on u.userID = Playlist.userID
		WHERE first_name LIKE 'Random' AND last_name LIKE 'Týpek'
		))

INSERT INTO Collab(songID, userID)
VALUES ((SELECT songID
		FROM Song
		WHERE name LIKE 'Darkness within me'), (SELECT userID
		FROM [User]
		WHERE artistN LIKE 'Emo Gang')), ((SELECT songID
		FROM Song
		WHERE name LIKE 'Darkness within me'), (SELECT userID
		FROM [User]
		WHERE artistN LIKE 'Hell Fanatics'))

INSERT INTO BannedWords(name)
VALUES('Badword'),('RLYBADWORD'), ('TheWorstWordEver'), ('bword')