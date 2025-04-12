-- Create database (if it doesn't already exist)
CREATE DATABASE physiotherapy_clinic;

-- Create table for Clinics
CREATE TABLE Clinics (
    ClinicId SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Address VARCHAR(255) NOT NULL,
    Neighborhood VARCHAR(100),
    City VARCHAR(100) NOT NULL,
    State VARCHAR(100),
    ZipCode VARCHAR(10)
);

-- Create table for Patients
CREATE TABLE Patients (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    BirthDate DATE NOT NULL,
    CPF VARCHAR(14) UNIQUE NOT NULL,
    Phone VARCHAR(20),
    Address TEXT,
    Neighborhood VARCHAR(100),
    City VARCHAR(100),
    State VARCHAR(100),
    ZipCode VARCHAR(10),
    GestationWeeks INT CHECK (GestationWeeks BETWEEN 20 AND 42), -- Common gestation range
    DeliveryType VARCHAR(10) CHECK (DeliveryType IN ('Normal', 'Cesárea')), -- Normal or Cesarean delivery
    WasPremature BOOLEAN GENERATED ALWAYS AS (GestationWeeks < 37) STORED, -- If weeks < 37, premature
    BirthHistory TEXT, -- Details about the birth
    MainComplaint TEXT, -- Patient's main complaint
    Observations TEXT -- Additional observations
);

-- Create table for Companions
CREATE TABLE Companions (
    CompanionId SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    CPF VARCHAR(14) NOT NULL UNIQUE,
    Address VARCHAR(255) NOT NULL,
    Phone VARCHAR(20),
    BirthDate DATE 
);

-- Create table for link between Patients and Companions
CREATE TABLE PatientCompanion (
    PatientId INT NOT NULL,
    CompanionId INT NOT NULL,
    PRIMARY KEY (PatientId, CompanionId),
    FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE,
    FOREIGN KEY (CompanionId) REFERENCES Companions(CompanionId) ON DELETE CASCADE
);

-- Create table for Physiotherapists
CREATE TABLE Physiotherapists (
    PhysiotherapistId SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    CPF VARCHAR(14) NOT NULL UNIQUE,
    Email VARCHAR(255) NOT NULL,
    Phone VARCHAR(15) NOT NULL
);

-- Create table for Physiotherapists and Clinics (associate physiotherapists to clinics)
CREATE TABLE PhysiotherapistClinic (
    PhysiotherapistId INT NOT NULL,
    ClinicId INT NOT NULL,
    FOREIGN KEY (PhysiotherapistId) REFERENCES Physiotherapists(PhysiotherapistId) ON DELETE CASCADE,
    FOREIGN KEY (ClinicId) REFERENCES Clinics(ClinicId) ON DELETE CASCADE,
    PRIMARY KEY (PhysiotherapistId, ClinicId)
);

-- Create table for Schedules (appointment times for each physiotherapist at each clinic)
CREATE TABLE Schedules (
    ScheduleId SERIAL PRIMARY KEY,
    PhysiotherapistId INT NOT NULL,
    ClinicId INT NOT NULL,
    Day DATE NOT NULL,
    Time TIME NOT NULL,
    Available BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (PhysiotherapistId) REFERENCES Physiotherapists(PhysiotherapistId) ON DELETE CASCADE,
    FOREIGN KEY (ClinicId) REFERENCES Clinics(ClinicId) ON DELETE CASCADE
);

-- Create table for Booked Schedules (confirmed appointments)
CREATE TABLE BookedSchedules (
    BookedScheduleId SERIAL PRIMARY KEY,
    PatientId INT NOT NULL,
    PhysiotherapistId INT NOT NULL,
    ClinicId INT NOT NULL,
    ScheduleId INT NOT NULL,
    DateTime TIMESTAMP NOT NULL,
    Observations TEXT,
    FOREIGN KEY (PatientId) REFERENCES Patients(Id) ON DELETE CASCADE,
    FOREIGN KEY (PhysiotherapistId) REFERENCES Physiotherapists(PhysiotherapistId) ON DELETE CASCADE,
    FOREIGN KEY (ClinicId) REFERENCES Clinics(ClinicId) ON DELETE CASCADE,
    FOREIGN KEY (ScheduleId) REFERENCES Schedules(ScheduleId) ON DELETE CASCADE
);

-- Create table for Users
CREATE TABLE Users (
    UserId SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL
);

-- Create indexes for performance
CREATE INDEX idx_clinics_city ON Clinics(City);
CREATE INDEX idx_physiotherapists_cpf ON Physiotherapists(CPF);
CREATE INDEX idx_schedules_physiotherapist_clinic ON Schedules(PhysiotherapistId, ClinicId);
CREATE INDEX idx_bookedschedules_physiotherapist_clinic_patient ON BookedSchedules(PhysiotherapistId, ClinicId, PatientId);
