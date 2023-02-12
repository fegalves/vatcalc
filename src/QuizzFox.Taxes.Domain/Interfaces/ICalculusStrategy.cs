﻿using QuizzFox.Taxes.Domain.Models;

namespace QuizzFox.Taxes.Domain.Interfaces;

internal interface ICalculusStrategy
{
    public VatCalculationTypes CalculationType { get; }

    CalculationResult CalculateVat(VatCalculationDetails details);
}