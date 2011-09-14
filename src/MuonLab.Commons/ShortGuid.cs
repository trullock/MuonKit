using System;

namespace MuonLab.Commons
{
    public struct ShortGuid
    {
        private readonly Guid guid;

        public ShortGuid(Guid guid)
        {
            this.guid = guid;
        }

        public static bool TryParse(string guid, out ShortGuid shortGuid)
        {
            Guid parsed;
            try
            {
                parsed = new Guid(Convert.FromBase64String(guid.Replace("_", "/").Replace("-", "+") + "=="));
            }
            catch
            {
                try
                {
                    parsed = new Guid(guid);
                }
                catch
                {
                    shortGuid = new ShortGuid();
                    return false;
                }
            }

            shortGuid = new ShortGuid(parsed);
            return true;
        }

        public override string ToString()
        {
            return Convert.ToBase64String(guid.ToByteArray())
                .Substring(0, 22)
                .Replace("/", "_")
                .Replace("+", "-");
        }

        public Guid ToGuid()
        {
            return this.guid;
        }

        public static implicit operator string(ShortGuid guid)
        {
            return guid.ToString();
        }

        public static implicit operator Guid(ShortGuid shortGuid)
        {
            return shortGuid.guid;
        }
    }
}