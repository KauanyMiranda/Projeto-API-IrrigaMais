CREATE DATABASE IF NOT EXISTS irriga_mais;

USE irriga_mais;

CREATE TABLE IF NOT EXISTS usuario(
	id_usuario INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	nome_usuario VARCHAR(255) NOT NULL,
	email_usuario VARCHAR(100) NOT NULL,
	senha_usuario VARCHAR(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS necessidade_hidrica(
	id_necessidade_hidrica INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(255) NOT NULL,
    qtd_litros DOUBLE NOT NULL
);

CREATE TABLE IF NOT EXISTS tipo_sensor(
	id_tipo_sensor INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(225) NOT NULL,
    unidade_medida VARCHAR(5)
);

CREATE TABLE IF NOT EXISTS dados_api(
	id_dados_api INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    cidade VARCHAR(50) NOT NULL,
    pais VARCHAR(50) NOT NULL,
    descricao VARCHAR(100),
    icone VARCHAR(100),
    temp DOUBLE NOT NULL,
    temp_max DOUBLE NOT NULL,
    temp_min DOUBLE NOT NULL,
    previsao DOUBLE NOT NULL,
    umidade DOUBLE NOT NULL,
    vento DOUBLE NOT NULL,
    dt_consulta DATETIME NOT NULL
);

CREATE TABLE IF NOT EXISTS rotina(
	id_rotina INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	nome_rotina VARCHAR(255) NOT NULL,
	tipo_execucao ENUM("Automático", "Inteligente"),
	horario VARCHAR(10) NOT NULL,
	frequencia INT NOT NULL,
	dia_seg BOOLEAN, 
	dia_ter BOOLEAN,
	dia_qua BOOLEAN,
	dia_qui BOOLEAN,
	dia_sex BOOLEAN,
	dia_sab BOOLEAN,
	dia_dom BOOLEAN,
    id_usuario_fk INT NOT NULL,
    FOREIGN KEY (id_usuario_fk) REFERENCES usuario(id_usuario)
);

CREATE TABLE IF NOT EXISTS relatorio(
	id_relatorio INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	tipo_relatorio VARCHAR(20) NOT NULL,
	dt_geracao DATETIME,
	dt_inicial DATETIME NOT NULL,
	dt_final DATETIME NOT NULL, 
    id_usuario_fk INT NOT NULL,
    FOREIGN KEY (id_usuario_fk) REFERENCES usuario(id_usuario)
);

CREATE TABLE IF NOT EXISTS sensor(
	id_sensor INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(255) NOT NULL,
    localizacao VARCHAR(255) NOT NULL,
    status_sensor ENUM('Ativo', 'Inativo'),
    id_tipo_sensor_fk INT NOT NULL,
    id_usuario_fk INT NOT NULL,
    FOREIGN KEY (id_tipo_sensor_fk) REFERENCES tipo_sensor(id_tipo_sensor),
    FOREIGN KEY (id_usuario_fk) REFERENCES usuario(id_usuario)
);


CREATE TABLE IF NOT EXISTS planta(
	id_planta INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome_planta VARCHAR(255) NOT NULL,
    id_necessidade_hidrica_fk INT NOT NULL,
    id_rotina_fk INT NOT NULL,
    id_usuario_fk INT NOT NULL,
    id_sensor_fk INT NOT NULL,
    FOREIGN KEY (id_necessidade_hidrica_fk) REFERENCES necessidade_hidrica(id_necessidade_hidrica),
	FOREIGN KEY (id_rotina_fk) REFERENCES rotina(id_rotina),
	FOREIGN KEY (id_sensor_fk) REFERENCES sensor(id_sensor),
    FOREIGN KEY (id_usuario_fk) REFERENCES usuario(id_usuario)
);

CREATE TABLE IF NOT EXISTS leitura_sensor(
	id_leitura_sensor INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    dt_leitura DATETIME NOT NULL,
    valor DOUBLE NOT NULL, 
    id_sensor_fk INT NOT NULL,
    FOREIGN KEY (id_sensor_fk) REFERENCES sensor(id_sensor)
); 

CREATE TABLE IF NOT EXISTS irrigacao(
	id_irrigacao INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    consumo_hidrico DOUBLE NOT NULL,
    dt_inicial DATETIME NOT NULL,
    dt_final DATETIME NOT NULL,
    id_leitura_sensor_fk INT NOT NULL,
    id_dados_api_fk INT NOT NULL,
    FOREIGN KEY (id_leitura_sensor_fk) REFERENCES leitura_sensor(id_leitura_sensor),
    FOREIGN KEY (id_dados_api_fk) REFERENCES dados_api(id_dados_api)
);

CREATE TABLE rotina_irrigacao(
	id_rotina_irrigacao INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	id_rotina_fk INT NOT NULL,
    id_irrigacao_fk INT NOT NULL,
    
    FOREIGN KEY (id_rotina_fk) REFERENCES rotina(id_rotina),
    FOREIGN KEY (id_irrigacao_fk) REFERENCES irrigacao(id_irrigacao)
);

INSERT INTO necessidade_hidrica(nome, qtd_litros) VALUES 
	("Muito Baixa", 0.5),
    ("Baixa", 1),
    ("Média", 1.5),
    ("Alta", 2),
    ("Muito Alta", 2.5);
    
INSERT INTO tipo_sensor(nome, unidade_medida) VALUES 
	("Umidade", "%"),
    ("Chuva", "%");

INSERT INTO usuario(nome_usuario, email_usuario, senha_usuario) VALUES
	("Kauany", "kauany@gmail.com", "123");

INSERT INTO sensor (nome, localizacao, status_sensor, id_tipo_sensor_fk, id_usuario_fk) VALUES 
	('Sensor 1', 'Setor A', 'Ativo', 1, 1),
    ('Sensor 2', 'Setor B', 'Ativo', 1, 1),
    ('Sensor 3', 'Setor C', 'Ativo', 1, 1);
