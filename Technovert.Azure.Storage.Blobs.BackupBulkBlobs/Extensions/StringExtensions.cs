using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.Azure.Storage.Blobs.BackupBulkBlobs.Extensions
{
    internal static class StringExtensions
    {
        internal static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
