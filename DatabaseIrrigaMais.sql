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

CREATE TABLE IF NOT EXISTS rotina(
	id_rotina INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	nome_rotina VARCHAR(255) NOT NULL,
	tipo_execucao ENUM("Automática", "Inteligente"),
	horario VARCHAR(10) NOT NULL,
	frequencia INT NOT NULL,
	dia_seg BOOLEAN, 
	dia_ter BOOLEAN,
	dia_qua BOOLEAN,
	dia_qui BOOLEAN,
	dia_sex BOOLEAN,
	dia_sab BOOLEAN,
	dia_dom BOOLEAN
);

CREATE TABLE IF NOT EXISTS relatorio(
	id_relatorio INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	tipo_relatorio VARCHAR(20) NOT NULL,
	dt_geracao DATETIME,
	dt_inicial DATETIME NOT NULL,
	dt_final DATETIME NOT NULL, 
    fk_usuario_id INT NOT NULL,
    FOREIGN KEY (fk_usuario_id) REFERENCES usuario(id_usuario)
);

CREATE TABLE IF NOT EXISTS sensor(
	id_sensor INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(255) NOT NULL,
    localizacao VARCHAR(255) NOT NULL,
    status_sensor ENUM('Ativo', 'Inativo'),
    fk_tipo_sensor_id INT NOT NULL,
    fk_usuario_id INT NOT NULL,
    FOREIGN KEY (fk_tipo_sensor_id) REFERENCES tipo_sensor(id_tipo_sensor),
    FOREIGN KEY (fk_usuario_id) REFERENCES usuario(id_usuario)
);

CREATE TABLE IF NOT EXISTS planta(
	id_planta INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome_planta VARCHAR(255) NOT NULL,
    fk_necessidade_hidrica_id INT NOT NULL,
    fk_rotina_id INT NOT NULL,
    fk_usuario_id INT NOT NULL,
    fk_sensor_id INT NOT NULL,
    FOREIGN KEY (fk_necessidade_hidrica_id) REFERENCES necessidade_hidrica(id_necessidade_hidrica),
	FOREIGN KEY (fk_rotina_id) REFERENCES rotina(id_rotina),
	FOREIGN KEY (fk_sensor_id) REFERENCES sensor(id_sensor),
    FOREIGN KEY (fk_usuario_id) REFERENCES usuario(id_usuario)
);

CREATE TABLE IF NOT EXISTS leitura_sensor(
	id_leitura_sensor INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    dt_leitura DATETIME NOT NULL,
    valor DOUBLE NOT NULL, 
    fk_sensor_id INT NOT NULL,
    FOREIGN KEY (fk_sensor_id) REFERENCES sensor(id_sensor)
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

INSERT INTO sensor (nome, localizacao, status_sensor, fk_tipo_sensor_id, fk_usuario_id) VALUES 
	('Sensor 1', 'Setor A', 'Ativo', 1, 1),
    ('Sensor 2', 'Setor B', 'Ativo', 1, 1),
    ('Sensor 3', 'Setor C', 'Ativo', 1, 1);
