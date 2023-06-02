create database DB_wara_service;

use DB_wara_service;

create table RegisterURL(
ID INT PRIMARY KEY,
UserID INT,
Title VARCHAR(100),
Completed BIT,
ConsultationDate DATETIME
);
select * from RegisterURL;
delete from RegisterURL;