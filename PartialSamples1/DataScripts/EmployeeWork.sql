--- Goes along with EmployeeWork class for SQL Server

CREATE TABLE EmployeeWork
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    HourlyWage DECIMAL(10,2) NOT NULL,
    HoursWorked INT NOT NULL,
    OvertimeRate DECIMAL(5,2) NOT NULL DEFAULT 1.5,
    OvertimeThreshold INT NOT NULL DEFAULT 40,

    Salary AS 
    (
        CASE 
            WHEN HoursWorked <= OvertimeThreshold 
                THEN HourlyWage * HoursWorked
            ELSE (HourlyWage * OvertimeThreshold) 
                 + ((HoursWorked - OvertimeThreshold) * OvertimeRate * HourlyWage)
        END
    )
);

INSERT INTO EmployeeWork (HourlyWage, HoursWorked, OvertimeRate, OvertimeThreshold)
VALUES (15.50, 40, 1.5, 40);

INSERT INTO EmployeeWork (HourlyWage, HoursWorked, OvertimeRate, OvertimeThreshold)
VALUES (20.00, 38, 1.5, 40);

INSERT INTO EmployeeWork (HourlyWage, HoursWorked, OvertimeRate, OvertimeThreshold)
VALUES (18.75, 45, 1.5, 40);

INSERT INTO EmployeeWork (HourlyWage, HoursWorked, OvertimeRate, OvertimeThreshold)
VALUES (22.00, 50, 1.75, 40);

INSERT INTO EmployeeWork (HourlyWage, HoursWorked, OvertimeRate, OvertimeThreshold)
VALUES (16.25, 42, 1.5, 40);
