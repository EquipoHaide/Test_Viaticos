

--Asignar todos los permisos a un rol 
declare @idRol int
set @idRol = 1

insert into Seguridad.Accesos(Activo,FechaCaducidad,FechaCreacion,FechaModificacion,IdAccion,IdRol,IdUsuarioCreo,IdUsuarioModifico)
     select 1,'2022-12-31 23:59:59.0000000',GETDATE(),GETDATE(),ac.Id,@idRol,'','' from Seguridad.Acciones ac
	  where ac.Activo = 1 -- and ac.Id between 1273 and 1274 -- solo descomentar cuendo sea especifico

	  insert into Seguridad.RecursosAccion (IdUsuarioCreo,FechaCreacion,IdUsuarioModifico,FechaModificacion,IdRol,IdRecurso,EsLectura,EsEscritura,EsEjecucion)
     select '',GETDATE(),'',GETDATE(),1,ac.Id,1,1,1 from Seguridad.Acciones ac
	  where ac.Activo = 1 -- and ac.Id between 1273 and 1274 -- solo descomentar cuendo sea especifico



