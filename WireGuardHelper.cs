using System;
using System.Text;
using NSec.Cryptography;

namespace SaikyoVpn.Core
{
    public static class WireGuardHelper
    {
        public static (string PrivateKey, string PublicKey) GenerateKeyPair()
        {
            // Create an X25519 key pair
            var algorithm = KeyAgreementAlgorithm.X25519;
            using var key = Key.Create(algorithm, new KeyCreationParameters { ExportPolicy = KeyExportPolicies.AllowPlaintextExport });

            var privateKey = Convert.ToBase64String(key.Export(KeyBlobFormat.RawPrivateKey));
            var publicKey = Convert.ToBase64String(key.PublicKey.Export(KeyBlobFormat.RawPublicKey));
            return (privateKey, publicKey);
        }

        public static string BuildConfig(
            string privateKey,
            string address = "10.0.0.2/24",
            string dns = "1.1.1.1",
            string serverPublicKey = "Fgwqj508NI2KWcak7NwMB5tHROaYclp9HuxOHqbVSzw=",
            string endpoint = "s1410234.smartape-vps.com:51820",
            string allowedIPs = "0.0.0.0/0")
        {
            var sb = new StringBuilder();
            sb.AppendLine("[Interface]");
            sb.AppendLine($"PrivateKey = {privateKey}");
            sb.AppendLine($"Address = {address}");
            sb.AppendLine($"DNS = {dns}");
            sb.AppendLine();
            sb.AppendLine("[Peer]");
            sb.AppendLine($"PublicKey = {serverPublicKey}");
            sb.AppendLine($"Endpoint = {endpoint}");
            sb.AppendLine($"AllowedIPs = {allowedIPs}");
            sb.AppendLine("PersistentKeepalive = 25");
            return sb.ToString();
        }
    }
}
