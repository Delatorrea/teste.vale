namespace Domain.PostalCodeContext.Entities
{
    public record class Address(string cep, string uf, string cidade, string bairro, string logradouro);
}
