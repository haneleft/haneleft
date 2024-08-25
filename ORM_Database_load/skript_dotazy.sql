use Zkusebni

/* 1;1;12;Skladby serazene podle alb a poradi v nich*/
SELECT s.name as Song, a.name as Album, s.orderID as poradi, s.listened, s.live
FROM Song s
JOIN Album a on a.albumID = s.albumID
ORDER BY a.name, poradi

/* 1;2;6;Alba sezarezena od nejposlouchanejsich po nejmene poslouchane*/
SELECT a.name, a.dateA
FROM Song s
JOIN Album a on a.albumID = s.albumID
GROUP BY a.name, a.dateA
ORDER BY SUM(s.listened) DESC

/* 1;3;7;Pocet skladeb v albech*/
SELECT a.name as Album,  count(distinct s.songID) as Skladby
FROM Album a
LEFT JOIN Song s on s.albumID = a.albumID
GROUP BY a.name

/* 1;4;4;Nejvic poslechnuti u jedne skladby pro kazdeho autora*/
SELECT distinct u.first_name, u.last_name, u.artistN as Artis, MAX(s.listened) as Most_Listened
FROM [User] u
JOIN Album a on a.userID = u.userID
JOIN Song s on s.albumID = a.albumID
GROUP BY u.first_name, u.last_name, u.artistN



/* 2;1;3;Vypise vsechny skladby, ktere jsou soucasti zanru Metal (i jeho podzanru)*/
SELECT s.name as Song, a.name as Album, s.listened
FROM Song s
JOIN Album a on a.albumID = s.albumID
JOIN SongsGenre sg on sg.songID = s.songID
JOIN Genre g on g.genreID = sg.genreID
WHERE g.name LIKE '%Metal%'
GROUP BY s.name, a.name, s.listened

/* 2;2;7;Vypise vsechny krome obycejnych uzivatelu, kteri si jeste nikdy nezkoupili sluzby v systemu*/
SELECT u.first_name, u.last_name, u.email, u.address, u.role
FROM [User] u
LEFT JOIN Payment p on p.userID = u.userID
WHERE (role = 'User' AND p.paymentID IS NOT NULL) OR ( role = 'Interpret' OR role = 'Administrator')


/* 2;3;3;Vypise pinsicky ktere maji delku vetsi nez 5 min a diva se na ne vice jak 30 lidi*/
SELECT name, length, orderID, listened, live
FROM Song
WHERE length > '00:5:00' AND live > 30

/* 2;4;7;Vypise pisnicky, ktere maji zanr a nemaji zanr Pop*/
SELECT s.name as Song, s.length, s.listened, s.live
FROM Song s
LEFT JOIN SongsGenre sg on sg.songID = s.songID
LEFT JOIN Genre g on g.genreID = sg.genreID
WHERE g.genreID IS NOT NULL AND NOT(g.name LIKE 'Pop')
GROUP BY s.name, s.length, s.listened, s.live



/* 3;1;4;Chceme vypsat vsechny skladby od tvùrcù "Hell Fanatics" pomoci IN*/
SELECT name as Song, listened, live
FROM Song
WHERE albumID IN (SELECT a.albumID
				FROM [User] u
				JOIN Album a on u.userID = a.userID
				WHERE artistN LIKE 'Hell Fanatics')

/* 3;2;4;Chceme vypsat vsechny skladby od tvùrcù "Hell Fanatics" pomoci EXISTS*/
SELECT name as Song, listened, live
FROM Song
WHERE EXISTS (SELECT *
			FROM [User] u
			JOIN Album a on u.userID = a.userID
			WHERE artistN LIKE 'Hell Fanatics' AND a.albumID = Song.albumID)

/* 3;3;4;Chceme vypsat vsechny skladby od tvùrcù "Hell Fanatics" pomoci ANY*/
SELECT name as Song, listened, live
FROM Song 
WHERE albumID = ANY (SELECT a.albumID
				FROM [User] u
				JOIN Album a on u.userID = a.userID
				WHERE artistN LIKE 'Hell Fanatics'
				GROUP BY a.albumID)

/* 3;4;4;Chceme vypsat vsechny skladby od tvùrcù "Hell Fanatics" pomoci UNION*/
SELECT name as Song, listened, live
FROM Song
WHERE albumID IN (SELECT albumID
				FROM Album
				WHERE name LIKE 'Hell Is Upon Us')
UNION 
SELECT name as Song, listened, live
FROM Song
WHERE albumID IN (SELECT albumID
				FROM Album
				WHERE name LIKE 'Angels From HELL')
ORDER BY name DESC



/* 4;1;2;Vypise pocet komentaru k pisnickam*/
SELECT s.name as Song, s.listened, s.live, count(c.comID) as Comments
FROM Song s
JOIN Comments c on s.songID = c.songID
GROUP BY s.name, s.listened, s.live

/* 4;2;2;Vypise prumerne hodnoceni u pisnicek*/
SELECT s.name as Song, s.listened, s.live, AVG(c.stars) as Stars
FROM Song s
JOIN Comments c on s.songID = c.songID
GROUP BY s.name, s.listened, s.live

/* 4;3;1;Vypise prumerne hodnoceni pisnicek, ktere maji lepsi hodnoceni nez 3 hvezdicky*/
SELECT s.name as Song, s.listened, s.live, AVG(c.stars) as Stars
FROM Song s
JOIN Comments c on s.songID = c.songID
GROUP BY s.name, s.listened, s.live
HAVING AVG(c.stars) > 3

/* 4;4;1;Vypise kolik dani uzivatele zaplatili celkove za sluzby v tomto systemu*/
SELECT u.first_name, u.last_name, SUM(p.cost) as Payed
FROM [User] u
JOIN Payment p on p.userID = u.userID
GROUP BY u.first_name, u.last_name



/* 5;1;4;Zobrazi prumerne hodnoceni uzivatelu, kteri uz nekdy pridavali hodnoceni ke skladbam*/
SELECT u.first_name, u.last_name
FROM [User] u
JOIN Comments c on c.userID = u.userID
GROUP BY u.first_name, u.last_name

/* 5;2;4;Zobrazi prumerne hodnoceni uzivatelu, kteri uz nekdy pridavali hodnoceni ke skladbam pomoci IN*/
SELECT u.first_name, u.last_name
FROM [User] u
WHERE userID IN (SELECT userID
				FROM Comments)
GROUP BY u.first_name, u.last_name

/* 5;3;12;Vypise pisne a zanr, ktery byl u nich zadany jako prvni*/
SELECT s.name as Song, s.length, s.listened, s.live, MAX(g.name) as Genre
FROM Song s
LEFT JOIN SongsGenre sg on sg.songID = s.songID
LEFT JOIN Genre g on g.genreID = sg.genreID
GROUP BY  s.name, s.length, s.listened, s.live

/* 5;4;2;Vypise Alba a jejich pocet pisni, a kde jmeno alba obsahuje slovo "hell"*/
SELECT a.name as Album, count(s.songID) as Songs
FROM Album a
LEFT JOIN Song s on s.albumID = a.albumID
WHERE a.name LIKE '%hell%'
GROUP BY a.name



/* 6;1;2;Vypise uzivatele, kteri se nekdy podileli na spolupraci a vypise se zde i pocet spolupraci*/
SELECT first_name, last_name, 
	(SELECT count(*)
	FROM Collab
	WHERE Collab.userID = [User].userID) as Collabs
FROM [User]
WHERE userID IN (SELECT userID
				 FROM Collab)

/* 6;2;2;Vypisuje se zde prava doba trvani alba (pri vytvareni mame sice uvedenou delku ale jelikoz chybi strana serveru musi byt zadana rucne, muze byt tedy chybna)*/
SELECT u.first_name, u.last_name, p.name,
	length = CAST((SELECT SUM(DATEDIFF(SECOND,'00:00:00',length))
	FROM Song
	JOIN SongInPL sp on sp.songID = Song.songID
	WHERE p.playlistID = sp.playlistID)/60 as VARCHAR(5))  + ':' + 
	CAST((SELECT SUM(DATEDIFF(SECOND,'00:00:00',length))
	FROM Song
	JOIN SongInPL sp on sp.songID = Song.songID
	WHERE p.playlistID = sp.playlistID)%60 as VARCHAR(5))
FROM [User] u
JOIN Playlist p on p.userID = u.userID
