﻿using Crolow.Cms.Core.Models.Schemas;
using Microsoft.Extensions.Logging;
using NPoco;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Crolow.Cms.Core.Startup.Migrations;

public class CrolowOrdersFormComposer : ComponentComposer<CrolowOrdersComponent>
{
}

public class CrolowOrdersComponent : IComponent
{
    private readonly ICoreScopeProvider _coreScopeProvider;
    private readonly IMigrationPlanExecutor _migrationPlanExecutor;
    private readonly IKeyValueService _keyValueService;
    private readonly IRuntimeState _runtimeState;

    public CrolowOrdersComponent(
        ICoreScopeProvider coreScopeProvider,
        IMigrationPlanExecutor migrationPlanExecutor,
        IKeyValueService keyValueService,
        IRuntimeState runtimeState)
    {
        _coreScopeProvider = coreScopeProvider;
        _migrationPlanExecutor = migrationPlanExecutor;
        _keyValueService = keyValueService;
        _runtimeState = runtimeState;
    }

    public void Initialize()
    {
        if (_runtimeState.Level < RuntimeLevel.Run)
        {
            return;
        }

        // Create a migration plan for a specific project/feature
        // We can then track that latest migration state/step for this project/feature
        var migrationPlan = new MigrationPlan("CrolowOrders");

        // This is the steps we need to take
        // Each step in the migration adds a unique value
        migrationPlan.From(string.Empty)
            .To<CrolowOrdersTable>("crowloworders-db");

        // Go and upgrade our site (Will check if it needs to do the work or not)
        // Based on the current/latest step
        var upgrader = new Upgrader(migrationPlan);
        upgrader.Execute(_migrationPlanExecutor, _coreScopeProvider, _keyValueService);
    }

    public void Terminate()
    {
    }
}

public class CrolowOrdersTable : MigrationBase
{
    public CrolowOrdersTable(IMigrationContext context) : base(context)
    {
    }
    protected override void Migrate()
    {
        Logger.LogDebug("Running migration {MigrationStep}", "Add    public CrolowOrdersTable(IMigrationContext context) : base(context)\r\n");

        // Lots of methods available in the MigrationBase class - discover with this.
        if (TableExists("CrolowOrders") == false)
        {
            Create.Table<CrolowOrderSchema>().Do();
            Create.Table<CrolowOrderLineSchema>().Do();
        }
        else
        {
            Logger.LogDebug("The database table {DbTable} already exists, skipping", "CrolowOrders");
        }
    }
}