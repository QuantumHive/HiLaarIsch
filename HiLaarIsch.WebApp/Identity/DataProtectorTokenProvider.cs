using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Owin.Security.DataProtection;

namespace HiLaarIsch.Identity
{
    public class DataProtectorTokenProvider
    {
        internal static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

        public DataProtectorTokenProvider(IDataProtector protector)
        {
            Protector = protector;
            TokenLifespan = TimeSpan.FromDays(3); //TODO config
        }

        public IDataProtector Protector { get; }
        public TimeSpan TokenLifespan { get; set; }

        public string Generate(string purpose, Guid userId)
        {
            var ms = new MemoryStream();
            using (var writer = new BinaryWriter(ms, DefaultEncoding, true))
            {
                writer.Write(DateTimeOffset.UtcNow.UtcTicks);
                writer.Write(Convert.ToString(userId, CultureInfo.InvariantCulture));
                writer.Write(purpose ?? "");
                string stamp = null;
                //TODO
                //if (manager.SupportsUserSecurityStamp)
                //{
                //    stamp = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture();
                //}
                writer.Write(stamp ?? "");
            }
            var protectedBytes = Protector.Protect(ms.ToArray());
            return Convert.ToBase64String(protectedBytes);
        }

        public bool Validate(string purpose, string token, Guid userId)
        {
            var unprotectedData = Protector.Unprotect(Convert.FromBase64String(token));
            using (var reader = new BinaryReader(new MemoryStream(unprotectedData), DefaultEncoding, true))
            {
                var creationTime = new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
                var expirationTime = creationTime + TokenLifespan;
                if (expirationTime < DateTimeOffset.UtcNow)
                {
                    return false;
                }

                var previousUserId = reader.ReadString();
                if (!String.Equals(previousUserId, Convert.ToString(userId, CultureInfo.InvariantCulture)))
                {
                    return false;
                }
                var purp = reader.ReadString();
                if (!String.Equals(purp, purpose))
                {
                    return false;
                }
                var stamp = reader.ReadString();
                if (reader.PeekChar() != -1)
                {
                    return false;
                }
                //TODO
                //if (manager.SupportsUserSecurityStamp)
                //{
                //    var expectedStamp = await manager.GetSecurityStampAsync(userId).WithCurrentCulture();
                //    return stamp == expectedStamp;
                //}
                return stamp == "";
            }
        }
    }
}