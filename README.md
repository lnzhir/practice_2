## Типы данных: Задание 1
Проект - DataTypes1

## Типы данных: Задание 2
Проект - DataTypes2

## ООП
Проект - OOP

## Коллекции
Проект - Collections

## Базы данных
Описание таблиц:
```SQL
CREATE TABLE Authors
(
    id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    name NVARCHAR(256) NOT NULL
);

CREATE TABLE Publishers
(
    id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    name NVARCHAR(50) NOT NULL
);

CREATE TABLE Readers
(
    id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    name NVARCHAR(256) NOT NULL,
    registrationDate DATE NOT NULL,
);

CREATE TABLE Books
(
    id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    isbn VARCHAR(30),
    publishYear INT,
    count INT,
    publisherId UNIQUEIDENTIFIER NOT NULL REFERENCES Publishers (id),
    authorId UNIQUEIDENTIFIER NOT NULL REFERENCES Authors (id)
);

CREATE TABLE LendingBooks
(
    id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    readerId UNIQUEIDENTIFIER REFERENCES Readers (id),
    bookId UNIQUEIDENTIFIER REFERENCES Books (id),
    limitDate DATE,
    returned BIT
);
```

Запросы:
```SQL
SELECT Readers.name, Books.name, returned FROM LendingBooks, Readers, Books
WHERE 
LendingBooks.readerId = Readers.id and
LendingBooks.bookId = Books.id and
returned = 0

SELECT name, count FROM Books
ORDER BY count DESC

SELECT COUNT(*) returned FROM LendingBooks
WHERE returned = 0

SELECT id
FROM Books
INTERSECT SELECT bookId
FROM LendingBooks

SELECT LendingBooks.bookId, name
FROM LendingBooks LEFT JOIN Readers 
ON LendingBooks.readerId = Readers.Id

SELECT LendingBooks.bookId, name
FROM LendingBooks RIGHT JOIN Readers 
ON LendingBooks.readerId = Readers.Id

UPDATE Books
SET count = count + 1
WHERE id = '5AB47608-53AA-404E-942E-089F91D929FD'

DELETE LendingBooks
WHERE limitDate < '19-06-2026' and returned = 1
```

Индексы:
```SQL
CREATE UNIQUE INDEX UX_Books_ISBN 
ON Books(isbn)
WHERE isbn IS NOT NULL;

CREATE UNIQUE INDEX UX_Books_Details 
ON Books(name, authorId, publisherId, publishYear);
```

## Работа с БД из кода
Проект - DB

## HTTP
![](/http1.png)
![](/http2.png)
![](/http3.png)

## Финальное задание (Приложение для учета личных финансов и бюджетирования) 
Проекты: FinanceDB, FinanceFront, FinanceWebApi

Скриншоты запросов из Postman
![](/webapi1.png)
![](/webapi2.png)
![](/webapi3.png)
![](/webapi4.png)
![](/webapi5.png)
![](/webapi6.png)