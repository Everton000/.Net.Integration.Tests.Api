using Bogus;
using JornadaMilhas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Integration.Test.API.DataBuilders;

internal class RotaDataBuilder : Faker<Rota>
{
    public string? Origem { get; set; }
    public string? Destino { get; set; }

    public RotaDataBuilder()
    {
        CustomInstantiator(f =>
        {
            string origem = Origem ?? f.Lorem.Sentence(2);
            string destino = Destino ?? f.Lorem.Sentence(2);
            return new Rota(origem, destino);
        });
    }

    public Rota Build() => Generate();
}
