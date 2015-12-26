CREATE TABLE Users (
  user_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  email VARCHAR(80) NOT NULL,
  display_name VARCHAR(50) NOT NULL,
  first_name VARCHAR(100) NOT NULL,
  last_name VARCHAR(100) NOT NULL,
  password CHAR(41) NOT NULL,
  phone_number INT NOT NULL,
  UNIQUE (email)
);