using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System.IO;

namespace TienThoBookStore.WebAPI.Services
{
    public class PdfService
    {
        /// <summary>
        /// Tạo file sample gồm N trang đầu từ file full.
        /// </summary>
        public void CreateSample(string fullPdfPath, string samplePdfPath, int pages = 10)
        {
            using var input = PdfReader.Open(fullPdfPath, PdfDocumentOpenMode.Import);
            using var output = new PdfDocument();
            int count = Math.Min(input.PageCount, pages);
            for (int i = 0; i < count; i++)
                output.AddPage(input.Pages[i]);
            // Tạo thư mục sample nếu chưa tồn tại
            var dir = Path.GetDirectoryName(samplePdfPath)!;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            output.Save(samplePdfPath);
        }
    }
}
