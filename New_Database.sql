/* =========================================================
   MIGRATION: Move ServiceID to AppointmentServices
   SAFE: Idempotent, rerunnable, team-safe
   ========================================================= */

-- =========================================
-- 1. Create AppointmentServices table if not exists
-- =========================================
IF NOT EXISTS (
    SELECT 1 FROM sys.objects 
    WHERE object_id = OBJECT_ID(N'dbo.AppointmentServices') 
      AND type = 'U'
)
BEGIN
    CREATE TABLE dbo.AppointmentServices (
        AppointmentServiceID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        AppointmentID INT NOT NULL,
        ServiceID INT NOT NULL,
        CONSTRAINT FK_AppointmentServices_Appointments
            FOREIGN KEY (AppointmentID)
            REFERENCES dbo.Appointments (AppointmentID),
        CONSTRAINT FK_AppointmentServices_Services
            FOREIGN KEY (ServiceID)
            REFERENCES dbo.Services (ServiceID)
    );
END
GO

-- =========================================
-- 2. Migrate existing ServiceID data (SAFE)
--    - Only if column still exists
--    - Prevents duplicate inserts
-- =========================================
IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE Name = N'ServiceID'
      AND Object_ID = OBJECT_ID(N'dbo.Appointments')
)
BEGIN
    INSERT INTO dbo.AppointmentServices (AppointmentID, ServiceID)
    SELECT A.AppointmentID, A.ServiceID
    FROM dbo.Appointments A
    WHERE A.ServiceID IS NOT NULL
      AND NOT EXISTS (
          SELECT 1 FROM dbo.AppointmentServices S
          WHERE S.AppointmentID = A.AppointmentID
            AND S.ServiceID = A.ServiceID
      );
END
GO

-- =========================================
-- 3. Drop old FK from Appointments -> Services (NAME SAFE)
-- =========================================
DECLARE @fkName NVARCHAR(128);

SELECT @fkName = fk.name
FROM sys.foreign_keys fk
WHERE fk.parent_object_id = OBJECT_ID(N'dbo.Appointments')
  AND fk.referenced_object_id = OBJECT_ID(N'dbo.Services');

IF @fkName IS NOT NULL
BEGIN
    EXEC ('ALTER TABLE dbo.Appointments DROP CONSTRAINT ' + @fkName);
END
GO

-- =========================================
-- 4. Drop old ServiceID column (if exists)
-- =========================================
IF EXISTS (
    SELECT 1 FROM sys.columns
    WHERE Name = N'ServiceID'
      AND Object_ID = OBJECT_ID(N'dbo.Appointments')
)
BEGIN
    ALTER TABLE dbo.Appointments DROP COLUMN ServiceID;
END
GO

PRINT 'Migration complete: ServiceID moved to AppointmentServices.';
GO

-- =========================================
-- 5. Create UserSessions table if not exists
-- =========================================
IF NOT EXISTS (
    SELECT 1 FROM sys.objects
    WHERE object_id = OBJECT_ID(N'dbo.UserSessions')
      AND type = 'U'
)
BEGIN
    CREATE TABLE dbo.UserSessions (
        SessionID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        UserID INT NOT NULL,
        SessionToken NVARCHAR(100) NOT NULL,
        DeviceName NVARCHAR(100) NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        ExpiresAt DATETIME NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_UserSessions_Users
            FOREIGN KEY (UserID)
            REFERENCES dbo.Users (UserID)
    );
END
GO

-- =========================================
-- 6. Create ClinicInfo table if not exists
-- =========================================
IF NOT EXISTS (
    SELECT 1 FROM sys.objects
    WHERE object_id = OBJECT_ID(N'dbo.ClinicInfo')
      AND type = 'U'
)
BEGIN
    CREATE TABLE dbo.ClinicInfo (
        ClinicID INT NOT NULL PRIMARY KEY,
        ClinicName NVARCHAR(100) NULL,
        ClinicAddress NVARCHAR(200) NULL,
        ContactNumber NVARCHAR(50) NULL,
        Email NVARCHAR(100) NULL,
        OperatingHours NVARCHAR(100) NULL
    );
END
GO

PRINT 'All tables created / migrated successfully.';
GO
