namespace GestionApiTareas.Utilidades
{
    public class AuditInfo
    {
        public string user { get; set; }
        public string ip { get; set; }
    }

    public static class CRUD_Utilities
    {
        public static async Task<bool> DeleteAsync<T>(T entity, AuditInfo auditInfo, Func<Task> deleteAction) where T : class
        {
            if (entity is not null)
            {
                entity.GetType().GetProperty("Activo")?.SetValue(entity, false);
                entity.GetType().GetProperty("FechaEliminacion")?.SetValue(entity, DateTime.Now);
                entity.GetType().GetProperty("UsuarioEliminacion")?.SetValue(entity, auditInfo.user);
                entity.GetType().GetProperty("IpEliminacion")?.SetValue(entity, auditInfo.ip);
                entity.GetType().GetProperty("SistemaEliminacion")?.SetValue(entity, "Mantenimiento WEB");
                await deleteAction();
                return true;
            }
            return false;
        }

        public static async Task<bool> InsertOrUpdateAsync<T>(T entity, AuditInfo auditInfo, Func<T, Task> addEntityAction, Func<Task> saveChangesAction) where T : class, new()
        {
            if (entity == null)
            {
                // Crear una nueva entidad
                var newEntity = new T();
                newEntity.GetType().GetProperty("Activo")?.SetValue(newEntity, true);
                newEntity.GetType().GetProperty("FechaRegistro")?.SetValue(newEntity, DateTime.Now);
                newEntity.GetType().GetProperty("UsuarioRegistro")?.SetValue(newEntity, auditInfo.user);
                newEntity.GetType().GetProperty("IpRegistro")?.SetValue(newEntity, auditInfo.ip);
                newEntity.GetType().GetProperty("SistemaRegistro")?.SetValue(newEntity, "Mantenimiento WEB");
                await addEntityAction(newEntity);
                await saveChangesAction();
            }
            else
            {
                // Actualizar la entidad existente
                entity.GetType().GetProperty("Activo")?.SetValue(entity, true);
                entity.GetType().GetProperty("FechaModificacion")?.SetValue(entity, DateTime.Now);
                entity.GetType().GetProperty("UsuarioModificacion")?.SetValue(entity, auditInfo.user);
                entity.GetType().GetProperty("IpModificacion")?.SetValue(entity, auditInfo.ip);
                entity.GetType().GetProperty("SistemaModificacion")?.SetValue(entity, "Mantenimiento WEB");
                await saveChangesAction();
            }
            return true;
        }

        public static bool InsertAuditInfo<T>(T entity, AuditInfo auditInfo)
        {
            if (entity == null)
            {
                // Crear una nueva entidad
                entity.GetType().GetProperty("Activo")?.SetValue(entity, true);
                entity.GetType().GetProperty("FechaModificacion")?.SetValue(entity, DateTime.Now);
                entity.GetType().GetProperty("UsuarioModificacion")?.SetValue(entity, auditInfo.user);
                entity.GetType().GetProperty("IpModificacion")?.SetValue(entity, auditInfo.ip);
                entity.GetType().GetProperty("SistemaModificacion")?.SetValue(entity, "Mantenimiento WEB");
                return true;
            }
            return false;
        }
    }
}