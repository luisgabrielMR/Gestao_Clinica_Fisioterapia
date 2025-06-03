
-- ======================
-- CREATE TABLES
-- ======================

-- 1. Clinics
CREATE TABLE Clinics (
    clinic_id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    address TEXT NOT NULL,
    neighborhood VARCHAR(100),
    city VARCHAR(100) NOT NULL,
    state VARCHAR(100),
    zip_code VARCHAR(10),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 2. General People Table
CREATE TABLE People (
    person_id SERIAL PRIMARY KEY,
    full_name VARCHAR(255) NOT NULL,
    cpf VARCHAR(14) NOT NULL UNIQUE,
    birth_date DATE,
    phone VARCHAR(20),
    email VARCHAR(255),
    address TEXT,
    neighborhood VARCHAR(100),
    city VARCHAR(100),
    state VARCHAR(100),
    zip_code VARCHAR(10),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 3. Users Table (for authentication)
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(100) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 4. Specializations

-- Patients
CREATE TABLE Patients (
    patient_id SERIAL PRIMARY KEY,
    person_id INT NOT NULL UNIQUE,
    gestation_weeks INT CHECK (gestation_weeks BETWEEN 20 AND 42),
    delivery_type VARCHAR(10) CHECK (delivery_type IN ('Normal', 'Cesárea')),
    was_premature BOOLEAN GENERATED ALWAYS AS (gestation_weeks < 37) STORED,
    birth_history TEXT,
    main_complaint TEXT,
    observations TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (person_id) REFERENCES People(person_id) ON DELETE CASCADE
);

-- Companions
CREATE TABLE Companions (
    companion_id SERIAL PRIMARY KEY,
    person_id INT NOT NULL UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (person_id) REFERENCES People(person_id) ON DELETE CASCADE
);

-- Relationship Patient ↔ Companion (N:N)
CREATE TABLE PatientCompanion (
    patient_id INT NOT NULL,
    companion_id INT NOT NULL,
    PRIMARY KEY (patient_id, companion_id),
    FOREIGN KEY (patient_id) REFERENCES Patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (companion_id) REFERENCES Companions(companion_id) ON DELETE CASCADE
);

-- Physiotherapists
CREATE TABLE Physiotherapists (
    physiotherapist_id SERIAL PRIMARY KEY,
    person_id INT NOT NULL UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (person_id) REFERENCES People(person_id) ON DELETE CASCADE
);

-- 5. Relationship Physiotherapist ↔ Clinic (N:N)
CREATE TABLE PhysiotherapistClinic (
    physiotherapist_id INT NOT NULL,
    clinic_id INT NOT NULL,
    PRIMARY KEY (physiotherapist_id, clinic_id),
    FOREIGN KEY (physiotherapist_id) REFERENCES Physiotherapists(physiotherapist_id) ON DELETE CASCADE,
    FOREIGN KEY (clinic_id) REFERENCES Clinics(clinic_id) ON DELETE CASCADE
);

-- 6. Weekly Availability
CREATE TABLE WeeklyAvailability (
    availability_id SERIAL PRIMARY KEY,
    physiotherapist_id INT NOT NULL,
    clinic_id INT NOT NULL,
    weekday SMALLINT NOT NULL CHECK (weekday BETWEEN 0 AND 6), -- 0 = Sunday, 6 = Saturday
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (physiotherapist_id) REFERENCES Physiotherapists(physiotherapist_id) ON DELETE CASCADE,
    FOREIGN KEY (clinic_id) REFERENCES Clinics(clinic_id) ON DELETE CASCADE,
    CHECK (start_time < end_time)
);

-- 7. Appointments (Real bookings)
CREATE TABLE Appointments (
    appointment_id SERIAL PRIMARY KEY,
    patient_id INT NOT NULL,
    physiotherapist_id INT NOT NULL,
    clinic_id INT NOT NULL,
    appointment_datetime TIMESTAMP NOT NULL,
    observations TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (patient_id) REFERENCES Patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (physiotherapist_id) REFERENCES Physiotherapists(physiotherapist_id) ON DELETE CASCADE,
    FOREIGN KEY (clinic_id) REFERENCES Clinics(clinic_id) ON DELETE CASCADE
);

-- ======================
-- Indexes for performance
-- ======================

CREATE INDEX idx_people_cpf ON People(cpf);
CREATE INDEX idx_people_city ON People(city);
CREATE INDEX idx_clinics_city ON Clinics(city);
CREATE INDEX idx_availability_physio_weekday ON WeeklyAvailability(physiotherapist_id, weekday);
CREATE INDEX idx_appointments_datetime ON Appointments(appointment_datetime);
CREATE INDEX idx_appointments_physio_patient ON Appointments(physiotherapist_id, patient_id);
