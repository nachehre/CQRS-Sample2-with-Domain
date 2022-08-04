namespace Ordering.Commands
{
	public interface IOrderCommandHandler
	{
		void Handle(OrderCommand command);
	}
}