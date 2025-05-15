namespace CCS.AdvProgS2.Shared.Contract;

public interface ILevel
{
	public int Id { get; }
	public string Name { get; }
	public List<IEntity> Entities { get; }
}