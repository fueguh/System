-- =========================================
-- 1. Create AppointmentServices table if not exists
-- =========================================
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentServices]') 
                 AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[AppointmentServices] (
        [AppointmentServiceID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [AppointmentID] INT NOT NULL,
        [ServiceID] INT NOT NULL,
        CONSTRAINT [FK_AppointmentServices_Appointments] 
            FOREIGN KEY ([AppointmentID]) REFERENCES [dbo].[Appointments]([AppointmentID]),
        CONSTRAINT [FK_AppointmentServices_Services] 
            FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Services]([ServiceID])
    );
END
GO

-- =========================================
-- 2. Copy existing ServiceID data to AppointmentServices
-- =========================================
INSERT INTO [dbo].[AppointmentServices] (AppointmentID, ServiceID)
SELECT AppointmentID, ServiceID
FROM [dbo].[Appointments]
WHERE ServiceID IS NOT NULL;
GO

-- =========================================
-- 3. Drop the old foreign key first (if exists)
-- =========================================
IF EXISTS (SELECT * FROM sys.foreign_keys 
           WHERE name = 'FK_Appointments_Services')
BEGIN
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT FK_Appointments_Services;
END
GO

-- =========================================
-- 4. Drop the old ServiceID column
-- =========================================
IF EXISTS (SELECT * FROM sys.columns 
           WHERE Name = N'ServiceID' AND Object_ID = Object_ID(N'dbo.Appointments'))
BEGIN
    ALTER TABLE [dbo].[Appointments] DROP COLUMN ServiceID;
END
GO

PRINT 'Migration complete: ServiceID moved to AppointmentServices.';
GO

-- =========================================
-- 5. Create UserSessions table if not exists
-- =========================================
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[UserSessions]') 
                 AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[UserSessions] (
        [SessionID]    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [UserID]       INT NOT NULL,
        [SessionToken] NVARCHAR(100) NOT NULL,
        [DeviceName]   NVARCHAR(100) NULL,
        [CreatedAt]    DATETIME DEFAULT (getdate()) NULL,
        [ExpiresAt]    DATETIME NULL,
        [IsActive]     BIT DEFAULT ((1)) NULL,
        CONSTRAINT [FK_UserSessions_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users]([UserID])
    );
END
GO

-- =========================================
-- 6. Create ClinicInfo table if not exists
-- =========================================
IF NOT EXISTS (SELECT * FROM sys.objects 
               WHERE object_id = OBJECT_ID(N'[dbo].[ClinicInfo]') 
                 AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ClinicInfo] (
        [ClinicID]       INT NOT NULL PRIMARY KEY,
        [ClinicName]     NVARCHAR(100) NULL,
        [ClinicAddress]  NVARCHAR(200) NULL,
        [ContactNumber]  NVARCHAR(50) NULL,
        [Email]          NVARCHAR(100) NULL,
        [OperatingHours] NVARCHAR(100) NULL
    );
END
GO

PRINT 'All tables created/migrated successfully.';
