using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public class Validaciones
    {
        private static readonly Random random = new Random();
        private const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        //private const string SpecialCharacters = "!@#$%^&*()";

        public static void ActualizarVisibilidadEtiquetas(string contra, Label lblMayu, Label lblMin, Label lblNum)
        {
            bool mayuscula = false, minuscula = false, numero = false;

            foreach (char c in contra)
            {
                if (char.IsUpper(c))
                {
                    mayuscula = true;
                }
                else if (char.IsLower(c))
                {
                    minuscula = true;
                }
                else if (char.IsDigit(c))
                {
                    numero = true;
                }
            }

            lblMayu.Visible = !mayuscula;
            lblMin.Visible = !minuscula;
            lblNum.Visible = !numero;
        }

        public static string GenerarContraseñaAleatoria()
        {
            int longitud = 7; // Longitud mínima de la contraseña
            string caracteres = UpperCaseLetters + LowerCaseLetters + Numbers;

            // Asegurarse de que la contraseña cumpla con los criterios mínimos
            while (true)
            {
                StringBuilder contraseña = new StringBuilder();
                contraseña.Append(RandomCharacter(UpperCaseLetters));
                contraseña.Append(RandomCharacter(LowerCaseLetters));
                contraseña.Append(RandomCharacter(Numbers));

                // Generar caracteres aleatorios adicionales
                for (int i = 4; i < longitud; i++)
                {
                    contraseña.Append(RandomCharacter(caracteres));
                }

                // Mezclar los caracteres aleatoriamente
                contraseña = new StringBuilder(new string(contraseña.ToString().ToCharArray().OrderBy(c => random.Next()).ToArray()));

                // Comprobar si la contraseña cumple con los criterios
                if (ContraseñaCumpleCriterios(contraseña.ToString()))
                {
                    return contraseña.ToString();
                }
            }
        }

        private static char RandomCharacter(string caracteres)
        {
            int indice = random.Next(0, caracteres.Length);
            return caracteres[indice];
        }

        public static bool ContraseñaCumpleCriterios(string contraseña)
        {
            bool mayuscula = false, minuscula = false, numero = false;

            foreach (char c in contraseña)
            {
                if (char.IsUpper(c))
                {
                    mayuscula = true;
                }
                else if (char.IsLower(c))
                {
                    minuscula = true;
                }
                else if (char.IsDigit(c))
                {
                    numero = true;
                }
            }
            return mayuscula && minuscula && numero;
        }
        public static bool EsCorreoValido(string correo)
        {
            // Expresión regular para validar el formato del correo
            string patronCorreo = @"^[a-zA-Z0-9_.+-]+@(gmail\.com|yahoo\.com|hotmail\.com|ofi\.com|\w+\.(com|es|net|org)|.ofi)$";

            // Realizar la validación del formato utilizando la expresión regular
            bool formatoValido = System.Text.RegularExpressions.Regex.IsMatch(correo, patronCorreo);

            // Retorna el resultado de la validación de formato
            return formatoValido;
        }

        public static bool ValidarLabelsVerdes(Label lblNombreApellidos, Label lblINSS, Label lblCelular, Label lblCorreo)
        {
            return lblNombreApellidos.ForeColor == Color.Green
                && lblINSS.ForeColor == Color.Green
                && lblCelular.ForeColor == Color.Green
                && lblCorreo.ForeColor == Color.Green;
        }
    }
}
