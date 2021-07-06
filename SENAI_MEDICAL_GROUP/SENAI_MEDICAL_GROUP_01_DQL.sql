-- DQL

-- Selecionar o banco de dados que ser� utilizado
USE MEDICAL_GROUP_TARDE
GO
-- Mostrar as informa��es em Consulta
SELECT * FROM Consulta
GO
-- Mostrar as informa��es em Paciente
SELECT * FROM Paciente
GO
-- Mostrar as informa��es em TipoUsuario
SELECT * FROM TipoUsuario;
GO
-- Mostrar as informa��es em Usuario
SELECT * FROM Usuario
GO
-- Mostrar as informa��es em Especialidade
SELECT * FROM Especialidade
GO
-- Mostrar as informa��es em Clinica
SELECT * FROM Clinica
GO
-- Mostrar as informa��es em Medico
SELECT * FROM Medico
GO
-- Mostrar as informa��es em Situacao
SELECT * FROM Situacao
GO

-- Mostra os dados dos usu�rio e mostrar os seus correspondentes tipos
SELECT IdUsuario, TipoUsuario, Email FROM Usuario
INNER JOIN TipoUsuario
ON Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario

-- Mostra os nomes dos m�dicos que trabalham na clinica
SELECT NomeMedico, NomeFantasia FROM Consulta
INNER JOIN Medico
ON Consulta.IdMedico = Medico.IdMedico
INNER JOIN Clinica
ON Medico.IdClinica = Clinica.IdClinica

-- Mostra os nomes dos pacientes, m�dicos a data da consulta, a especialidade do m�dico, situacao da consulta e o horario de abertura e fechamento da clinica, juntamente com o nome da clinica
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

-- Mostrar os nomes dos m�dicos e suas especialidades
SELECT NomeMedico, Especialidade FROM Medico
INNER JOIN Especialidade
ON Medico.IdEspecialidade = Especialidade.IdEspecialidade;

-- Busca um usu�rio atrav�s do seu e-mail e senha
SELECT TipoUsuario[Permissao], Email FROM Usuario
INNER JOIN TipoUsuario
ON Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario
WHERE Email = 'adm@adm.com' AND Senha = 'adm@adm';

-- Converte a data de nascimento para PT-BR
SELECT NomePaciente AS Nomes, FORMAT (DataNascimento, 'd', 'pt-br') AS DataNascimento FROM Paciente 

-- Mostra a idade de todos os pacientes atrav�s da sua data de nascimento
SELECT Paciente.NomePaciente, Paciente.DataNascimento,DATEDIFF(YEAR, Paciente.DataNascimento,GETDATE()) AS IdadeAtual FROM Paciente;

