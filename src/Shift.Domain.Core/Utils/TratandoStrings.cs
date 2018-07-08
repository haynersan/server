#region usings

using System;
using System.Globalization;
using System.Linq;
using System.Text;

#endregion


namespace Shift.Domain.Core.Utils
{
    public static class  TratandoStrings
    {

        //Remove os espaços EXTRAS entre as palavras.
        //Exemplo: 
        // Entrada:     cachorro      e      gato   
        // Saída: cachorro e gato
        public static String RemoverEspacosEntrePalavras(String meuTexto)
        {
            meuTexto = meuTexto.Trim();

            while (meuTexto.Contains("  "))
            {
                meuTexto = meuTexto.Replace("  ", " ");
            }
            return meuTexto;
        }


        public static string RemoverAcentuacao(this string text)
        {
            return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

    }
}
