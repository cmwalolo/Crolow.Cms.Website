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

public class CrolowFormComposer : ComponentComposer<CrolowFormComponent>
{
}

public class CrolowFormComponent : IComponent
{
    private readonly ICoreScopeProvider _coreScopeProvider;
    private readonly IMigrationPlanExecutor _migrationPlanExecutor;
    private readonly IKeyValueService _keyValueService;
    private readonly IRuntimeState _runtimeState;

    public CrolowFormComponent(
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

        var migrationPlan = new MigrationPlan("-CrolowForm-2");
        migrationPlan.From(string.Empty)
            .To<CrolowFormTable>("crowlowform-db-2")
            .To<CrolowFormTableUpgrade_1_0>("crowlowform-db-1-0");

        var upgrader = new Upgrader(migrationPlan);
        upgrader.Execute(_migrationPlanExecutor, _coreScopeProvider, _keyValueService);
    }

    public void Terminate()
    {
    }
}
public class CrolowFormComponent_1_0 : IComponent
{
    private readonly ICoreScopeProvider _coreScopeProvider;
    private readonly IMigrationPlanExecutor _migrationPlanExecutor;
    private readonly IKeyValueService _keyValueService;
    private readonly IRuntimeState _runtimeState;

    public CrolowFormComponent_1_0(
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
        var migrationPlan = new MigrationPlan("-CrolowForm-1-0");

        // This is the steps we need to take
        // Each step in the migration adds a unique value
        migrationPlan.From(string.Empty)
            .To<CrolowFormTableUpgrade_1_0>("crowlowform-db-1-0");

        // Go and upgrade our site (Will check if it needs to do the work or not)
        // Based on the current/latest step
        var upgrader = new Upgrader(migrationPlan);
        upgrader.Execute(_migrationPlanExecutor, _coreScopeProvider, _keyValueService);
    }
    public void Terminate()
    {
    }
}
public class CrolowFormTableUpgrade_1_0 : MigrationBase
{
    public CrolowFormTableUpgrade_1_0(IMigrationContext context) : base(context)
    {
    }
    protected override void Migrate()
    {
        Logger.LogDebug("Running migration {MigrationStep}", "AddCommentsTable");

        // Lots of methods available in the MigrationBase class - discover with this.
        if (TableExists("CrolowForm") == true)
        {
            if (base.DatabaseType != DatabaseType.SQLite)
            {
                Alter.Table("CrolowForm").AlterColumn("Application").AsString().Do();
            }
        }
        else
        {
            Logger.LogDebug("The Application column could not be updated");
        }
    }
}

public class CrolowFormTable : MigrationBase
{
    public CrolowFormTable(IMigrationContext context) : base(context)
    {
    }
    protected override void Migrate()
    {
        Logger.LogDebug("Running migration {MigrationStep}", "AddCommentsTable");

        // Lots of methods available in the MigrationBase class - discover with this.
        if (TableExists("CrolowForm") == false)
        {
            Create.Table<CrolowFormSchema>().Do();
        }
        else
        {
            Logger.LogDebug("The database table {DbTable} already exists, skipping", "CrolowForm");
        }
    }
}

[TableName("CrolowForm")]
[PrimaryKey("Id", AutoIncrement = true)]
[ExplicitColumns]
public class CrolowFormSchema
{
    [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
    [Column("Id")]
    public int Id { get; set; }

    [Column("BlogPostUmbracoId")]
    public int BlogPostUmbracoId { get; set; }

    [Column("Name")]
    public required string Name { get; set; }

    [Column("Email")]
    public required string Email { get; set; }

    [Column("Website")]
    public required string Website { get; set; }

    [Column("Message")]
    [SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
    public string Message { get; set; }

    [Column("Date")]
    public DateTime Date { get; set; }

    [Column("Status")]
    public int Status { get; set; }
    [Column("Subscribe")]
    public int Subscribe { get; set; }

    [Column("Application")]
    public string Application { get; set; }
    [Column("Additional")]
    public string Additional { get; set; }

}