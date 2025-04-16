CREATE TABLE IF NOT EXISTS Books (
    book_id INTEGER PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    author TEXT NOT NULL,
    year INTEGER NOT NULL,
    genre TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Users (
    user_id INTEGER PRIMARY KEY AUTOINCREMENT,
    school_id INTEGER UNIQUE NOT NULL,
    fname TEXT NOT NULL,
    lname TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Transactions (
    transaction_id INTEGER PRIMARY KEY AUTOINCREMENT,
    book_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,
    borrow_date DATETIME NOT NULL,
    due_date DATETIME NOT NULL,
    status TEXT CHECK(status IN ('borrowed', 'overdue')),
    FOREIGN KEY(book_id) REFERENCES Books(book_id),
    FOREIGN KEY(user_id) REFERENCES Users(user_id)
);