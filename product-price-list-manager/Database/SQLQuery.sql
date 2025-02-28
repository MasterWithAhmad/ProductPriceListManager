CREATE TABLE Products (
    ProductID NVARCHAR(10) PRIMARY KEY,
    ProductName NVARCHAR(255) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
    StockQuantity INT NOT NULL CHECK (StockQuantity >= 0),
    LastUpdated DATETIME DEFAULT GETDATE()
);

INSERT INTO Products (ProductID, ProductName, Category, Price, StockQuantity, LastUpdated) VALUES
('P1001', 'iPhone 14', 'Smartphone', 799.99, 10, '2025-02-27'),
('P1002', 'Dell Laptop', 'Laptop', 1200.00, 5, '2025-02-25'),
('P1003', 'Wireless Mouse', 'Accessories', 25.99, 30, '2025-02-20'),
('P1004', 'Samsung Galaxy S23', 'Smartphone', 699.99, 8, '2025-02-22'),
('P1005', 'HP Pavilion 15', 'Laptop', 950.00, 4, '2025-02-23'),
('P1006', 'Sony WH-1000XM5 Headphones', 'Accessories', 349.99, 15, '2025-02-24'),
('P1007', 'Apple AirPods Pro', 'Accessories', 199.99, 20, '2025-02-21'),
('P1008', 'Logitech K380 Keyboard', 'Accessories', 49.99, 25, '2025-02-19'),
('P1009', 'Razer Gaming Mouse', 'Accessories', 59.99, 18, '2025-02-18'),
('P1010', 'LG 27-inch Monitor', 'Monitor', 299.99, 6, '2025-02-20'),
('P1011', 'ASUS ROG Gaming Laptop', 'Laptop', 1500.00, 3, '2025-02-18'),
('P1012', 'Xiaomi Mi Band 7', 'Wearable', 49.99, 22, '2025-02-25'),
('P1013', 'Canon EOS R10 Camera', 'Camera', 1299.99, 7, '2025-02-24'),
('P1014', 'Bose SoundLink Speaker', 'Audio', 179.99, 12, '2025-02-21'),
('P1015', 'Google Pixel 7', 'Smartphone', 599.99, 9, '2025-02-22'),
('P1016', 'Microsoft Surface Pro 9', 'Laptop', 1399.99, 4, '2025-02-26'),
('P1017', 'Samsung 1TB SSD', 'Storage', 149.99, 17, '2025-02-19'),
('P1018', 'TP-Link Wi-Fi Router', 'Networking', 89.99, 14, '2025-02-23'),
('P1019', 'NVIDIA RTX 4070 Graphics Card', 'Components', 599.99, 5, '2025-02-20'),
('P1020', 'Corsair 16GB RAM', 'Components', 79.99, 16, '2025-02-21'),
('P1021', 'Samsung Galaxy Buds 2', 'Accessories', 129.99, 18, '2025-02-22'),
('P1022', 'Lenovo ThinkPad X1', 'Laptop', 1650.00, 3, '2025-02-26'),
('P1023', 'Seagate 2TB External HDD', 'Storage', 89.99, 25, '2025-02-20'),
('P1024', 'Sony PlayStation 5', 'Gaming', 499.99, 7, '2025-02-24'),
('P1025', 'Xbox Series X', 'Gaming', 499.99, 6, '2025-02-23'),
('P1026', 'Apple MacBook Air M2', 'Laptop', 1199.99, 4, '2025-02-25'),
('P1027', 'Google Nest Hub', 'Smart Home', 99.99, 12, '2025-02-21'),
('P1028', 'JBL Flip 6 Bluetooth Speaker', 'Audio', 119.99, 14, '2025-02-20'),
('P1029', 'Garmin Fenix 7 Smartwatch', 'Wearable', 699.99, 5, '2025-02-22'),
('P1030', 'TP-Link Mesh WiFi System', 'Networking', 249.99, 8, '2025-02-23'),
('P1031', 'Razer BlackWidow Keyboard', 'Accessories', 129.99, 10, '2025-02-22'),
('P1032', 'WD Black 1TB NVMe SSD', 'Storage', 169.99, 15, '2025-02-19'),
('P1033', 'Samsung 32-inch Curved Monitor', 'Monitor', 399.99, 6, '2025-02-18'),
('P1034', 'Corsair K95 RGB Keyboard', 'Accessories', 199.99, 9, '2025-02-19'),
('P1035', 'Asus TUF Gaming Laptop', 'Laptop', 1299.99, 3, '2025-02-17'),
('P1036', 'HyperX Cloud II Gaming Headset', 'Accessories', 99.99, 20, '2025-02-18'),
('P1037', 'Intel Core i9-13900K Processor', 'Components', 599.99, 4, '2025-02-19'),
('P1038', 'ASUS ROG Strix RTX 4090', 'Components', 1799.99, 2, '2025-02-20'),
('P1039', 'Elgato Stream Deck', 'Accessories', 149.99, 7, '2025-02-21'),
('P1040', 'Bose QuietComfort Earbuds', 'Audio', 249.99, 10, '2025-02-22');