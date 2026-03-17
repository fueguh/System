-- Step 1: Clear Junction and Dependent Tables (Child Tables)
DELETE FROM appointmentServices;
DELETE FROM TreatmentRecords;
DELETE FROM StockTransactions;
DELETE FROM UserSessions;
DELETE FROM AuditTrail;
DELETE FROM DentistAvailability;

-- Step 2: Clear Tables with Foreign Key dependencies (Intermediate)
DELETE FROM appointments;
DELETE FROM Receipts;
DELETE FROM ItemManagement;

-- Step 3: Clear Primary Data (Parent Tables)
DELETE FROM Patients;
DELETE FROM Services;
DELETE FROM Categories;
DELETE FROM Suppliers;
DELETE FROM users;