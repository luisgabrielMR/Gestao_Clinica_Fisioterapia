using System.Text.RegularExpressions;

namespace ClinicaFisioterapiaApi.Common.Validators;

public class CpfValidator : ICpfValidator
{
    public bool IsValid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = Regex.Replace(cpf, "[^0-9]", "");

        if (cpf.Length != 11 || Regex.IsMatch(cpf, @"^(\d)\1{10}$"))
            return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf[..9];
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCpf += digito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito += resto.ToString();
        return cpf.EndsWith(digito);
    }
}
