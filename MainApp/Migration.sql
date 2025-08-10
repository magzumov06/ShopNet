CREATE DATABASE "ShopNet_DB";

CREATE TABLE customers (
    id SERIAL PRIMARY KEY,
    firstname VARCHAR(100) NOT NULL,
    lastname VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phone VARCHAR(20) UNIQUE
);

CREATE TABLE sellers (
    id SERIAL PRIMARY KEY,
    firstname VARCHAR(100) NOT NULL,
    lastname VARCHAR(100) NOT NULL,
    shop_name VARCHAR(150) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE categories (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL,
    description TEXT
);

CREATE TABLE products (
     id SERIAL PRIMARY KEY,
     name VARCHAR(200) NOT NULL,
     price NUMERIC(10,2) CHECK (price > 0),
     quantity INT CHECK (quantity >= 0),
     categoryId INT REFERENCES categories(id) ON DELETE SET NULL,
     sellerId INT REFERENCES sellers(id) ON DELETE CASCADE
);

CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    customer_id INT REFERENCES customers(id) ON DELETE CASCADE,
    order_date DATE NOT NULL DEFAULT CURRENT_DATE
);

CREATE TABLE order_items (
    id SERIAL PRIMARY KEY,
    order_id INT REFERENCES orders(id) ON DELETE CASCADE,
    product_id INT REFERENCES products(id) ON DELETE CASCADE,
    quantity INT CHECK (quantity > 0),
    price NUMERIC(10,2) CHECK (price > 0),
    UNIQUE(order_id, product_id)
);