/*
Navicat MySQL Data Transfer

Source Server         : MySql
Source Server Version : 50715
Source Host           : localhost:3307
Source Database       : cellmax

Target Server Type    : MYSQL
Target Server Version : 50715
File Encoding         : 65001

Date: 2016-12-08 23:34:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for articulos
-- ----------------------------
DROP TABLE IF EXISTS `articulos`;
CREATE TABLE `articulos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `descripcion` varchar(128) DEFAULT NULL,
  `precio_compra` decimal(10,0) NOT NULL,
  `precio_venta` decimal(10,0) NOT NULL,
  `categoria_id` int(11) DEFAULT NULL,
  `proveedor_id` int(11) DEFAULT NULL,
  `fecha_creacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fecha_actualizacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `moneda_id` int(11) DEFAULT NULL,
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_prov_idx` (`proveedor_id`),
  KEY `fk_cat_idx` (`categoria_id`),
  KEY `fk_moned_idx` (`moneda_id`),
  CONSTRAINT `fk_cat` FOREIGN KEY (`categoria_id`) REFERENCES `categorias` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_moned` FOREIGN KEY (`moneda_id`) REFERENCES `monedas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_prov` FOREIGN KEY (`proveedor_id`) REFERENCES `proveedores` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of articulos
-- ----------------------------
INSERT INTO `articulos` VALUES ('7', 'Blu studio c mini', '5 pulgadas', '124', '123', null, null, '2016-10-30 20:06:22', '2016-10-30 20:06:22', null);
INSERT INTO `articulos` VALUES ('8', 'Cristal protector', 'samsung 5pulg', '65', '120', null, null, '2016-11-09 14:02:12', '2016-11-09 14:02:12', null);
INSERT INTO `articulos` VALUES ('9', 'Micro SD 32GB', 'Memoria micro sd de 32gb', '250', '320', null, null, '2016-11-09 18:23:05', '2016-11-09 18:23:05', '1');
INSERT INTO `articulos` VALUES ('14', 'nada', 'nada', '2', '5', null, null, '2016-11-10 23:26:14', '2016-11-10 23:26:14', '1');
INSERT INTO `articulos` VALUES ('16', 'Cover Samsung', 'Cover samsun s3', '40', '100', null, null, '2016-11-12 15:51:13', '2016-11-12 15:51:13', '1');

-- ----------------------------
-- Table structure for caja
-- ----------------------------
DROP TABLE IF EXISTS `caja`;
CREATE TABLE `caja` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `hora` datetime DEFAULT CURRENT_TIMESTAMP,
  `concepto` tinytext,
  `descripcion` text,
  `entrada` decimal(10,0) DEFAULT NULL,
  `salida` decimal(10,0) DEFAULT NULL,
  `moneda_id` int(11) DEFAULT NULL COMMENT 'describe la moneda con la que se hizo la transaccion',
  `usuario_id` int(11) DEFAULT NULL COMMENT 'cajero que hizo la venta, se determina por el id del usuario actual',
  `venta_id` int(11) DEFAULT NULL,
  `reparacion_id` int(11) DEFAULT NULL,
  `recarga_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_moneda` (`moneda_id`),
  KEY `fk_usuario_idx` (`usuario_id`),
  KEY `fk_reparacion_caja_idx` (`reparacion_id`),
  KEY `fk_venta_caja_idx` (`venta_id`),
  KEY `fk_recarga_caja_idx` (`recarga_id`),
  CONSTRAINT `fk_moneda` FOREIGN KEY (`moneda_id`) REFERENCES `monedas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_recarga_caja` FOREIGN KEY (`recarga_id`) REFERENCES `recargas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_reparacion_caja` FOREIGN KEY (`reparacion_id`) REFERENCES `reparaciones` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_usuario_caja` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_venta_caja` FOREIGN KEY (`venta_id`) REFERENCES `ventas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of caja
-- ----------------------------
INSERT INTO `caja` VALUES ('3', '2016-11-09 13:16:29', 'Blu studio c mini', '5 pulgadas', '200', null, '1', null, '1', null, null);
INSERT INTO `caja` VALUES ('4', '2016-11-09 13:20:13', 'Blu studio c mini', '5 pulgadas', '140', null, '1', null, '2', null, null);
INSERT INTO `caja` VALUES ('5', '2016-11-09 13:22:40', 'Blu studio c mini', '5 pulgadas', '140', null, '1', null, '3', null, null);
INSERT INTO `caja` VALUES ('7', '2016-11-09 13:55:56', 'Blu studio c mini', '5 pulgadas', '566', null, '1', null, '5', null, null);
INSERT INTO `caja` VALUES ('8', '2016-11-09 13:57:09', 'Blu studio c mini', '5 pulgadas', '283', null, '1', null, '6', null, null);
INSERT INTO `caja` VALUES ('9', '2016-11-09 14:00:54', 'Blu studio c mini', '5 pulgadas', '141', null, '1', null, '7', null, null);
INSERT INTO `caja` VALUES ('10', '2016-11-09 14:05:27', 'Cristal protector', 'samsung 5pulg', '138', null, '1', null, '8', null, null);
INSERT INTO `caja` VALUES ('11', '2016-11-12 10:49:42', 'Cristal protector', 'samsung 5pulg', '406', null, '1', null, '9', null, null);
INSERT INTO `caja` VALUES ('14', '2016-11-12 15:58:35', 'Cover Samsung', 'Cover samsun s3', '230', null, '1', null, '12', null, null);
INSERT INTO `caja` VALUES ('15', '2016-11-13 16:27:54', 'Cristal protector', 'samsung 5pulg', '406', null, '1', null, '13', null, null);
INSERT INTO `caja` VALUES ('16', '2016-12-03 11:17:16', 'nada', 'nada', '11', null, '1', null, '14', null, null);
INSERT INTO `caja` VALUES ('17', '2016-12-08 23:14:51', 'Hardware', 'czxc', '2334', null, null, null, null, '1', null);

-- ----------------------------
-- Table structure for categorias
-- ----------------------------
DROP TABLE IF EXISTS `categorias`;
CREATE TABLE `categorias` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `fecha_creacion` datetime DEFAULT CURRENT_TIMESTAMP,
  `fecha_actualizacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of categorias
-- ----------------------------

-- ----------------------------
-- Table structure for inventario
-- ----------------------------
DROP TABLE IF EXISTS `inventario`;
CREATE TABLE `inventario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `articulo_id` int(11) DEFAULT NULL,
  `existencias` decimal(10,0) NOT NULL,
  `fecha_creacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fecha_actualizacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_inv_producto_idx` (`articulo_id`),
  CONSTRAINT `fk_inv_producto` FOREIGN KEY (`articulo_id`) REFERENCES `articulos` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of inventario
-- ----------------------------
INSERT INTO `inventario` VALUES ('9', '7', '0', '2016-10-30 20:18:48', '2016-10-30 20:18:48');
INSERT INTO `inventario` VALUES ('10', '8', '3', '2016-11-09 14:04:41', '2016-11-09 14:04:41');
INSERT INTO `inventario` VALUES ('14', '14', '1', '2016-11-10 23:48:00', '2016-11-10 23:48:00');
INSERT INTO `inventario` VALUES ('16', '16', '10', '2016-11-12 15:52:04', '2016-11-12 15:52:04');

-- ----------------------------
-- Table structure for monedas
-- ----------------------------
DROP TABLE IF EXISTS `monedas`;
CREATE TABLE `monedas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `simbolo` varchar(45) NOT NULL,
  `valor` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `nombre_UNIQUE` (`nombre`),
  UNIQUE KEY `simbolo_UNIQUE` (`simbolo`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of monedas
-- ----------------------------
INSERT INTO `monedas` VALUES ('1', 'cordoba', 'C$', null);

-- ----------------------------
-- Table structure for proveedores
-- ----------------------------
DROP TABLE IF EXISTS `proveedores`;
CREATE TABLE `proveedores` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `telefono` varchar(45) DEFAULT NULL,
  `direccion` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `fecha_creacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `fecha_actualizacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of proveedores
-- ----------------------------

-- ----------------------------
-- Table structure for recargas
-- ----------------------------
DROP TABLE IF EXISTS `recargas`;
CREATE TABLE `recargas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `saldo_id` int(11) NOT NULL,
  `valor` decimal(10,0) DEFAULT NULL,
  `fecha_venta` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `fk_saldo_idx` (`saldo_id`),
  CONSTRAINT `fk_saldo` FOREIGN KEY (`saldo_id`) REFERENCES `saldo_recargas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of recargas
-- ----------------------------
INSERT INTO `recargas` VALUES ('7', '2', '10', '2016-11-11 23:22:49');
INSERT INTO `recargas` VALUES ('8', '1', '0', '2016-11-11 23:47:38');
INSERT INTO `recargas` VALUES ('9', '1', '30', '2016-11-11 23:48:14');
INSERT INTO `recargas` VALUES ('10', '2', '50', '2016-11-12 11:04:47');
INSERT INTO `recargas` VALUES ('11', '2', '50', '2016-11-12 11:04:55');
INSERT INTO `recargas` VALUES ('12', '1', '20', '2016-11-12 16:02:58');
INSERT INTO `recargas` VALUES ('13', '2', '20', '2016-11-12 16:03:42');
INSERT INTO `recargas` VALUES ('14', '2', '20', '2016-11-12 16:05:04');
INSERT INTO `recargas` VALUES ('15', '2', '30', '2016-11-12 16:05:44');
INSERT INTO `recargas` VALUES ('16', '1', '20', '2016-11-27 16:43:18');
INSERT INTO `recargas` VALUES ('17', '1', '30', '2016-11-28 04:49:05');
INSERT INTO `recargas` VALUES ('18', '1', '10', '2016-11-28 04:53:08');
INSERT INTO `recargas` VALUES ('19', '1', '10', '2016-11-28 04:53:18');
INSERT INTO `recargas` VALUES ('20', '1', '10', '2016-11-28 04:53:19');
INSERT INTO `recargas` VALUES ('21', '1', '10', '2016-11-28 04:53:19');
INSERT INTO `recargas` VALUES ('22', '1', '10', '2016-11-28 04:53:20');
INSERT INTO `recargas` VALUES ('23', '1', '10', '2016-11-28 04:53:20');
INSERT INTO `recargas` VALUES ('24', '1', '10', '2016-11-28 04:53:20');
INSERT INTO `recargas` VALUES ('25', '1', '10', '2016-11-28 04:53:21');
INSERT INTO `recargas` VALUES ('26', '1', '10', '2016-11-28 04:53:21');
INSERT INTO `recargas` VALUES ('27', '1', '10', '2016-11-28 04:53:21');
INSERT INTO `recargas` VALUES ('28', '1', '10', '2016-11-28 04:53:21');
INSERT INTO `recargas` VALUES ('29', '1', '10', '2016-11-28 04:53:21');
INSERT INTO `recargas` VALUES ('31', '2', '20', '2016-11-28 22:42:20');
INSERT INTO `recargas` VALUES ('32', '1', '20', '2016-12-01 00:11:32');
INSERT INTO `recargas` VALUES ('33', '1', '0', '2016-12-01 00:11:36');
INSERT INTO `recargas` VALUES ('35', '2', '10', '2016-12-01 00:19:40');
INSERT INTO `recargas` VALUES ('36', '1', '50', '2016-12-01 22:51:58');

-- ----------------------------
-- Table structure for reparaciones
-- ----------------------------
DROP TABLE IF EXISTS `reparaciones`;
CREATE TABLE `reparaciones` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` varchar(45) DEFAULT NULL,
  `detalles` varchar(190) NOT NULL,
  `observaciones` varchar(125) DEFAULT '-',
  `precio_repuesto` decimal(10,2) DEFAULT NULL,
  `detalles_repuesto` varchar(95) DEFAULT NULL,
  `precio` decimal(10,2) NOT NULL,
  `fecha_creacion` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of reparaciones
-- ----------------------------
INSERT INTO `reparaciones` VALUES ('1', 'Hardware', 'czxc', 'asd', '1232.00', 'sdadsad', '2334.00', '2016-12-08 23:14:51');

-- ----------------------------
-- Table structure for saldo_recargas
-- ----------------------------
DROP TABLE IF EXISTS `saldo_recargas`;
CREATE TABLE `saldo_recargas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `compania` varchar(45) NOT NULL,
  `saldo` decimal(10,0) NOT NULL,
  `moneda_id` int(11) DEFAULT NULL,
  `ganancia` decimal(10,2) DEFAULT NULL,
  `fecha_recarga` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_moneda_venta_idx` (`moneda_id`),
  CONSTRAINT `fk_moneda_venta` FOREIGN KEY (`moneda_id`) REFERENCES `monedas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of saldo_recargas
-- ----------------------------
INSERT INTO `saldo_recargas` VALUES ('1', 'claro', '660', null, '2.00', null);
INSERT INTO `saldo_recargas` VALUES ('2', 'movistar', '442', null, '2.50', null);

-- ----------------------------
-- Table structure for usuarios
-- ----------------------------
DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_usuario` varchar(45) NOT NULL DEFAULT 'N/E',
  `nombre_completo` varchar(255) DEFAULT 'N/E',
  `puesto` varchar(255) DEFAULT 'N/E',
  `cedula` varchar(255) DEFAULT 'N/E',
  `contrasena` varchar(45) NOT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `lvl` int(11) DEFAULT '1',
  `fecha_ingreso` varchar(255) DEFAULT 'NOW()',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `nombre_usuario_UNIQUE` (`nombre_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of usuarios
-- ----------------------------
INSERT INTO `usuarios` VALUES ('1', 'luishck', 'Luis Centeno', 'Administrador', '421-270195-0003G', '123', 'Condega', '1', 'NOW()');

-- ----------------------------
-- Table structure for ventas
-- ----------------------------
DROP TABLE IF EXISTS `ventas`;
CREATE TABLE `ventas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `codigo_articulo` int(11) DEFAULT NULL,
  `cantidad` int(11) DEFAULT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  `moneda_id` int(11) DEFAULT NULL,
  `fecha_venta` datetime DEFAULT CURRENT_TIMESTAMP,
  `total` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_art_venta_idx` (`codigo_articulo`),
  KEY `fk_usuario` (`usuario_id`),
  KEY `fk_moneda_idx` (`moneda_id`),
  CONSTRAINT `fk_art_venta` FOREIGN KEY (`codigo_articulo`) REFERENCES `articulos` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_usuario` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ventas
-- ----------------------------
INSERT INTO `ventas` VALUES ('1', '7', '1', null, '1', '2016-11-09 13:16:29', '200');
INSERT INTO `ventas` VALUES ('2', '7', '1', null, '1', '2016-11-09 13:20:13', '140');
INSERT INTO `ventas` VALUES ('3', '7', '1', null, '1', '2016-11-09 13:22:40', '140');
INSERT INTO `ventas` VALUES ('5', '7', '4', null, '1', '2016-11-09 13:55:56', '566');
INSERT INTO `ventas` VALUES ('6', '7', '2', null, '1', '2016-11-09 13:57:09', '283');
INSERT INTO `ventas` VALUES ('7', '7', '1', null, '1', '2016-11-09 14:00:54', '141');
INSERT INTO `ventas` VALUES ('8', '8', '1', null, '1', '2016-11-09 14:05:27', '138');
INSERT INTO `ventas` VALUES ('9', '8', '3', null, '1', '2016-11-12 10:49:42', '406');
INSERT INTO `ventas` VALUES ('12', '16', '2', null, '1', '2016-11-12 15:58:35', '230');
INSERT INTO `ventas` VALUES ('13', '8', '3', null, '1', '2016-11-13 16:27:54', '406');
INSERT INTO `ventas` VALUES ('14', '14', '2', null, '1', '2016-12-03 11:17:16', '11');

-- ----------------------------
-- Procedure structure for agregar_al_inventario
-- ----------------------------
DROP PROCEDURE IF EXISTS `agregar_al_inventario`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `agregar_al_inventario`(in art int, in exs decimal)
BEGIN
	insert into inventario(articulo_id,existencias)
		values(art,exs);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for agregar_articulos
-- ----------------------------
DROP PROCEDURE IF EXISTS `agregar_articulos`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `agregar_articulos`(in cod varchar(45), in nom varchar(45), in descr varchar(128), in prec_comp decimal,
in prec_vnt decimal, in cat int, in  prov int)
BEGIN
	insert into articulos(codigo,nombre,descripcion,precio_compra,precio_venta,categoria_id,proveedor_id)
    values(cod,nom,descr,prec_comp,prec_vnt,cat,prov);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for agregar_categoria
-- ----------------------------
DROP PROCEDURE IF EXISTS `agregar_categoria`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `agregar_categoria`(in nom varchar(45))
BEGIN
	insert into categorias(nombre)
		values(nombre);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for agregar_moneda
-- ----------------------------
DROP PROCEDURE IF EXISTS `agregar_moneda`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `agregar_moneda`(in nom varchar(45),in sim varchar(45), in cam decimal)
BEGIN
	insert into monedas(nombre,simbolo,valor)
		values(nom,sim,cam);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for buscar_articulo
-- ----------------------------
DROP PROCEDURE IF EXISTS `buscar_articulo`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_articulo`(in art_id int)
BEGIN
SELECT articulos.id, articulos.nombre, articulos.descripcion, articulos.precio_venta, inventario.existencias, articulos.proveedor_id
	FROM cellmax.inventario 
		INNER JOIN articulos 
			where(inventario.articulo_id= art_id and articulos.id = art_id);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for comprar_saldo
-- ----------------------------
DROP PROCEDURE IF EXISTS `comprar_saldo`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `comprar_saldo`(in ID_SALDO int, in COMP varchar(45), in CANTIDAD decimal)
BEGIN
	SET SQL_SAFE_UPDATES = 0;
		UPDATE cellmax.saldo_recargas  SET saldo= (saldo + CANTIDAD)
		WHERE cellmax.saldo_recargas.id = ID_SALDO OR cellmax.saldo_recargas.compania = COMP;
		SET SQL_SAFE_UPDATES = 1;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for crear_articulos
-- ----------------------------
DROP PROCEDURE IF EXISTS `crear_articulos`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `crear_articulos`(in nom varchar(45), in descr varchar(128), in prec_comp decimal,
in prec_vnt decimal, in cat int, in  prov int, in moned int)
BEGIN
	insert into articulos(nombre,descripcion,precio_compra,precio_venta,categoria_id,proveedor_id,moneda_id)
    values(nom,descr,prec_comp,prec_vnt,cat,prov,moned);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for nueva_saldo_recargas
-- ----------------------------
DROP PROCEDURE IF EXISTS `nueva_saldo_recargas`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `nueva_saldo_recargas`(in COMPANY varchar(45), in SALD decimal, in MONED_ID int)
BEGIN
	INSERT into cellmax.saldo_recargas(compania, saldo, moneda_id)
    VALUES(COMPANY,SALD,MONED_ID);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for nuevo_proveedor
-- ----------------------------
DROP PROCEDURE IF EXISTS `nuevo_proveedor`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `nuevo_proveedor`(in nom varchar(45), in tel varchar(45),in dir varchar(45),in em varchar(45))
BEGIN 
	insert into proveedores(nombre,telefono,direccion,email)
		values(nom,tel,dir,em);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for registrar_en_caja
-- ----------------------------
DROP PROCEDURE IF EXISTS `registrar_en_caja`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `registrar_en_caja`(in concept TINYTEXT,in decrp TEXT,in entr DECIMAL,in salid DECIMAL,in moned INT,in usr INT,in art INT,in rep INT,in recr INT)
BEGIN
	INSERT INTO `cellmax`.`caja` (`concepto`, `descripcion`, `entrada`, `salida`, `moneda_id`, `usuario_id`, `articulo_id`, `reparacion_id`, `recarga_id`)
    VALUES(concept,decrp,entr,salid,moned,usr,art,rep,recr);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for vender_producto
-- ----------------------------
DROP PROCEDURE IF EXISTS `vender_producto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `vender_producto`(in ART int, in CANT int, in MONED int, in TOTAL_VNT decimal)
BEGIN
	INSERT INTO cellmax.ventas (codigo_articulo, cantidad, moneda_id, fecha_venta, total) 
    VALUES (ART, CANT, MONED, NOW(), TOTAL_VNT);

    /**ACTUALIZAR LAS EXISTENCIAS EN INVENTARIO**/
    update cellmax.inventario set existencias = inventario.existencias - CANT 
		where cellmax.inventario.articulo_id = ART;        

END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for vender_recarga
-- ----------------------------
DROP PROCEDURE IF EXISTS `vender_recarga`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `vender_recarga`(in _valor decimal, in _saldo_id int)
BEGIN
	INSERT INTO cellmax.recargas (saldo_id, valor) VALUES (_saldo_id, _valor);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for vender_reparacion
-- ----------------------------
DROP PROCEDURE IF EXISTS `vender_reparacion`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `vender_reparacion`(in tip varchar(45), in dtll varchar(125), in obsr varchar(125), in prec decimal)
BEGIN
	INSERT INTO `cellmax`.`reparaciones` (`tipo`, `detalles`, `observaciones`, `precio`) VALUES (tip, dtll, obsr, prec);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for ver_inventario
-- ----------------------------
DROP PROCEDURE IF EXISTS `ver_inventario`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ver_inventario`()
BEGIN
	SELECT inventario.id, articulos.nombre, inventario.existencias, 
    articulos.precio_compra, articulos.precio_venta, articulos.id as art_id
		FROM cellmax.articulos
			INNER JOIN cellmax.inventario ON cellmax.articulos.id = cellmax.inventario.articulo_id;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for ver_registro_ventas
-- ----------------------------
DROP PROCEDURE IF EXISTS `ver_registro_ventas`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ver_registro_ventas`()
BEGIN
SELECT ventas.id, codigo_articulo, articulos.nombre, articulos.precio_venta, ventas.cantidad,ventas.moneda_id, ventas.total, ventas.fecha_venta 
FROM cellmax.ventas
inner join cellmax.articulos on cellmax.ventas.codigo_articulo = articulos.id;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for login
-- ----------------------------
DROP FUNCTION IF EXISTS `login`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `login`(user varchar(10), pass varchar(10)) RETURNS bit(1)
BEGIN
	if EXISTS(select*from usuarios where nombre_usuario = user and contrasena = pass)
		then 
			RETURN 1;
		else
			RETURN 0;
		end if;
END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `recargas_AFTER_INSERT`;
DELIMITER ;;
CREATE TRIGGER `recargas_AFTER_INSERT` AFTER INSERT ON `recargas` FOR EACH ROW BEGIN
	update cellmax.saldo_recargas set saldo = saldo_recargas.saldo - new.valor where cellmax.saldo_recargas.id =
    new.saldo_id;
END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `reparaciones_AFTER_INSERT`;
DELIMITER ;;
CREATE TRIGGER `reparaciones_AFTER_INSERT` AFTER INSERT ON `reparaciones` FOR EACH ROW BEGIN
    /*INSERTAR LOS VALORES EN CAJA*/
    INSERT INTO cellmax.caja(concepto, descripcion, entrada, reparacion_id)
    VALUES(new.tipo,new.detalles,new.precio,new.id);
END
;;
DELIMITER ;
DROP TRIGGER IF EXISTS `ventas_AFTER_INSERT`;
DELIMITER ;;
CREATE TRIGGER `ventas_AFTER_INSERT` AFTER INSERT ON `ventas` FOR EACH ROW BEGIN
	DECLARE CONCEPT varchar(94);
    DECLARE DESCR varchar(94);
    DECLARE MONED int;
    
    select nombre, descripcion into CONCEPT, DESCR from cellmax.articulos
    where cellmax.articulos.id = new.codigo_articulo;
    
    /*INSERTAR LOS VALORES EN CAJA*/
    INSERT INTO cellmax.caja(concepto, descripcion, entrada, moneda_id, venta_id)
    VALUES(CONCEPT,DESCR,new.total,new.moneda_id,new.id);
END
;;
DELIMITER ;
