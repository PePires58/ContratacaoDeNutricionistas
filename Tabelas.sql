Create Database ContratacaoDeNutricionistas 

Use ContratacaoDeNutricionistas 


--Tudo em uma tabela:
/*
Create Table usuario (
	id_usuario				Int Identity(1,1) Primary Key,
	CPF_CNPJ				Varchar(14), 
	CRM						Int Null, 
	Nome					Varchar(50),
	Telefone				Varchar(12),
	Sexo					Char, -- F - FEMININO ou M - MASCULINO
	Avaliacao				Float Null, --Acho que deveria ser Numeric(2,2)
	Login					Varchar(20),
	senha					Varchar(8)
)
*/

--Forma individual - Inicio
Create Table paciente (
	id_paciente				Int Identity(1,1) Primary Key,
	CPF_CNPJ				Varchar(14),
	Nome					Varchar(50),
	Telefone				Varchar(12),
	Sexo					Char, -- F - FEMININO ou M - MASCULINO
	Login					Varchar(20),
	senha					Varchar(8)
)

Create Table nutricionista (
	id_nutri				Int Identity(1,1) Primary Key,
	CRM						Varchar(20),
	CPF_CNPJ				Varchar(14),
	Nome					Varchar(50),
	Telefone				Varchar(12),
	Sexo					Char, -- F - FEMININO ou M - MASCULINO
	Avaliacao				Float, --Acho que deveria ser Numeric(2,2)
	Login					Varchar(20),
	senha					Varchar(8)
)
--Fim

Create Table endereco (
	id_nutri				Int,
	Rua						Varchar(100),
	Numero					Int,
	Bairro					Varchar(50),
	CEP						Varchar(8),
	Constraint FK_nutricionista FOREIGN KEY (id_nutri) References nutricionista (id_nutri)
)

Create Table agenda (
	id_agenda				Int Identity(1,1) Primary Key,
	id_nutri				Int,
	dt_agendamento_inicio	Date,
	dt_agendamento_fim		Date,
	Preco					Float,
	Forma_pagamento			Varchar(100),
	Disponibilidade			Char -- S - SIM ou N - N„O
	Constraint FK_nutricionista Foreign Key (id_nutri) References nutricionista (id_nutri)
)

Create Table consulta (
	id_consulta				Int Identity(1,1) Primary Key,
	id_nutri				Int,
	id_paciente				Int,
	id_agenda				Int,
	Status					Varchar(50),
	Constraint FK_nutricionista Foreign Key (id_nutri) References nutricionista (id_nutri),
	Constraint FK_paciente Foreign Key (id_paciente) References paciente (id_paciente),
	Constraint FK_agenda Foreign Key (id_agenda) References agenda (id_agenda)
)

Create Table ficha (
	id_ficha				Int Identity(1,1) Primary Key,
	id_nutri				Int,
	id_paciente				Int,
	dt_cadastro				Date,
	Arquivo					Varbinary(max),
	Constraint FK_nutricionista Foreign Key (id_nutri) References nutricionista (id_nutri),
	Constraint FK_paciente Foreign Key (id_paciente) References paciente (id_paciente),
)










	
