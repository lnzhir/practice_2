

SELECT 
	Readers.name, 
	Books.name, 
	returned 
FROM 
	LendingBooks, 
	Readers, 
	Books
WHERE 
LendingBooks.readerId = Readers.id and
LendingBooks.bookId = Books.id and
returned = 0

SELECT 
	name, 
	count 
FROM Books
ORDER BY count DESC

SELECT COUNT(*) returned 
FROM LendingBooks
WHERE returned = 0

SELECT id
FROM Books
INTERSECT 
SELECT bookId
FROM LendingBooks

SELECT 
	LendingBooks.bookId, 
	name
FROM LendingBooks 
LEFT JOIN Readers 
ON LendingBooks.readerId = Readers.Id

SELECT 
	LendingBooks.bookId, 
	name
FROM LendingBooks 
RIGHT JOIN Readers 
ON LendingBooks.readerId = Readers.Id

UPDATE Books
SET count = count + 1
WHERE id = '5AB47608-53AA-404E-942E-089F91D929FD'

DELETE LendingBooks
WHERE limitDate < '19-06-2026' and returned = 1

