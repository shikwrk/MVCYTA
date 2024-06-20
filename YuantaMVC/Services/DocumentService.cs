using YuantaMVC.InterFaces.Repositories;
using YuantaMVC.InterFaces.Services;
using YuantaMVC.Models;
using YuantaMVC.Models.Dtos;

namespace YuantaMVC.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;

        public DocumentService(IRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }
        /// <summary>
        /// 根據id獲得文件
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public DocumentDto GetDocument(int documentId)
        {
            var document = _documentRepository.GetById(documentId);
            if (document == null) return null;

            return new DocumentDto
            {
                Id = document.Id,
                Content = document.Content
            };
        }
    }
}
