declare @idSubject nvarchar(max)
set @idSubject = '' --Modificar segun el usuario 

----------SEGURIDAD----------
insert into Seguridad.Modulos(Nombre,Activo) values ('Seguridad',1)

declare @idmodulo int
set @idmodulo = (select Id From Seguridad.Modulos where Nombre = 'Seguridad')

-- Opcion 1-- USUARIOS
insert into Seguridad.OpcionesModulo(IdModulo,Nombre,Activo) values(@idmodulo,'Usuarios',1)
--acciones--
declare @idOpcion int
set @idOpcion = (select Id From Seguridad.OpcionesModulo where Nombre = 'Usuarios')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion,1,1,1,GETDATE(),GETDATE(),'Consultar Usuarios','ConsultarUsuarios','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion,1,0,1,GETDATE(),GETDATE(),'Activar Usuario','ActivarUsuario','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion,1,0,1,GETDATE(),GETDATE(),'Desactivar Usuario','DesactivarUsuario','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion,1,0,1,GETDATE(),GETDATE(),'Administrar Roles Usuario','AdministrarRolesUsuario','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion,1,0,1,GETDATE(),GETDATE(),'Consultar Roles Usuario','ConsultarRolesUsuario','','')


-- Opcion 2--ROLES
insert into Seguridad.OpcionesModulo(IdModulo,Nombre,Activo) values(@idmodulo,'Roles',1)

--acciones--
declare @idOpcion2 int
set @idOpcion2 = (select Id From Seguridad.OpcionesModulo where Nombre = 'Roles')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,1,1,GETDATE(),GETDATE(),'Consultar Roles','ConsultarRoles','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Crear Rol','CrearRol','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Modificar Rol','ModificarRol','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Eliminar Rol','EliminarRol','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Acciones','ConsultarRecursosAccion','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Acciones','AdministrarRecursosAccion','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Accesos','ConsultarAccesos','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Accesos','AdministrarAccesos','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Usuarios','ConsultarRecursosUsuarios','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Usuarios','AdministrarRecursosUsuarios','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Rol','ConsultarRecursosRol','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Rol','AdministrarRecursosRol','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Grupos','ConsultarRecursosGrupo','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Grupos','AdministrarRecursosGrupo','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Delegación','ConsultarRecursosDelegacion','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Delegación','AdministrarRecursosDelegacion','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Obligación','ConsultarRecursosObligacion','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Obligación','AdministrarRecursosObligacion','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Rubro','ConsultarRecursosRubro','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Rubro','AdministrarRecursosRubro','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Formato','ConsultarRecursosFormato','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Formato','AdministrarRecursosFormato','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Tipo Estado','ConsultarRecursosTipoEstado','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Tipo Estado','AdministrarRecursosTipoEstado','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Operación Ingreso','ConsultarRecursosOperacionIngreso','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Operación Ingreso','AdministrarRecursosOperacionIngreso','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Operación Trámite','ConsultarRecursosOperacionTramite','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Operación Trámite','AdministrarRecursosOperacionTramite','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Oficina','ConsultarRecursosOficina','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Oficina','AdministrarRecursosOficina','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Tipo Movimiento','ConsultarRecursosTipoMovimiento','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Tipo Movimiento','AdministrarRecursosTipoMovimiento','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Consultar Recursos Configuracion Cobro','ConsultarRecursosConfiguracionCobro','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion2,1,0,1,GETDATE(),GETDATE(),'Administrar Recursos Configuracion Cobro','AdministrarRecursosConfiguracionCobro','','')

-- Opcion 3-- GRUPOS
insert into Seguridad.OpcionesModulo(IdModulo,Nombre,Activo) values(@idmodulo,'Grupos',1)

--acciones--
declare @idOpcion3 int
set @idOpcion3 = (select Id From Seguridad.OpcionesModulo where Nombre = 'Grupos')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion3,1,1,1,GETDATE(),GETDATE(),'Consultar Grupos','ConsultarGrupos','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion3,1,0,1,GETDATE(),GETDATE(),'Crear Grupo','CrearGrupo','','')

insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion3,1,0,1,GETDATE(),GETDATE(),'Modificar Grupo','ModificarGrupo','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion3,1,0,1,GETDATE(),GETDATE(),'Eliminar Grupo','EliminarGrupo','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion3,1,0,1,GETDATE(),GETDATE(),'Administrar Usuarios Grupo','AdministrarUsuariosGrupo','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion3,1,0,1,GETDATE(),GETDATE(),'Consultar Usuarios Grupo','ConsultarUsuariosGrupo','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion3,1,0,1,GETDATE(),GETDATE(),'Administrar Roles Grupo','AdministrarRolesGrupo','','')
insert into Seguridad.Acciones(IdOpcionModulo,Activo,EsPrincipal,EsVisible,FechaCreacion,FechaModificacion,Nombre,Ruta,IdUsuarioCreo,IdUsuarioModifico) values(@idOpcion3,1,0,1,GETDATE(),GETDATE(),'Consultar Roles Grupo','ConsultarRolesGrupo','','')
