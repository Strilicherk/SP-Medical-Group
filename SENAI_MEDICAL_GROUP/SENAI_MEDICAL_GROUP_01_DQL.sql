-- DQL

-- Selecionar o banco de dados que será utilizado
USE MEDICAL_GROUP_TARDE
GO
-- Mostrar as informações em Consulta
SELECT * FROM Consulta
GO
-- Mostrar as informações em Paciente
SELECT * FROM Paciente
GO
-- Mostrar as informações em TipoUsuario
SELECT * FROM TipoUsuario;
GO
-- Mostrar as informações em Usuario
SELECT * FROM Usuario
GO
-- Mostrar as informações em Especialidade
SELECT * FROM Especialidade
GO
-- Mostrar as informações em Clinica
SELECT * FROM Clinica
GO
-- Mostrar as informações em Medico
SELECT * FROM Medico
GO
-- Mostrar as informações em Situacao
SELECT * FROM Situacao
GO

-- Mostra os dados dos usuário e mostrar os seus correspondentes tipos
SELECT IdUsuario, TipoUsuario, Email FROM Usuario
INNER JOIN TipoUsuario
ON Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario

-- Mostra os nomes dos médicos que trabalham na clinica
SELECT NomeMedico, NomeFantasia FROM Consulta
INNER JOIN Medico
ON Consulta.IdMedico = Medico.IdMedico
INNER JOIN Clinica
ON Medico.IdClinica = Clinica.IdClinica

-- Mostra os nomes dos pacientes, médicos a data da consulta, a especialidade do médico, situacao da consulta e o horario de abertura e fechamento da clinica, juntamente com o nome da clinica
SELECT NomePaciente, NomeMedico, DataConsulta, Especialidade AS Especialidade, Situacao, HorarioAbertura [Horario Abertura Clinica], HorarioFechamento[Horario Fechamento Clinica], NomeFantasia[Nome Clinica] FROM Consulta
INNER JOIN Medico
ON Consulta.IdMedico = Medico.IdMedico
INNER JOIN Paciente
ON Consulta.IdPaciente = Paciente.IdPaciente
INNER JOIN Especialidade
ON Medico.IdEspecialidade = Especialidade.IdEspecialidade
INNER JOIN Situacao
ON Consulta.IdSituacao = Situacao.IdSituacao
INNER JOIN Clinica
ON Medico.IdClinica = Clinica.IdClinica

-- Mostrar os nomes dos médicos e suas especialidades
SELECT NomeMedico, Especialidade FROM Medico
INNER JOIN Especialidade
ON Medico.IdEspecialidade = Especialidade.IdEspecialidade;

-- Busca um usuário através do seu e-mail e senha
SELECT TipoUsuario[Permissao], Email FROM Usuario
INNER JOIN TipoUsuario
ON Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario
WHERE Email = 'adm@adm.com' AND Senha = 'adm@adm';

-- Converte a data de nascimento para PT-BR
SELECT NomePaciente AS Nomes, FORMAT (DataNascimento, 'd', 'pt-br') AS DataNascimento FROM Paciente 

-- Mostra a idade de todos os pacientes através da sua data de nascimento
SELECT Paciente.NomePaciente, Paciente.DataNascimento,DATEDIFF(YEAR, Paciente.DataNascimento,GETDATE()) AS IdadeAtual FROM Paciente;

