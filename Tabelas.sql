Create Database ContratacaoDeNutricionistas 

Use ContratacaoDeNutricionistas

/* 
EXCLUIR TABELAS:
 
Drop Table usuario_tb
Drop Table agenda_tb
Drop Table endereco_tb
Drop Table paciente_tb
Drop Table nutricionista_tb

*/

--Tabela usuario
Create Table usuario_tb (
	id_usuario				INT Identity(1,1) Primary Key	Not Null, --Identificador de cada usuário(chave primaria).
	CPF						VARCHAR(14)						Not Null, --CPF do usuario.						 
	CRN						INT Null						Null,	  --CRN do nutricionista.
	Nome					VARCHAR(50)						Not Null, --Nome do usuário
	Telefone				VARCHAR(12)						Not Null, --Telefone para contato.
	tp_usuario				INT								Not Null, --0 - Paciente ou 1 - Nutricionista
	Avaliacao				FLOAT						    Null,     --Acho que deveria ser Numeric(2,2)
	Login					VARCHAR(20)						Not Null, --Para o acesso.
	senha					VARCHAR(8)						Not Null  --Senha para o acesso.
)

--Tabela endereço do Nutricionista
Create Table endereco_tb (
	id_endereco				INT Identity(1,1) Primary Key	Not Null, --Identificação do endereco(acho que não deveria ter)(chave primaria).
	id_usuario				INT								Not Null, --Id da(o) usuário.
	Rua						VARCHAR(100)					Not Null, --Nome da rua.
	Complemento				VARCHAR(5)						Null,     --Se é casa, apto...
	Numero					INT								Not Null, --Numero da casa, apto..
	Bairro					VARCHAR(50)						Not Null, --Nome do bairro.
	Cidade					VARCHAR(30)						Not Null, --Nome da cidade.
	Estado					VARCHAR(2)						Not Null, --Sigla do estado.
	CEP						VARCHAR(8)						Not Null, --numero do CEP.
	Constraint FK_id_usuario_usuario_tb2 Foreign Key (id_usuario) References usuario_tb (id_usuario), --Chave estrangeira
)

--Tabela de Agendamentos
Create Table agenda_tb (
	id_agenda				INT Identity(1,1) Primary Key	Not Null, --Identificar agenda do nutricionista(também acho que não deveria ter).(chave primaria)
	id_usuario				INT								Not Null, --Id da(o) nutricionista.
	id_endereco				INT								Not Null, --id do endereço do nutricionista.
	dt_inicio				Date							Not Null, --Data inicio do agendamento.
	dt_fim					Date							Not Null, --Data fim do agendamento.
	Constraint FK_id_usuario_usuario_tb2 Foreign Key (id_usuario) References usuario_tb (id_usuario), --Chave estrangeira
	Constraint FK_id_endereco_endereco_tb Foreign Key (id_endereco) References endereco_tb (id_endereco) --Chave estrangeira
)













	
