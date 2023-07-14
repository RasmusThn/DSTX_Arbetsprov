# DSTX_Arbetsprov

1. Create Database and Table, info down below.
2. Go to ~\source\repos\DSTX_Arbetsprov\DSTX_Arbetsprov\appsettings.json and change, "MyNotSoSecretToken": "Api-Key Here", to your Key.
3. And in the same file set det DefaultConnection to the connection-string for your Db
          
      
            
MySQL commands to create DB and collect Data:                                          
////////////////////////////////////////////////                                                                      
CREATE DATABASE DSTX_DB;

USE DSTX_DB;


CREATE TABLE FileRecords (
  id INT AUTO_INCREMENT PRIMARY KEY,
  filename VARCHAR(255) NOT NULL,
  FileData LONGBLOB NOT NULL,
  ReportId INT NOT NULL
);

SELECT * FROM dstx_db.filerecords;

SELECT FileData FROM FileRecords WHERE id = 1;                                                                        
////////////////////////////////////////////////                                                              
