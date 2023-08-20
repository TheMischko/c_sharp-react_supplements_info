using SupplementsServer.API.Models;

namespace SupplementsServer.API.Services; 

public interface ISupplementService {
    public Task<List<Supplement>> GetAllSupplements();
}