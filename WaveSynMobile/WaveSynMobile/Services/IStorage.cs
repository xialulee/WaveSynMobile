using System.IO;
using System.Threading.Tasks;

namespace WaveSynMobile.Services {
    public interface IStorage {
        Task<Stream> AskSaveAsStream(string fileName);
    }
}
