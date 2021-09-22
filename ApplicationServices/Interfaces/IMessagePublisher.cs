using System.Threading.Tasks;

namespace ApplicationServices.Interfaces
{
	public interface IMessagePublisher
	{
		Task Publish<T>(T message);
	}
}