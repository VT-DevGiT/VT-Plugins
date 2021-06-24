using Synapse.Api;
using Synapse.Api.Roles;


namespace VT_Referance.PlayerScript
{
    [API]
    public interface IScpRole : IRole
    {
        string ScpName { get; }

    }
}
