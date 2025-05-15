namespace CCS.AdvProgS2.Shared.Contract.Entities;

public interface IPlayerEntity : IEntity
{
	public Guid PlayerId { get; }
}