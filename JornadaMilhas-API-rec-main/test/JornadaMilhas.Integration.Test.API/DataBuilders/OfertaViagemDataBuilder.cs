using Bogus;
using JornadaMilhas.Dominio.Entidades;
using JornadaMilhas.Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Integration.Test.API.DataBuilders;

internal class OfertaViagemDataBuilder : Faker<OfertaViagem>
{
    public Rota? Rota { get; set; }
    public Periodo? Periodo { get; set; }
    public double PrecoMinimo { get; set; } = 1;
    public double PrecoMaximo { get; set; } = 100.0;

    public OfertaViagemDataBuilder()
    {
        CustomInstantiator(f => {
            Periodo periodo = Periodo ?? new PeriodoDataBuilder().Build();
            Rota rota = Rota ?? new RotaDataBuilder().Build();
            double preco = f.Random.Double(PrecoMinimo, PrecoMaximo);
            return new OfertaViagem(rota, periodo, preco);
        });
    }
}

