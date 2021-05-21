using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Read_and_learn.Helpers
{
    /// <summary>
    /// Helper to work with permissions.
    /// </summary>
    public class PermissionHelper
    {
        /// <summary>
        /// Check an request target permission.
        /// </summary>
        /// <param name="permission">Target permision</param>
        /// <returns>
        ///     <see cref="PermissionStatus"/>
        /// </returns>
        public static async Task<PermissionStatus> CheckAndRequestPermission(Permission permission)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (status != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (results.ContainsKey(permission))
                    status = results[permission];
            }

            return status;
        }
    }
}
