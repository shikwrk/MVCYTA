using YuantaMVC.Models.Dtos;

namespace YuantaMVC.InterFaces.Services
{
    public interface IDocumentService
    {
        DocumentDto GetDocument(int documentId);
    }
}
