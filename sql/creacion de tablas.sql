DROP TABLE IF exists items cascade;
drop table if exists rangos cascade;
drop table if exists biomas cascade;
drop table if exists elementos cascade;
drop table if exists mg_rango cascade;
drop table if exists mg_bioma cascade;
drop table if exists mg_debilidades cascade;
drop table if exists elemento_monstro cascade;
DROP table if exists monstro_grande cascade;
drop table if exists categoria_monstro cascade;
drop table if exists imagen_monstro cascade;
drop table if exists usuario cascade;

create table usuario (
 idUsuario serial unique,
 nombreUsuario varchar(50),
 password varchar(50),
 primary key (idUsuario)
);

create TABLE categoria_monstro (
 id_tipo_monstro serial UNIQUE,
 tipo VARCHAR(20),
 PRIMARY KEY(id_tipo_monstro)
);

create table imagen_monstro (
 id_imagen serial unique,
 primary key (id_imagen),
 icon_url text,
 image_url text
);

create table monstro_grande(
id_monstroG serial UNIQUE,
nombre varchar(20),
vida int,
id_imagen int,
id_categoria int,
Primary key (id_monstroG),
CONSTRAINT fk_categoria 
	FOREIGN KEY (id_categoria) 
		REFERENCES categoria_monstro(id_tipo_monstro)
		on delete cascade
		on update cascade,
Constraint fk_imagen FOREIGN key (id_imagen)
	REFERENCES imagen_monstro(id_imagen)
	on delete cascade
	on update cascade
);

create table items (
 id_item serial UNIQUE,
 id_monstro int,
 nombre_item VARCHAR(50),
 descripcion_item VARCHAR(255),
 PRIMARY KEY (id_item),
 CONSTRAINT fk_items
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
id_rango int,
id_monstro int,
primary key (id_rango, id_monstro),
CONSTRAINT fk_idRango
FOREIGN key (id_rango) 
	REFERENCES rangos(id_rango)
		on delete cascade 
		on update cascade,
CONSTRAINT fk_idMonstro
FOREIGN key (id_monstro) references monstro_grande(id_monstroG)
	on delete cascade 
	on update cascade
);

create table mg_bioma(
id_bioma int,
id_monstro int,
PRIMARY KEY(id_bioma, id_monstro),
CONSTRAINT fk_idBioma
FOREIGN key (id_bioma) REFERENCES biomas(id_bioma)
	on delete cascade
	on update cascade,
Constraint fk_idMonstro
FOREIGN key (id_monstro) references monstro_grande(id_monstroG)
	on delete cascade
	on update cascade
);

create table mg_debilidades (
id_elemento int,
id_monstro int,
eficacia float,
PRIMARY KEY (id_elemento, id_monstro),
Constraint fk_idMElemento
FOREIGN key (id_elemento) REFERENCES elementos(id_elemento)
	on delete cascade 
	on update cascade,
Constraint fk_idMonstro
foreign key (id_monstro) REFERENCES monstro_grande(id_monstroG)
	on delete cascade
	on update cascade
);

create table elemento_monstro(
id_elemento int,
id_monstro int,
PRIMARY key (id_elemento, id_monstro),
Constraint fk_idElemento
FOREIGN key (id_elemento) REFERENCES elementos(id_elemento)
	on delete cascade
	on update cascade,
Constraint fk_idMonstro
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

insert into imagen_monstro (icon_url, image_url) values
(
'https://static.wikia.nocookie.net/monsterhunterespanol/images/4/42/MHW-Icono_Pukei-Pukei.png/revision/latest/scale-to-width-down/80?cb=20210823122942&path-prefix=es',
'https://static.wikia.nocookie.net/monsterhunterespanol/images/b/bf/MHW-Render_Pukei-Pukei.png/revision/latest/scale-to-width-down/1000?cb=20171119121532&path-prefix=es'
),
(
'https://static.wikia.nocookie.net/monsterhunterespanol/images/3/30/MHW-Icono_Tobi-Kadachi.png/revision/latest?cb=20210823124001&path-prefix=es', 
'https://static.wikia.nocookie.net/monsterhunterespanol/images/4/43/MHRise-Render_Tobi-Kadachi.png/revision/latest?cb=20210325182434&path-prefix=es');

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

insert into monstro_grande (nombre, vida, id_imagen ,id_categoria) values 
('Pukei-Pukei', 3481,1,8),
('Tobi-Hadachi', 2552,2,5);

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

insert into mg_debilidades(id_elemento, id_monstro, eficacia) values
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



