﻿using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Core.Repositories;

public interface IActionDefinitionRepository : IReadOnlyRepository<ActionDefinition, string>
{
}
