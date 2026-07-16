


CREATE UNIQUE INDEX UX_Books_ISBN 
ON Books(isbn)
WHERE isbn IS NOT NULL;

CREATE UNIQUE INDEX UX_Books_Details 
ON Books(name, authorId, publisherId, publishYear);

