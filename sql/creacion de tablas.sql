DROP TABLE IF exists items cascade;
drop table if exists rangos cascade;
drop table if exists biomas cascade;
drop table if exists elementos cascade;
drop table if exists mg_rango cascade;
drop table if exists mg_bioma cascade;
drop table if exists debilidades cascade;
drop table if exists elemento_monstro cascade;
DROP table if exists monstro_grande cascade;
drop table if exists categoria_monstro cascade;


create TABLE categoria_monstro (
 id_tipo_monstro serial UNIQUE,
 tipo VARCHAR(20),
 PRIMARY KEY(id_tipo_monstro)
);

create table monstro_grande(
id_monstroG serial UNIQUE,
nombre varchar(20),
vida int,
id_categoria int,
CONSTRAINT fk_categoria 
	FOREIGN KEY (id_categoria) 
		REFERENCES categoria_monstro(id_tipo_monstro)
		on delete cascade
		on update cascade 
);

create table items (
 id_item serial UNIQUE,
 id_monstro int,
 nombre_item VARCHAR(50),
 descripcion_item VARCHAR(255),
 PRIMARY KEY (id_item),
 	FOREIGN KEY (id_monstro) 
 		REFERENCES monstro_grande(id_monstroG)
		 on delete cascade
		 on update cascade
);

create table rangos(
id_rango serial unique,
rango varchar(20),
primary key (id_rango)
);

create table biomas(
id_bioma serial unique,
nombre_bioma varchar(50),
primary key (id_bioma)
);

create table elementos (
id_elemento serial unique,
elemento varchar(20),
primary key (id_elemento)
);

create table mg_rango(
id serial unique primary key,
id_rango int,
FOREIGN key (id_rango) 
	REFERENCES rangos(id_rango)
		on delete cascade 
		on update cascade,
id_monstro int,
FOREIGN key (id_monstro) references monstro_grande(id_monstroG)
	on delete cascade 
	on update cascade
);

create table mg_bioma(
id serial unique primary key,
id_bioma int,
FOREIGN key (id_bioma) REFERENCES biomas(id_bioma)
	on delete cascade
	on update cascade,
id_monstro int,
FOREIGN key (id_monstro) references monstro_grande(id_monstroG)
	on delete cascade
	on update cascade
);

create table debilidades (
id serial unique primary key,
id_elemento int,
id_monstro int,
eficacia float,
FOREIGN key (id_elemento) REFERENCES elementos(id_elemento)
	on delete cascade 
	on update cascade,
foreign key (id_monstro) REFERENCES monstro_grande(id_monstroG)
	on delete cascade
	on update cascade
);

create table elemento_monstro(
id serial unique primary key,
id_elemento int,
id_monstro int,
FOREIGN key (id_elemento) REFERENCES elementos(id_elemento)
	on delete cascade
	on update cascade,
foreign key (id_monstro) REFERENCES monstro_grande(id_monstroG)
	on delete cascade
	on update cascade
);
--INSERCION DE DATOS PRIMITIVOS
insert into categoria_monstro (tipo) values
('monstro pequeno'),
('Wyvern bruto'),
('Wyvern volador'),
('Wyvern nadador'),
('Wyvern de colmillo'),
('Bestia de colmillo'),
('Dragon anciano'),
('Wyvern pajaro');

insert into biomas (nombre_bioma) values 
('Bosque primigenio'),
('Yermo de agujas'),
('Altiplano coralinos'),
('Valle putrefacto'),
('Lecho de los ancianos'),
('Arroyo de escarcha');

insert into elementos (elemento) values 
('Fuego'),
('Agua'),
('Treuno'),
('Hielo'),
('Dragon'),
('Ninguno');

insert into rangos(rango) values
('Rango bajo'),
('Rango alto'),
('Rango maestro');


--insercion de datos de prueba

insert into monstro_grande (nombre, vida, id_categoria) values 
('Pukei-Pukei', 3481, 8),
('Tobi-Hadachi', 2552, 5);

insert into mg_rango (id_rango, id_monstro) values 
(1, 2),
(2, 2),
(3, 2),
(1, 1),
(2, 1),
(3, 1);

insert into mg_bioma (id_bioma,id_monstro) values 
(1, 1),
(2, 1),
(1, 2);

insert into debilidades(id_elemento, id_monstro, eficacia) values
(3,1,3.00),
(1,1,2.00),
(4,1,2.00),
(2,2,3),
(1,2,2),
(4,2,2);

INSERT into elemento_monstro(id_elemento,id_monstro)values
(3,2),
(6,1);

insert into items (nombre_item,descripcion_item,id_monstro) values
('itemEjemplo', 'descripcionItem', 1),
('itemEjemplo', 'descripcionItem', 1),
('itemEjemplo', 'descripcionItem', 1),
('itemEjemplo', 'descripcionItem', 1),
('itemEjemplo', 'descripcionItem', 1),
('itemEjemplo', 'descripcionItem', 1),
('itemEjemplo', 'descripcionItem', 1),
('itemEjemplo', 'descripcionItem', 2),
('itemEjemplo', 'descripcionItem', 2),
('itemEjemplo', 'descripcionItem', 2),
('itemEjemplo', 'descripcionItem', 2),
('itemEjemplo', 'descripcionItem', 2),
('itemEjemplo', 'descripcionItem', 2);


