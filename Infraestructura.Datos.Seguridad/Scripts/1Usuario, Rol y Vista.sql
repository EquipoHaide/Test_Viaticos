
--Crear Rol Admin
INSERT INTO [Seguridad].[Roles]
           ([IdUsuarioCreo]
           ,[FechaCreacion]
           ,[IdUsuarioModifico]
           ,[FechaModificacion]
           ,[Activo]
           ,[Nombre]
           ,[Descripcion])
     VALUES
           ('adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a'
           ,GETDATE()
           ,'adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a'
           ,GETDATE()
           ,1
           ,'Administrador'
           ,'Administrador')


--Primer Usuario
insert into [Seguridad].[Usuarios](SubjectId,Nombre,Apellidos,CorreoElectronicoLaboral,CorreoElectronicoPersonal,TelefonoPersonal,TelefonoLaboral,Extension,NombreUsuario,NumeroEmpleado,AreaAdscripcion,Dependencia,EsCliente,EsHabilitado,FechaCreacion,FechaModificacion,SesionesPermitidas)
				              values('adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a'
							         ,'Jose'
									 ,'Pacheco'
									 ,'jose.alex880@gmail.com'
									 ,'jose.alex880@gmail.com'
									 ,'9812221234'
									 ,'9818119200'
									 ,'27348'
									 ,'alex8800'
									 ,'31965'
									 ,'ireccion Informatica'
									 ,'Secretaria de Informatica'
									 ,1
									 ,1
									 ,GETDATE()
                                     ,GETDATE()
									 ,30)

INSERT INTO [Seguridad].[RolUsuarios]
           ([IdUsuarioCreo]
           ,[FechaCreacion]
           ,[IdUsuario]
           ,[IdRol])
     VALUES
           ('adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a'
           ,GETDATE()
           ,(select Id from [Seguridad].[Usuarios] where SubjectId = 'adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a')
           ,(select Id from Seguridad.Roles where Nombre = 'Administrador'))

INSERT INTO [Seguridad].[RolesDirectos]
           ([SubjectId]
           ,[IdRol])
     VALUES
           ('adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a'
           ,(select Id from Seguridad.Roles where Nombre = 'Administrador'))

INSERT INTO [Seguridad].[RolesParticulares]
([SubjectId]
,[IdRol])
     VALUES
           ('adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a'
           ,(select Id from Seguridad.Roles where Nombre = 'Administrador'))

INSERT INTO [Seguridad].[RolesUsuario]
([SubjectId]
,[IdRol])
     VALUES
           ('adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a'
           ,(select Id from Seguridad.Roles where Nombre = 'Administrador'))

INSERT INTO [Seguridad].[RecursosRol]
([IdUsuarioCreo]
,[FechaCreacion]
,[IdUsuarioModifico]
,[FechaModificacion]
,[IdRol]
,[IdRecurso]
,[EsLectura]
,[EsEscritura]
,[EsEjecucion])
     VALUES
           ('adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a',
           GETDATE(),
           'adef6c640a9ad1c111636fe67c6ad676bd706893b9bb955dd2ba028007e5f03a',
           GETDATE(),
           (select Id from Seguridad.Roles where Nombre = 'Administrador'),
           (select Id from Seguridad.Roles where Nombre = 'Administrador'),
           1,1,1)