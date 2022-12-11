// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global
namespace Pac_Man_DesignPatterns.Utils
{
    public interface IObservable
    {
        public void RegisterObserver(IObserver parObserver);

        public void RemoveObserver(IObserver parObserver);
    }
}
