USE MASTER 
GO
DROP DATABASE IF EXISTS SinhVien
CREATE DATABASE SinhVien
GO
USE SinhVien
GO
DROP TABLE IF EXISTS TblStudent 
CREATE TABLE TblStudent
(
StuId int identity primary key ,
StuPass NVARCHAR(50),
StuAdress NVARCHAR(50),
StuPhone NVARCHAR(50),
StuEmail NVARCHAR(50),
StuName NVARCHAR(50),
deptId int ,
)
GO
DROP TABLE IF EXISTS TblExam 
CREATE TABLE TblExam (
ExamId int identity PRIMARY KEY ,
ExamName NVARCHAR(50),
ExamMark float,
ExamDate	Date,
StuId int ,
CouId int ,
/*** Đánh giá nhận xét , của giáo viên , hoặc vắng mặt ko đủ tiêu chuẩn thi ***/
Comment Text ,
/*** ddiemr dau  ***/
MarkPass int ,
)
GO
DROP TABLE IF EXISTS TblCourse
CREATE TABLE TblCourse (
CouId int identity primary key,
CouName NVARCHAR(50),
CouSemester NVARCHAR(50),

)
GO
ALTER TABLE TblExam
ADD CONSTRAINT FK_TblExam_TblStudent
foreign key (StuId)
REFERENCES TblStudent(StuId)
go

ALTER TABLE TblExam 
ADD CONSTRAINT 
Fk_TblExam_TblCourse
FOREIGN KEY (CouId)
REFERENCES TblCourse(CouId)
GO
insert into TblStudent(StuPass,StuName,StuAdress,StuPhone,StuEmail,deptId)
Values (N'1003241',N'hay ko ' , N'hay qua ha ' , N'096784933234' , N'huynhtienthang@gmal.com' , '32')

Go
insert into TblStudent(StuPass,StuName,StuAdress,StuPhone,StuEmail,deptId)
Values (N'1003252',N'huỳnh tiến  ' , N'đường võ đức  ' , N'096784933890' , N'tienthang@gmal.com' , '35')
Go
insert into TblStudent(StuPass,StuName,StuAdress,StuPhone,StuEmail,deptId)
Values (N'1003263',N'hay quá  ' , N'hay ko  ' , N'098989087332' , N'hang@gmal.com' , '63')

Go
insert into TblStudent(StuPass,StuName,StuAdress,StuPhone,StuEmail,deptId)
Values (N'10032635',N'12312312  ' , N'Ngô đá dde' , N'098989084332' , N'quan@gmal.com' , '23')

Go

insert into TblStudent(StuPass,StuName,StuAdress,StuPhone,StuEmail,deptId)
Values (N'1003252',N'huỳnh tiến  ' , N'đường võ đức f ' , N'096784933890' , N'tienthang@gmal.com' , '35')
Go
insert into TblStudent(StuPass,StuName,StuAdress,StuPhone,StuEmail,deptId)
Values (N'1003263',N'hay quá  ' , N'hay ko f ' , N'098989087332' , N'hang@gmal.com' , '63')

Go
insert into TblStudent(StuPass,StuName,StuAdress,StuPhone,StuEmail,deptId)
Values (N'10032635',N'12312312  ' , N'Ngô đá f' , N'098989084332' , N'quan@gmal.com' , '23')

Go

insert into TblCourse(CouName,CouSemester)
Values (N'Ko hay rồi',N'Kỳ 1 - 2022')
Go
insert into TblCourse(CouName,CouSemester)
Values (N'Ko hay rồi ta',N'Kỳ 2 - 2023')
Go

/*** ExamId int identity PRIMARY KEY ,
ExamName NVARCHAR(50),
ExamMark int ,
ExamDate	Date,
StuId int ,
CouId int ,
/*** Đánh giá nhận xét , của giáo viên , hoặc vắng mặt ko đủ tiêu chuẩn thi ***/
Comment Text ,
/*** ddiemr dau  ***/
MarkPass int , ***/
insert into TblExam(ExamName,ExamMark,ExamDate,StuId,CouId,Comment,MarkPass)
VALUES(N'Hay mệt','5','2020/03/03','1','1',N'Học hành ko ổn lắm','6')
GO
insert into TblExam(ExamName,ExamMark,ExamDate,StuId,CouId,Comment,MarkPass)
VALUES(N'Hay mệt','6','2020/03/03','2','1',N'Học hành ko ổn lắm','6')
GO
insert into TblExam(ExamName,ExamMark,ExamDate,StuId,CouId,Comment,MarkPass)
VALUES(N'Hay mệt','7','2020/03/03','3','2',N'Học hành ko ổn lắm','6')
GO
insert into TblExam(ExamName,ExamMark,ExamDate,StuId,CouId,Comment,MarkPass)
VALUES(N'Hay mệt','8','2020/03/03','4','2',N'Học hành ko ổn lắm','6')
GO