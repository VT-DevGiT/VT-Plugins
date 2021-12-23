using Synapse.Api;
using Synapse.Api.Roles;


namespace VT_Referance.PlayerScript
{
    [API]
    public interface IScpDeathAnnonce : IRole
    {
        string ScpName { get; }

    }
}
