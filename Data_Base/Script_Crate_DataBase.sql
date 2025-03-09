-- Criar o banco de dados (caso ainda não exista)
CREATE DATABASE clinica_fisioterapia;

-- Criar tabela de Clínicas
CREATE TABLE Clinicas (
    ClinicaId SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Endereco VARCHAR(255) NOT NULL,
    Bairro VARCHAR(100),
    Cidade VARCHAR(100) NOT NULL,
    Estado VARCHAR(100),
    Cep VARCHAR(10)
);

-- Criar tabela de Pacientes
CREATE TABLE Pacientes (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    data_nascimento DATE NOT NULL,
    cpf VARCHAR(14) UNIQUE NOT NULL,
    telefone VARCHAR(20),
    endereco TEXT,
    bairro VARCHAR(100),
    cidade VARCHAR(100),
    estado VARCHAR(100),
    cep VARCHAR(10),
    semanas_gestacao INT CHECK (semanas_gestacao BETWEEN 20 AND 42), -- Faixa comum de gestação
    tipo_parto VARCHAR(10) CHECK (tipo_parto IN ('Normal', 'Cesárea')), -- Parto Normal ou Cesárea
    foi_prematuro BOOLEAN GENERATED ALWAYS AS (semanas_gestacao < 37) STORED, -- Se semanas < 37, é prematuro
    historico_parto TEXT, -- Detalhes sobre o parto
    principal_queixa TEXT, -- Queixa principal do paciente
    observacoes TEXT -- Observações adicionais
);

-- Criar tabela de Acompanhantes
CREATE TABLE Acompanhantes (
    AcompanhanteId SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    CPF VARCHAR(14) NOT NULL UNIQUE,
    Endereco VARCHAR(255) NOT NULL,
    Telefone VARCHAR(20),
    DataNascimento DATE 
);

-- Criar tabela de ligação entre Pacientes e Acompanhantes
CREATE TABLE PacienteAcompanhante (
    PacienteId INT NOT NULL,
    AcompanhanteId INT NOT NULL,
    PRIMARY KEY (PacienteId, AcompanhanteId),
    FOREIGN KEY (PacienteId) REFERENCES Pacientes(id) ON DELETE CASCADE,
    FOREIGN KEY (AcompanhanteId) REFERENCES Acompanhantes(AcompanhanteId) ON DELETE CASCADE
);

-- Criar tabela de Fisioterapeutas
CREATE TABLE Fisioterapeutas (
    FisioterapeutaId SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    CPF VARCHAR(14) NOT NULL UNIQUE,
    Email VARCHAR(255) NOT NULL,
    Telefone VARCHAR(15) NOT NULL
);

-- Criar tabela de Fisioterapeutas e Clínicas (associar fisioterapeutas a clínicas)
CREATE TABLE FisioterapeutaClinica (
    FisioterapeutaId INT NOT NULL,
    ClinicaId INT NOT NULL,
    FOREIGN KEY (FisioterapeutaId) REFERENCES Fisioterapeutas(FisioterapeutaId) ON DELETE CASCADE,
    FOREIGN KEY (ClinicaId) REFERENCES Clinicas(ClinicaId) ON DELETE CASCADE,
    PRIMARY KEY (FisioterapeutaId, ClinicaId)
);

-- Criar tabela de Horários (agendamento de horários para cada fisioterapeuta em cada clínica)
CREATE TABLE Horarios (
    HorarioId SERIAL PRIMARY KEY,
    FisioterapeutaId INT NOT NULL,
    ClinicaId INT NOT NULL,
    Dia DATE NOT NULL,
    Hora TIME NOT NULL,
    Disponivel BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (FisioterapeutaId) REFERENCES Fisioterapeutas(FisioterapeutaId) ON DELETE CASCADE,
    FOREIGN KEY (ClinicaId) REFERENCES Clinicas(ClinicaId) ON DELETE CASCADE
);

-- Criar tabela de Horários Marcados (agendamentos realizados)
CREATE TABLE HorariosMarcados (
    HorarioMarcadoId SERIAL PRIMARY KEY,
    PacienteId INT NOT NULL,
    FisioterapeutaId INT NOT NULL,
    ClinicaId INT NOT NULL,
    HorarioId INT NOT NULL,
    DataHora TIMESTAMP NOT NULL,
    Observacoes TEXT,
    FOREIGN KEY (PacienteId) REFERENCES Pacientes(id) ON DELETE CASCADE,
    FOREIGN KEY (FisioterapeutaId) REFERENCES Fisioterapeutas(FisioterapeutaId) ON DELETE CASCADE,
    FOREIGN KEY (ClinicaId) REFERENCES Clinicas(ClinicaId) ON DELETE CASCADE,
    FOREIGN KEY (HorarioId) REFERENCES Horarios(HorarioId) ON DELETE CASCADE
);
-- Criar tabela de Usuários
CREATE TABLE Usuarios (
    UsuarioId SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Senha VARCHAR(255) NOT NULL
);

-- Criar índices para performance
CREATE INDEX idx_clinicas_cidade ON Clinicas(Cidade);
CREATE INDEX idx_fisioterapeutas_cpf ON Fisioterapeutas(CPF);
CREATE INDEX idx_horarios_fisioterapeuta_clinica ON Horarios(FisioterapeutaId, ClinicaId);
CREATE INDEX idx_horarios_marcados_fisioterapeuta_clinica_paciente ON HorariosMarcados(FisioterapeutaId, ClinicaId, PacienteId);
