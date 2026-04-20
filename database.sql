-- ======================
-- 1. USERS
-- ======================
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(100) UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ======================
-- 2. GAMES
-- ======================
CREATE TABLE games (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ======================
-- 3. PRODUCTS
-- ======================
CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    game_id INT,
    name VARCHAR(100),
    price INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (game_id) REFERENCES games(id)
);

-- ======================
-- 4. TRANSACTIONS
-- ======================
CREATE TABLE transactions (
    id SERIAL PRIMARY KEY,
    user_id INT,
    product_id INT,
    quantity INT,
    total_price INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (product_id) REFERENCES products(id)
);

-- ======================
-- INDEX
-- ======================
CREATE INDEX idx_user_email ON users(email);
CREATE INDEX idx_product_game ON products(game_id);
CREATE INDEX idx_transaction_user ON transactions(user_id);
CREATE INDEX idx_transaction_product ON transactions(product_id);

-- ======================
--  DATA USERS
-- ======================
INSERT INTO users (name, email) VALUES
('Steven', 'steven@mail.com'),
('Budi', 'budi@mail.com'),
('Siti', 'siti@mail.com'),
('Andi', 'andi@mail.com'),
('Rina', 'rina@mail.com');

-- ======================
-- DATA GAMES
-- ======================
INSERT INTO games (name) VALUES
('Genshin Impact'),
('Honkai Star Rail'),
('Wuthering Waves'),
('Zenless Zone Zero'),
('Arknights Endfield');

-- ======================
-- DATA PRODUCTS
-- ======================
INSERT INTO products (game_id, name, price) VALUES
(1, '60 Genesis Crystal', 15000),
(1, '300 Genesis Crystal', 75000),
(2, '90 Stellar Jade', 20000),
(3, 'Lunite Pack', 50000),
(4, 'Monochrome Pack', 40000);

-- ======================
--  DATA TRANSACTIONS
-- ======================
INSERT INTO transactions (user_id, product_id, quantity, total_price) VALUES
(1, 1, 1, 15000),
(2, 2, 1, 75000),
(3, 3, 2, 40000),
(4, 4, 1, 50000),
(5, 5, 1, 40000);