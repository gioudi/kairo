using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Support.Util
{
    public static class IFormFileExtension
    {
        public static byte[] ToByteArray(this IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            else
                return null;
        }
    }
}
