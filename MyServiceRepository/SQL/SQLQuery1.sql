
-- SQL is a structural query language used to manage relational database
-- Relational DB concepts : Tables, Primary Key and Foreign key

select * from students

Create table parents( -- Tables
name varchar(max),
age int, 
student_id int foreign key references students(id)) -- Foreign key

alter table parents
add parent_id int 

select * from parents

alter table parents
alter column parent_id int NOT NULL;

alter table parents
add primary key (parent_id);

insert into parents values ('Uma', 60, 1, 1)
insert into students values (3,'Atul', 12, 70)
insert into parents values ('Sujata', 55, 3, 2)
insert into students values (4,'Rajat', 12, 72)

insert into parents values ('Uma', 60, 5, 2) -- Foreign key constraint

sp_help parents

select distinct name from parents

select * from parents order by parent_id desc

-- LIMIT and OFFSET

select * from parents
order by parent_id -- You need to specify an ORDER BY clause
offset 0 Rows	-- Skip the first 0 rows
FETCH NEXT 10 ROWS ONLY;  -- Retrieve the next 10 rows


--  Joins

select * from students
select * from parents

-- Inner Join

select s.name as 'student Name' , p.name as 'Parent Name' from
students s
Join parents p
on s.id = p.student_id -- Rows matching this condition

select s.name as 'student Name' , p.name as 'Parent Name' from
students s
left join  -- LEFT JOIN and LEFT OUTER JOIN are functionally the same
parents p
on p.student_id = s.id

select s.name as 'student Name' , p.name as 'Parent Name' from
students s
right join 
parents p
on p.student_id = s.id

select s.name as 'student Name' , p.name as 'Parent Name' from
students s
full outer join 
parents p
on p.student_id = s.id

-- Self Join
select * from 
students s
left join students s1
on s.id = s1.id

-- Group By

select MAX(result) as 'marks', class
from students
group by class


select MAX(result) as 'marks', class
from students
group by class 
having class = 12 and MAX(result) > 1 -- works on aggregated data

-- Subquery

select * from
parents where age > ( select AVG(age) from parents)

-- Common Table Expressions

-- Allow to define a temp result set using WITH clause.

With Student12 as
(select * from students where class = 12)
select name , result
from Student12 

-- Window Fuction

-- ROW_NUMBER , Assign unique number to each row
select name,ROW_NUMBER() OVER (Order by parent_id desc)as 'RowNo' from parents

select name, RANK() OVER (order by result desc) as 'Class Rank' from students

select name, RANK() OVER (order by class desc) as 'Class Rank' from students  -- Tied values same rank

select * from students

select name, Dense_rank() OVER (partition by result order by class desc) as 'Class Rank' from students  -- Tied values same rank

-- Index
Create NONCLUSTERED INDEX Ind1
on students(id,class)

DROP INDEX Ind1
ON students;

ALTER INDEX Ind1
ON students
REBUILD;

--A clustered index physically sorts the data in the table based on the indexed column(s), unlike a non-clustered index that stores a separate data structure for indexing. 
--Each table can have only one clustered index because the rows can be physically stored in only one order.

--CREATE CLUSTERED INDEX Index_Name2
--ON students (id);

sp_help  students


-- Create Procedures

create procedure testproc
(@param1 int)
AS
BEGIN
	print 'hi'
END;

ALTER procedure testproc
(@param1 int)
AS
BEGIN
	select * from students where id = @param1
END;

exec testproc @param1 = 1
exec testproc 1

-- Functions

Create Function testFund(
@param1 int)
returns int
begin
	return (select CONVERT(int, result) from students where id = @param1)
end

SELECT dbo.testFund(1) AS result;

Create Function testFund2(
@param1 int)
returns table
as
RETURN
(
    SELECT * 
    FROM students 
    WHERE id = @param1
);

SELECT * 
FROM dbo.testFund2(1);

-- Transaction and TRY/CATCH

ALTER PROCEDURE PROC_2
(
@P1 INT
)
AS
BEGIN
	BEGIN TRANSACTION

	BEGIN TRY

		SELECT * FROM parents WHERE student_id = @P1
		SELECT * FROM parents WHERE student_id = 2
		COMMIT

	END TRY
	BEGIN CATCH

	ROLLBACK;

	END CATCH

END;

EXEC PROC_2 1