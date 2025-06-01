-- Enable pgcrypto extension if not present
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE product_categories (
                                    id UUID PRIMARY KEY,
                                    name VARCHAR(100) NOT NULL
);

CREATE TABLE products (
                          id UUID PRIMARY KEY,
                          name VARCHAR(150) NOT NULL,
                          description TEXT,
                          category_id UUID NOT NULL REFERENCES product_categories(id),
                          price NUMERIC(10,2) NOT NULL CHECK (price >= 0)
);

-- Indexes
CREATE INDEX idx_products_category_id ON products(category_id);
CREATE INDEX idx_products_name ON products(name);

-- Insert categories
INSERT INTO product_categories (id, name) VALUES
                                              ('018f3e42-756f-73c7-9c8a-ff30c5eb56a1', 'Electronics'),
                                              ('018f3e42-756f-73c7-9c8a-ff30c5eb56a2', 'Books'),
                                              ('018f3e42-756f-73c7-9c8a-ff30c5eb56a3', 'Clothing'),
                                              ('018f3e42-756f-73c7-9c8a-ff30c5eb56a4', 'Home & Kitchen'),
                                              ('018f3e42-756f-73c7-9c8a-ff30c5eb56a5', 'Toys');

-- Insert products
INSERT INTO products (id, name, description, category_id, price) VALUES
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc01', 'Smartphone', 'Latest model with OLED display', '018f3e42-756f-73c7-9c8a-ff30c5eb56a1', 699.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc02', 'Wireless Headphones', 'Noise cancelling over-ear headphones', '018f3e42-756f-73c7-9c8a-ff30c5eb56a1', 199.90),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc03', 'Laptop', '15 inch, 16GB RAM, 512GB SSD', '018f3e42-756f-73c7-9c8a-ff30c5eb56a1', 1299.00),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc04', 'USB-C Charger', '65W fast charging', '018f3e42-756f-73c7-9c8a-ff30c5eb56a1', 29.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc05', 'Fiction Novel', 'Bestselling mystery thriller', '018f3e42-756f-73c7-9c8a-ff30c5eb56a2', 12.50),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc06', 'Science Textbook', 'University level physics', '018f3e42-756f-73c7-9c8a-ff30c5eb56a2', 45.00),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc07', 'T-Shirt', 'Cotton, unisex, medium', '018f3e42-756f-73c7-9c8a-ff30c5eb56a3', 9.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc08', 'Jeans', 'Slim fit, dark blue', '018f3e42-756f-73c7-9c8a-ff30c5eb56a3', 39.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc09', 'Jacket', 'Waterproof, lightweight', '018f3e42-756f-73c7-9c8a-ff30c5eb56a3', 79.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc0a', 'Blender', '600W, multiple speeds', '018f3e42-756f-73c7-9c8a-ff30c5eb56a4', 49.95),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc0b', 'Coffee Maker', 'Programmable, 12 cup', '018f3e42-756f-73c7-9c8a-ff30c5eb56a4', 89.90),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc0c', 'Knife Set', 'Stainless steel, 6 pieces', '018f3e42-756f-73c7-9c8a-ff30c5eb56a4', 34.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc0d', 'Cookbook', 'Easy recipes for beginners', '018f3e42-756f-73c7-9c8a-ff30c5eb56a2', 14.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc0e', 'Puzzle Set', '1000-piece landscape puzzle', '018f3e42-756f-73c7-9c8a-ff30c5eb56a5', 19.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc0f', 'Board Game', 'Strategy game for 4 players', '018f3e42-756f-73c7-9c8a-ff30c5eb56a5', 29.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc10', 'Dollhouse', 'Wooden with furniture', '018f3e42-756f-73c7-9c8a-ff30c5eb56a5', 59.99),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc11', 'Action Figure', '10 cm collectible', '018f3e42-756f-73c7-9c8a-ff30c5eb56a5', 15.00),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc12', 'Desk Lamp', 'LED with dimmer', '018f3e42-756f-73c7-9c8a-ff30c5eb56a4', 24.90),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc13', 'Notebook', '200 pages, hardcover', '018f3e42-756f-73c7-9c8a-ff30c5eb56a2', 5.49),
                                                                     ('018f3e45-01da-7a3e-bb4a-382ff1c4dc14', 'Backpack', 'Water resistant, laptop sleeve', '018f3e42-756f-73c7-9c8a-ff30c5eb56a3', 44.00);