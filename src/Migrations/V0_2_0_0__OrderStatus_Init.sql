DROP TYPE IF EXISTS OrderStatus;
CREATE TYPE OrderStatus AS ENUM ('New', 'Processing', 'Finish');