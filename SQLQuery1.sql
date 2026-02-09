USE [Dental];
GO

-- Create Receipts table
CREATE TABLE [dbo].[Receipts] (
    [ReceiptID] INT IDENTITY(1,1) NOT NULL,
    [AppointmentID] INT NOT NULL,
    [PatientID] INT NOT NULL,
    [UserID] INT NOT NULL,
    [TotalAmount] DECIMAL(10,2) NOT NULL,
    [PaymentMethod] VARCHAR(50) NOT NULL,
    [DateIssued] DATETIME NOT NULL 
        CONSTRAINT DF_Receipts_DateIssued DEFAULT (GETDATE()),
    CONSTRAINT PK_Receipts PRIMARY KEY CLUSTERED ([ReceiptID] ASC)
);
GO

-- Foreign key to Appointments
ALTER TABLE [dbo].[Receipts]
ADD CONSTRAINT FK_Receipts_Appointments
FOREIGN KEY ([AppointmentID]) REFERENCES [dbo].[Appointments] ([AppointmentID]);
GO

-- Foreign key to Patients
ALTER TABLE [dbo].[Receipts]
ADD CONSTRAINT FK_Receipts_Patients
FOREIGN KEY ([PatientID]) REFERENCES [dbo].[Patients] ([PatientID]);
GO

-- Foreign key to Users (staff/admin issuing receipt)
ALTER TABLE [dbo].[Receipts]
ADD CONSTRAINT FK_Receipts_Users
FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]);
GO