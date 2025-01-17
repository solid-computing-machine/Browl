﻿using Browl.Service.MarketDataCollector.Domain.Enums;
using Browl.Service.MarketDataCollector.Domain.Resources.Address;
using Browl.Service.MarketDataCollector.Domain.Resources.Telephone;

namespace Browl.Service.MarketDataCollector.Domain.Resources.Customer;

public class CustomerViewResource : ICloneable
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }

    public Gender Sexo { get; set; }
    public ICollection<TelephoneViewResource> Telefones { get; set; }

    public string Documento { get; set; }
    public DateTime Criacao { get; set; }
    public DateTime? UltimaAtualizacao { get; set; }

    public AddressViewResource Endereco { get; set; }

    public object Clone()
    {
        var cliente = (CustomerViewResource)MemberwiseClone();
        cliente.Endereco = (AddressViewResource)cliente.Endereco.Clone();
        var telefones = new List<TelephoneViewResource>();
        cliente.Telefones.ToList().ForEach(p => telefones.Add((TelephoneViewResource)p.Clone()));
        cliente.Telefones = telefones;
        return cliente;
    }

    public CustomerViewResource CloneTipado()
    {
        return (CustomerViewResource)Clone();
    }
}