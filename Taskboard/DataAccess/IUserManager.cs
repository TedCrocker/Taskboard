namespace Taskboard.DataAccess
{
	public interface IUserManager
	{
		bool Authenticate(string userName, string password);
		void Unauthenticate();
		string  DisplayName { get; }
	}
}