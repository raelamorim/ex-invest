using System.Threading.Tasks;
using ExInvest.Investimentos.Models;

namespace ExInvest.Investimentos.Repositories
{
	public interface ISaldoRepository
	{
		Task Create(Saldo saldo);
		void CreateTable();
	}
}