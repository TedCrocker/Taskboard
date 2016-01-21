namespace Taskboard.Data
{
	public interface IUserManager
	{
		bool Authenticate(string userName, string password);
		void Unauthenticate();
		string  DisplayName { get; }
	}
}