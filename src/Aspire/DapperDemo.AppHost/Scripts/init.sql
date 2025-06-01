IF
    DB_ID('app-db') IS NULL
CREATE
    DATABASE [app-db];
GO

USE [app-db];
GO

IF OBJECT_ID('dbo.product_categories', 'U') IS NULL
    BEGIN
        CREATE TABLE dbo.product_categories
        (
            id   UNIQUEIDENTIFIER PRIMARY KEY,
            name NVARCHAR(255) NOT NULL
        );
    END
GO

IF OBJECT_ID('dbo.products', 'U') IS NULL
    BEGIN
        CREATE TABLE dbo.products
        (
            id          UNIQUEIDENTIFIER PRIMARY KEY,
            name        NVARCHAR(255)    NOT NULL,
            category_id UNIQUEIDENTIFIER NOT NULL,
            description NVARCHAR(MAX),
            price       DECIMAL(18, 2)   NOT NULL,
            FOREIGN KEY (category_id) REFERENCES dbo.product_categories (id)
        );
    END
GO

IF NOT EXISTS (SELECT 1
               FROM dbo.product_categories)
    BEGIN
        INSERT INTO dbo.product_categories (id, name)
        VALUES ('A1B2C3D4-E5F6-7890-1234-567890ABCDEF', 'Electronics'),
               ('B2C3D4E5-F6A1-2345-6789-0ABCDEF12345', 'Books'),
               ('C3D4E5F6-A1B2-3456-7890-ABCDEF123456', 'Home Goods'),
               ('D4E5F6A1-B2C3-4567-8901-BCDEF1234567', 'Clothing'),
               ('E5F6A1B2-C3D4-5678-9012-CDEF12345678', 'Food');
    END
GO

IF NOT EXISTS (SELECT 1
               FROM dbo.products)
    BEGIN
        INSERT INTO dbo.products (id, name, category_id, description, price)
        VALUES ('00000000-0000-0000-0000-000000000001', 'Laptop Pro', 'A1B2C3D4-E5F6-7890-1234-567890ABCDEF',
                'Powerful laptop for professionals.', 1200.00),
               ('00000000-0000-0000-0000-000000000002', 'Fantasy Novel', 'B2C3D4E5-F6A1-2345-6789-0ABCDEF12345',
                'Epic fantasy adventure.', 25.50),
               ('00000000-0000-0000-0000-000000000003', 'Smart TV 4K', 'A1B2C3D4-E5F6-7890-1234-567890ABCDEF',
                'High-definition smart television.', 800.00),
               ('00000000-0000-0000-0000-000000000004', 'Cookbook Essentials', 'B2C3D4E5-F6A1-2345-6789-0ABCDEF12345',
                'Recipes for everyday cooking.', 30.00),
               ('00000000-0000-0000-0000-000000000005', 'Coffee Maker', 'C3D4E5F6-A1B2-3456-7890-ABCDEF123456',
                'Automatic drip coffee maker.', 75.00),
               ('00000000-0000-0000-0000-000000000006', 'Summer Dress', 'D4E5F6A1-B2C3-4567-8901-BCDEF1234567',
                'Light and comfortable summer dress.', 45.99),
               ('00000000-0000-0000-0000-000000000007', 'Organic Apples', 'E5F6A1B2-C3D4-5678-9012-CDEF12345678',
                'Fresh organic apples (1kg).', 5.00),
               ('00000000-0000-0000-0000-000000000008', 'Gaming Mouse', 'A1B2C3D4-E5F6-7890-1234-567890ABCDEF',
                'High-precision gaming mouse.', 50.00),
               ('00000000-0000-0000-0000-000000000009', 'Science Fiction Classic',
                'B2C3D4E5-F6A1-2345-6789-0ABCDEF12345',
                'A timeless science fiction novel.', 18.75),
               ('00000000-0000-0000-0000-000000000010', 'Vacuum Cleaner', 'C3D4E5F6-A1B2-3456-7890-ABCDEF123456',
                'Powerful bagless vacuum cleaner.', 150.00),
               ('00000000-0000-0000-0000-000000000011', 'Jeans Slim Fit', 'D4E5F6A1-B2C3-4567-8901-BCDEF1234567',
                'Comfortable slim fit jeans.', 65.00),
               ('00000000-0000-0000-0000-000000000012', 'Whole Grain Bread', 'E5F6A1B2-C3D4-5678-9012-CDEF12345678',
                'Freshly baked whole grain bread.', 3.50),
               ('00000000-0000-0000-0000-000000000013', 'External Hard Drive', 'A1B2C3D4-E5F6-7890-1234-567890ABCDEF',
                '1TB external hard drive.', 70.00),
               ('00000000-0000-0000-0000-000000000014', 'Self-Help Bestseller', 'B2C3D4E5-F6A1-2345-6789-0ABCDEF12345',
                'Guide to personal development.', 20.00),
               ('00000000-0000-0000-0000-000000000015', 'Blender', 'C3D4E5F6-A1B2-3456-7890-ABCDEF123456',
                'High-speed kitchen blender.', 90.00),
               ('00000000-0000-0000-0000-000000000016', 'Running Shoes', 'D4E5F6A1-B2C3-4567-8901-BCDEF1234567',
                'Lightweight running shoes.', 85.00),
               ('00000000-0000-0000-0000-000000000017', 'Milk (1 Liter)', 'E5F6A1B2-C3D4-5678-9012-CDEF12345678',
                'Fresh pasteurized milk.', 1.80),
               ('00000000-0000-0000-0000-000000000018', 'Webcam Full HD', 'A1B2C3D4-E5F6-7890-1234-567890ABCDEF',
                'Full HD webcam for video calls.', 40.00),
               ('00000000-0000-0000-0000-000000000019', 'Children''s Storybook', 'B2C3D4E5-F6A1-2345-6789-0ABCDEF12345',
                'Illustrated stories for children.', 12.00),
               ('00000000-0000-0000-0000-000000000020', 'Desk Lamp LED', 'C3D4E5F6-A1B2-3456-7890-ABCDEF123456',
                'Modern LED desk lamp.', 35.00);
    END
GO