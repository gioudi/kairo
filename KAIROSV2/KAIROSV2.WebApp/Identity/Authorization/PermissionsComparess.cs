using KAIROSV2.Business.Entities;
using LightCore.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Identity.Authorization
{
    public static class PermissionsCompress
    {
        public static string CompressPermissionsIntoString(this IEnumerable<TURolesPermiso> permissions)
        {
            var result = string.Join(',', permissions.Select(e => e.IdPermiso.ToString()));
            return CompressUtil.Compress(result);
        }

        public static IEnumerable<int> DecompressPermissionsFromString(this string compressPermission)
        {
            if (compressPermission == null)
                throw new ArgumentNullException(nameof(compressPermission));
            var result = CompressUtil.Decompress(compressPermission);
            foreach (var permission in result.Split(','))
            {
                yield return (Convert.ToInt32(permission));
            }
        }
    }
}