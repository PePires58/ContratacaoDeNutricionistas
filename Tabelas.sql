--TABELA USUARIO
CREATE TABLE USUARIO_TB (
	ID_USUARIO				INT IDENTITY(1,1) PRIMARY KEY	NOT NULL, --IDENTIFICADOR DE CADA USU�RIO(CHAVE PRIMARIA).
	CPF						VARCHAR(14)						NOT NULL, --CPF DO USUARIO.						 
	CRN						INT     						NULL,	  --CRN DO NUTRICIONISTA.
	NOME					VARCHAR(50)						NOT NULL, --NOME DO USU�RIO
	TELEFONE				VARCHAR(15)						NOT NULL, --TELEFONE PARA CONTATO.
	TP_USUARIO				CHAR(1)                         NOT NULL, --0 - PACIENTE OU 1 - NUTRICIONISTA
	LOGIN					VARCHAR(20)						NOT NULL, --PARA O ACESSO.
	SENHA					VARCHAR(8)						NOT NULL  --SENHA PARA O ACESSO.
)

--TABELA ENDERE�O DO NUTRICIONISTA
CREATE TABLE ENDERECO_TB (
	ID_ENDERECO				INT IDENTITY(1,1) PRIMARY KEY	NOT NULL, --IDENTIFICA��O DO ENDERECO(ACHO QUE N�O DEVERIA TER)(CHAVE PRIMARIA).
	ID_USUARIO				INT								NOT NULL, --ID DA(O) USU�RIO.
	RUA						VARCHAR(100)					NOT NULL, --NOME DA RUA.
	COMPLEMENTO				VARCHAR(255)					NULL,     --SE � CASA, APTO...
	NUMERO					INT								NULL,	  --NUMERO DA CASA, APTO..
	BAIRRO					VARCHAR(50)						NOT NULL, --NOME DO BAIRRO.
	CIDADE					VARCHAR(30)						NOT NULL, --NOME DA CIDADE.
	ESTADO					VARCHAR(2)						NOT NULL, --SIGLA DO ESTADO.
	CEP						VARCHAR(9)						NOT NULL, --NUMERO DO CEP.
	CONSTRAINT FK_ID_USUARIO_USUARIO_TB1 FOREIGN KEY (ID_USUARIO) REFERENCES USUARIO_TB (ID_USUARIO), --CHAVE ESTRANGEIRA
)

--TABELA DE AGENDAMENTOS
CREATE TABLE AGENDA_TB (
	ID_AGENDA				INT IDENTITY(1,1) PRIMARY KEY	NOT NULL, --IDENTIFICAR AGENDA DO NUTRICIONISTA(TAMB�M ACHO QUE N�O DEVERIA TER).(CHAVE PRIMARIA)
	ID_USUARIO				INT								NOT NULL, --ID DA(O) NUTRICIONISTA.
	ID_ENDERECO				INT								NOT NULL, --ID DO ENDERE�O DO NUTRICIONISTA.
	DT_INICIO				DATETIME							NOT NULL, --DATA INICIO DO AGENDAMENTO.
	DT_FIM					DATETIME							NOT NULL, --DATA FIM DO AGENDAMENTO.
	AGENDA_STATUS			        CHAR(2) NOT NULL, --PODE OU N�O ESTAR ATIVA OU DEMAIS STATOS N�O PREVISTOS.
	CONSTRAINT FK_ID_USUARIO_USUARIO_TB2 FOREIGN KEY (ID_USUARIO) REFERENCES USUARIO_TB (ID_USUARIO), --CHAVE ESTRANGEIRA
	CONSTRAINT FK_ID_ENDERECO_ENDERECO_TB FOREIGN KEY (ID_ENDERECO) REFERENCES ENDERECO_TB (ID_ENDERECO) --CHAVE ESTRANGEIRA
)

--TABELA DO CONTRATANTE
CREATE TABLE CONTRATO_TB (
	ID_CONTRATO				INT IDENTITY(1,1) PRIMARY KEY	NOT NULL, -- IDENTIFICAR O CONTRATANTE
	ID_USUARIO				INT								NOT NULL, --ID DA(O) NUTRICIONISTA.
	ID_NUTRI				INT								NOT NULL, --IDENTIFICAR NUTRICIONISTA.
	RUA						VARCHAR(100)					NOT NULL, --NOME DA RUA.
	COMPLEMENTO				VARCHAR(255)					NULL,     --SE � CASA, APTO...
	NUMERO					INT								NULL, --NUMERO DA CASA, APTO..
	BAIRRO					VARCHAR(50)						NOT NULL, --NOME DO BAIRRO.
	CIDADE					VARCHAR(30)						NOT NULL, --NOME DA CIDADE.
	ESTADO					VARCHAR(2)						NOT NULL, --SIGLA DO ESTADO.
	CEP						VARCHAR(9)						NOT NULL, --NUMERO DO CEP.
	DT_INICIO				DATETIME							NOT NULL, --DATA INICIO DO AGENDAMENTO.
	DT_FIM					DATETIME							NOT NULL, --DATA FIM DO AGENDAMENTO.
	STATUS					CHAR(2)							NOT NULL, --SE A CONSULTA FOI REALIZADA, CANCELADA...
	CONSTRAINT FK_ID_USUARIO_USUARIO_TB3 FOREIGN KEY (ID_USUARIO) REFERENCES USUARIO_TB (ID_USUARIO), --CHAVE ESTRANGEIRA
)









	
