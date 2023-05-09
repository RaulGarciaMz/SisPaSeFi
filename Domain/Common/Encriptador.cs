using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Common
{
    public class Encriptador
    {
        public string Desencriptar(string texto)
        {
            string resultado = "";

            if (!string.IsNullOrEmpty(texto.Trim()))
            {
                var des = new TripleDESCryptoServiceProvider();   // Algoritmo TripleDES
                var hashmd5 = new MD5CryptoServiceProvider();     // objeto md5
                string myKey = "#SistemaPatrullajeSSF.2022!";    // Clave secreta

                des.Key = hashmd5.ComputeHash(new UnicodeEncoding().GetBytes(myKey));
                des.Mode = CipherMode.ECB;
                var desencrypta = des.CreateDecryptor();
                byte[] buff = Convert.FromBase64String(texto);
                resultado = Encoding.ASCII.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length));
            }

            return resultado;
        }

        public string ComputeMD5(string s)
        {
            using (MD5 md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(s)))
                            .Replace("-", "");
            }
        }

        public string GeneraCadenaClave(string[] datosClave)
        {
            var cve = "";
            for (var i = 0; i <= datosClave.Length - 3; i++)
            {
                cve = cve + datosClave[i];
            }

            return cve;
        }

        public DateTime GeneraFecha(string[] datosClave)
        {
            var strFecha = datosClave[datosClave.Length - 2];
            var strHora = datosClave[datosClave.Length - 1];

            var fechaHora = strFecha + " " + strHora;

            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(fechaHora, "yy-MM-dd HH:mm:ss", provider);
        }
    }
}
