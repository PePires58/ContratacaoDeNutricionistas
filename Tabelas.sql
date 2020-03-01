Create Database ContratacaoDeNutricionistas 

Use ContratacaoDeNutricionistas

/* 
EXCLUIR TABELAS:
 
Drop Table ficha_tb
Drop Table consulta_tb
Drop Table agenda_tb
Drop Table endereco_tb
Drop Table paciente_tb
Drop Table nutricionista_tb

*/

--Tudo em uma tabela:
/*
Create Table usuario_tb (
	id_usuario				Int Identity(1,1) Primary Key	Not Null, --Identificador de cada usuário(chave primaria).
	CPF_CNPJ				Varchar(14)						Not Null, --CNPJ OU CPF do usuario.						 
	CRM						Int Null						Null, CRM do nutricionista.
	Nome					Varchar(50)						Not Null, --Nome do usuário
	Telefone				Varchar(12)						Not Null, --Telefone para contato.
	tp_usuario				Char(1)							Not Null, -- F - FEMININO ou M - MASCULINO
	Avaliacao				Float Null					    Null, --Acho que deveria ser Numeric(2,2)
	Login					Varchar(20)						Not Null, --Para o acesso.
	senha					Varchar(8)						Not Null --Senha para o acesso.
)
*/

--Forma individual - Inicio
--Criação da tabela do Paciente
Create Table paciente_tb (
	id_paciente				Int Identity(1,1) Primary Key	Not Null, --Identificação única do paciente(chave primaria).
	CPF_CNPJ				Varchar(14)						Not Null, --CNPJ OU CPF do paciente.
	Nome					Varchar(50)						Not Null, --Nome do paciente.
	Telefone				Varchar(12)						Not Null, --Telefone para contato do paciente.
	Sexo					Char(1)							Not Null, -- F - FEMININO ou M - MASCULINO
	Login					Varchar(20)						Not Null, --Para o acesso.
	senha					Varchar(20)						Not Null --Senha para o acesso.
)

--Criação da tabela do Nutricionista
Create Table nutricionista_tb (
	id_nutri				Int Identity(1,1) Primary Key	Not Null, --Identificação única do nutricionista(chave primaria).
	CRM						Varchar(20)						Null, --CRM pode ou não ser obrigatório.
	CPF_CNPJ				Varchar(14)						Not Null, --CNPJ OU CPF do nutricionista.
	Nome					Varchar(50)						Not Null, --Nome do nutricionista.
	Telefone				Varchar(12)						Not Null, --Telefone para contato do nutricionista.
	Sexo					Char							Not Null, -- F - FEMININO ou M - MASCULINO
	Avaliacao				Float							Not Null, --Acho que deveria ser Numeric(2,2)
	Login					Varchar(20)						Not Null, --Para o acesso.
	senha					Varchar(8)						Not Null --Senha para o acesso.
)
--Fim

--Criação da tabela do Local de Trabalho
Create Table endereco_tb (
	id_endereco				Int Identity(1,1) Primary Key	Not Null, --Identificação do endereco(acho que não deveria ter)(chave primaria).
	id_nutri				Int								Not Null, --Id da(o) nutricionista.
	Rua						Varchar(100)					Not Null, --Nome da rua.
	Complemento				Varchar(5)						Null, --Se é casa, apto...
	Numero					Int								Not Null, --Numero da casa, apto..
	Bairro					Varchar(50)						Not Null, --Nome do bairro.
	CEP						Varchar(8)						Not Null, --numero do CEP.
	Constraint FK_id_nutri_nutricionista_tb FOREIGN KEY (id_nutri) References nutricionista_tb (id_nutri) --Chave estrangeira
)

--Criação databela de Agendamentos
Create Table agenda_tb (
	id_agenda				Int Identity(1,1) Primary Key	Not Null, --Identificar agenda do nutricionista(também acho que não deveria ter).(chave primaria)
	id_nutri				Int								Not Null, --Id da(o) nutricionista.
	id_endereco				Int								Not Null, --id do endereço do nutricionista.
	dt_inicio				Date							Not Null, --Data inicio do agendamento.
	dt_fim					Date							Not Null, --Data fim do agendamento.
	Preco					Float							Not Null, --Valores da consulta.
	Forma_pagamento			Int								Not Null, --Os tipos de pagamentos que o tal nutricionista aceita.
	Disponibilidade			Char(1)							Not Null, -- S - SIM ou N - NãO
	Constraint FK_id_nutri_nutricionista_tb2 Foreign Key (id_nutri) References nutricionista_tb (id_nutri), --Chave estrangeira
	Constraint FK_id_endereco_endereco_tb Foreign Key (id_endereco) References endereco_tb (id_endereco) --Chave estrangeira
)

--Criação da tabela de consultas
Create Table consulta_tb (
	id_consulta				Int Identity(1,1) Primary Key	Not Null, --Identificar consulta que está sendo feita(chave primaria).
	id_nutri				Int								Not Null, --Id da(o) nutricionista.
	id_paciente				Int								Not Null, --Id do paciente.
	id_agenda				Int								Not Null, --Id para indetificar a consulta do paciente e nutricionista. 
	Status					Int								Not Null, --Se a consulta foi realizada, cancelada...
	Constraint FK_id_nutri_nutricionista_tb3 Foreign Key (id_nutri) References nutricionista_tb (id_nutri), --Chave estrangeira
	Constraint FK_id_paciente_paciente_tb Foreign Key (id_paciente) References paciente_tb (id_paciente), --Chave estrangeira
	Constraint FK_id_agenda_agenda_tb Foreign Key (id_agenda) References agenda_tb (id_agenda) --Chave estrangeira
)

--Criação da tabela da ficha do paciente.
Create Table ficha_tb (
	id_ficha				Int Identity(1,1) Primary Key	Not Null, --Identificar ficha do paciente(chave primaria).
	id_nutri				Int								Not Null, --Id da(o) nutricionista.
	id_paciente				Int								Not Null, --Id do paciente.
	dt_cadastro				Date							Not Null, --data que foi realizada o cadastro.
	Arquivo					Varbinary(Max)					Not Null, --é a mesma coisa que BLOB. 
	Constraint FK_id_nutri_nutricionista_tb4 Foreign Key (id_nutri) References nutricionista_tb (id_nutri), --Chave estrangeira
	Constraint FK_id_paciente_paciente_tb2 Foreign Key (id_paciente) References paciente_tb (id_paciente) --Chave estrangeira
)










	
